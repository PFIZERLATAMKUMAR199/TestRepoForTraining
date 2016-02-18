using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using SEMP.Controls;
using SEMP.Logic.WCF.Services;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;


namespace SEMP.View.Pages
{// FRL - 2012-06-18
    public partial class ModifySolicitudePage : UserControl, IPage
    {
        CommandsMenu commands;
         int id;
        //syed 29
         ModifySolicitude modDetail = new ModifySolicitude();
        //syed 30 oct 2013
        
       
        public ModifySolicitudePage()
            : base()
        {
            ORM.Current.LanguageGetWordsCompleted += Language_GetWordsCompleted;
            InitializeComponent();
            //ORM.Current.SolicitudeGetCompleted += Solicitude_ShowInGrid;

            //syed 30 oct
            
            ORM.Current.SolicitudeGetToModifyCompleted += Solicitude_ShowInGrid1;
        


            ORM.Current.ModifySolicitudeModifyCompleted += ModifySolicitude_Completed;
            //ORM.Current.SolicitudeGetItemsModifyCompleted += SolicitudeGetItemsModify_Completed;
            

        }
        public void Load(CommandsMenu cmdMnu)
        {
            commands = cmdMnu;
            cmdMnu.ReloadAction = Clear;
            grdToModify.Visibility = Visibility.Collapsed;
            grdSolicitudeDetail.Visibility = Visibility.Collapsed;
            ((ModifySolicitude)DataContext).PendingSolicitudeDetail = null;
            //syed 30 oct
            grdInner.Visibility = Visibility.Collapsed;
            txtClick.Visibility = Visibility.Visible;
           
        }
            
    
        public void Clear()
        {
            grdToModify.SelectedIndex = -1; 
            var ProcSolicitude = new ModifySolicitude();
            DataContext = ProcSolicitude;
            //syed 31
            

            grdToModify.Visibility = Visibility.Visible;
            ORM.Current.SolicitudeGetToModifyAsync(2);
        }
        private void Language_GetWordsCompleted(Object Sender, LanguageGetWordsCompletedEventArgs Args)
        {
            grdToModify.Columns[0].Header = App.Lang["Solicitude"];
            grdToModify.Columns[1].Header = App.Lang["Solicitor"];
            grdToModify.Columns[2].Header = App.Lang["Relative"];
            grdToModify.Columns[3].Header = App.Lang["Kinship"];
            grdToModify.Columns[4].Header = App.Lang["ReceptionDate"];
           

            grdSolicitudeDetail.Columns[0].Header = App.Lang["Product"];
            grdSolicitudeDetail.Columns[1].Header = App.Lang["Dose"];
            grdSolicitudeDetail.Columns[2].Header = App.Lang["Hours"];
            grdSolicitudeDetail.Columns[3].Header = App.Lang["Ammount"];
            //neha 17-oct-2013
            grdSolicitudeDetail.Columns[4].Header = "Tratamiento";
            grdSolicitudeDetail.Columns[5].Header = "Fecha Expiracion";
            grdSolicitudeDetail.Columns[6].Header = "Receta Inicio Fecha";
            grdSolicitudeDetail.Columns[7].Header = "Receta Fin Fecha";

            //Adding Header values for Grid in Carga Page

            grdChildren.Columns[1].Header = App.Lang["Product"];
            grdChildren.Columns[2].Header = App.Lang["Dose"];
            grdChildren.Columns[3].Header = App.Lang["Hours"];
            grdChildren.Columns[4].Header = App.Lang["Days"];
            grdChildren.Columns[5].Header = App.Lang["Ammount"];
            grdChildren.Columns[6].Header = App.Lang["Value"];
            grdChildren.Columns[7].Header ="ProductoID";

        }

        private void Solicitude_ShowInGrid1(Object Sender, SolicitudeGetToModifyCompletedEventArgs Args)
        {
            //syed 28
            grdInner.Visibility = Visibility.Collapsed;

         
            
            var pendings = new ObservableCollection<PendingSolicitude>();
            //Args.Result.AsEnumerable().Where(f => f.StatusID.Equals(2)).ToList().ForEach(x => pendings.Add(new PendingSolicitude() { ID = x.ID, Name = x.Name, StatusID = x.StatusID, ReceptionDate = x.ReceptionDate, RelativeName = x.RelativeName, KinshipName = x.KinshipName }));
            Args.Result.AsEnumerable().ToList().ForEach(x => pendings.Add(new PendingSolicitude() { ID = x.ID, Name = x.Name, StatusID = x.StatusID, ReceptionDate = x.ReceptionDate, RelativeName = x.RelativeName, KinshipName = x.KinshipName, sSolicitudeType=x.sSolicitudeType, dtReceiptInitialDate=x.dtReceiptInitialDate,dtReceiptFinalDate=x.dtReceiptFinalDate}));
            
            //syed 31st oct

      

           // Args.Result.AsEnumerable().Where(f => f.SolicitorID == 3161).ToList().ForEach(x => pendings.Add(new PendingSolicitude() { ID = x.ID, Name = x.Name, StatusID = x.StatusID, ReceptionDate = x.ReceptionDate, RelativeName = x.RelativeName, KinshipName = x.KinshipName }));

            ((ModifySolicitude)DataContext).PendingSolicitudes = pendings;
            ((ModifySolicitude)DataContext).ShowAsChanged();
       
                
        
        
            }
          
            
        private void ModifySolicitude_Completed(Object Sender, ModifySolicitudeModifyCompletedEventArgs Args)
        {
            var pendings = new ObservableCollection<PendingSolicitude>();
            if (Args.Result.Value != 0)
            {
                this.Clear();
            }
        }

        //saiyam

        private void chkTreatmentChange_Checked(object sender, RoutedEventArgs e)
        {
                //MessageBox.Show("If you check this box, it will delete your application after you save","Treatment Changed", MessageBoxButton.OKCancel);
            MessageBoxResult result = MessageBox.Show("Si se marca esta casilla, se eliminarán de la aplicación después de guardar", "Tratamiento Cambiado", MessageBoxButton.OKCancel);

            // Process message box result

            switch (result)

            {

                case MessageBoxResult.OK:
                    {

                    grdChildren.IsEnabled = false;
//disabling all combo box
                    cboLocationCombo.IsEnabled = false;
                    cmdRelativeActiveOfUser.IsEnabled = false;
                    cboTreatment.IsEnabled = false;
                    txtObservation.IsEnabled = false;
                    dtPrescriptionDate.IsEnabled = false;
                    dtPrescriptionEndDate.IsEnabled = false;
                    txtDay.IsEnabled = false;
                    txtDose.IsEnabled = false;
                    txtHours.IsEnabled = false;

                    break;
                    }
                case MessageBoxResult.No:
                    {
                        (sender as CheckBox).IsChecked = false;

                    break;
                    }
                case MessageBoxResult.Cancel:

                    {

                        (sender as CheckBox).IsChecked = false;

                        break;

                    }

            }

                
        }

        //syed 28th october
        private void Save_Click()
        {
            int flag = 0;
            if (chkTreatmentChange.IsChecked == true)
            {
                flag = 1;
                
            }
            else
            {
                flag = 0;
            }
            grdToModify.SelectedIndex = -1; //THIS LINE MAKES SURE THE DATAGRID UPDATES THE LAST CHANGED ROW. (Datagrids don't update source until the editing row is deselected)
       
            
            ((ModifySolicitude)DataContext).Children = null;
            if(modDetail.Children.Count !=0)
            {
            modDetail.Children.Clear();
            }
            foreach (var item in ((ModifySolicitude)DataContext).UserSolicitudesModify)
            {
                modDetail.Children.Add
         (new SolicitudeItem()
         {
             Name = item.Name,
             Dose = item.Dose,
             Hours = item.Hours,
             Days = item.Days,
             Ammount = item.Ammount,
             Value = item.Value,
             ProductID = item.ProductID
         }
         );
            
            
        }
            
            ((ModifySolicitude)DataContext).Children = modDetail.Children;
            //end of code
            ORM.Current.SolicitudeModifySaveAsync(((ModifySolicitude)DataContext),id,flag);
        }

  
        
        private void grdChildren_CurrentCellChanged(object sender, EventArgs e)
        {
            grdToModify.BeginEdit();
        }

        private void SolicitudeModify_Completed(Object Sender, SolicitudeModifySaveCompletedEventArgs Args)
        {//lsyed handling save completed event

         //   ((ModifySolicitude)DataContext).ID = Args.Result;
            DataContext = new ModifySolicitude();
            txtProduct.Text = "";
            txtDose.Text = "";
            txtHours.Text = "";
            txtDay.Text = "";
        }

        private void grdToModify_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            //enabling all fields
            grdChildren.IsEnabled = true;
            cboLocationCombo.IsEnabled = true;
            cmdRelativeActiveOfUser.IsEnabled = true;
            cboTreatment.IsEnabled = true;
            txtObservation.IsEnabled = true;
            dtPrescriptionDate.IsEnabled = true;
            dtPrescriptionEndDate.IsEnabled = true;
            txtDay.IsEnabled = true;
            txtDose.IsEnabled = true;
            txtHours.IsEnabled = true;
            if (chkTreatmentChange.IsChecked == true)
            {
               
                chkTreatmentChange.IsChecked = false;

            }

            //syed 30 oct- clearing fields and hiding label of refresh button
            txtClick.Visibility = Visibility.Collapsed;
            txtDay.Text = "";
            txtDose.Text = "";
            txtHours.Text = "";

            //syed 28 oct
            commands.SaveAction = Save_Click;
            commands.CancelAction = cmdBack_Click;
            //Prescription Start Date and Prescription End Date
            dtPrescriptionEndDate.SetBinding(DatePicker.IsEnabledProperty, new Binding("TreatmentID")
            {
                Source = this.DataContext,
                Mode = BindingMode.OneWay,
                Converter = BooleanConverter.Current,
                ConverterParameter = new Predicate<Object>(x => (int)x != 1)
            });
            dtPrescriptionEndDate.DisplayDateStart = DateTime.Now;
            dtPrescriptionEndDate.DisplayDateEnd = DateTime.Now.AddMonths(6);
            //not needed below event
            ORM.Current.SolicitudeModifySaveCompleted += SolicitudeModify_Completed;

            // -->>

            cboProduct.SelectedIndex = -1;
            txtProduct.Text = "";
            grdInner.Visibility = Visibility.Visible;
        
          
                commands.ReloadAction = CommandsMenu.NoneAction;
            //    commands.SaveAction = CommandsMenu.NoneAction;
                grdToModify.Visibility = Visibility.Collapsed;
            //    grdSolicitudeDetail.Visibility = Visibility.Visible;

            //to populate product grid based on solicitud selected
                ORM.Current.SolicitudeProductsOfUserModifyCompleted  += Current_SolicitudeProductsOfUserModifyCompleted;
               // Solicitud ID
                id = Convert.ToInt32(((Solicitude)grdToModify.SelectedItem).ID);
                ORM.Current.SolicitudeProductsOfUserModifyAsync(id);
                //((NamedEntityComboBox)cboLocationCombo).SelectedIndex = 1;
            
            //syed 25 th october
            ORM.Current.SolicitudeFieldsToModifyCompleted += Current_SolicitudeFieldsToModifyCompleted;
            ORM.Current.SolicitudeFieldsToModifyAsync(id);

            
        }

      

        private void cmdBack_Click()
        {
            //syed 30 oct- code to refresh the page on page load
            txtClick.Visibility = Visibility.Visible;
            //syed 28
            //cmdBack.Visibility = Visibility.Collapsed;
            commands.CancelAction = CommandsMenu.NoneAction;
            //grdUserLastSolicitudes.Visibility = Visibility.Collapsed;

       
            //syed 28
            grdInner.Visibility = Visibility.Collapsed;
            grdToModify.Visibility = Visibility.Visible;
            commands.ReloadAction = Clear;
            commands.SaveAction = CommandsMenu.NoneAction;
        }






        //syed
        private void Current_SolicitudeProductsOfUserModifyCompleted(object sender, SolicitudeProductsOfUserModifyCompletedEventArgs e)
        {
            grdInner.Visibility = Visibility.Visible;
            //cmdBack.Visibility = Visibility.Collapsed;
                     
            ((ModifySolicitude)DataContext).UserSolicitudesModify = e.Result;
            ORM.Current.SolicitudeProductsOfUserModifyCompleted -= Current_SolicitudeProductsOfUserModifyCompleted;

   
        }

        //syed 25th oct populating fields

        private void Current_SolicitudeFieldsToModifyCompleted(object sender, SolicitudeFieldsToModifyCompletedEventArgs e)
        {
            grdInner.Visibility = Visibility.Visible;

            //syed 28 oct
           // cmdBack.Visibility = Visibility.Collapsed;
            //grdUserLastSolicitudes.Visibility = Visibility.Collapsed;

          //  int temp = Convert.ToInt32(((NamedEntityComboBox)cboSolicitor).SelectedValue);

            var fields = new ObservableCollection<Solicitude>();
            foreach (var x in e.Result)
            {
                ((NamedEntityComboBox)cboLocationCombo).SelectedValue = x.LocationID;
                ((NamedEntityComboBox)cboTreatment).SelectedValue = x.TreatmentID;
                ((NamedEntityComboBox)cmdRelativeActiveOfUser).SelectedValue = x.RelativeID;
                //syed 30 oct
                ((NamedEntityComboBox)cboSolicitor).SelectedValue = x.IDSolicitor;
                dtPrescriptionDate.SelectedDate = x.ReceptionDate;
                dtPrescriptionEndDate.SelectedDate = x.EndDate;
                
                if (x.Observations != null)
                {
                    txtObservation.Text = x.Observations;
                }
                else
                    txtObservation.Text = "";
            }

        }

        

        private void cmdProduct_Click(object sender, RoutedEventArgs e)
        {//syed 28 oct 2013
            txtDose.Text = "";
            txtHours.Text = "";
            txtDay.Text = "";
            ProductActiveOfUser.sName = txtProduct.Text;
        }



        //functionality to add product
        private void cmdAddProduct_Click(object sender, RoutedEventArgs e)
        {
            //syed 24-10-2013
            var MySolicitude = ((ModifySolicitude)DataContext);

            int nDose = Convert.ToInt16((txtDose.Text == "" ? "0" : txtDose.Text));
            int nHours = Convert.ToInt16((txtHours.Text == "" ? "0" : txtHours.Text));
            int nDay = Convert.ToInt16((txtDay.Text == "" ? "0" : txtDay.Text));

            //syed 24-10-2013
            if (nDose <= 0) throw new System.InvalidOperationException("La dosis no puede ser cero");
            if (nHours <= 0) throw new System.InvalidOperationException("Las horas no pueden ser cero");
            if (nDay <= 0) throw new System.InvalidOperationException("Los días no pueden ser cero");

            //syed 28 oct 2013 added condition to check if product is selected or not

            if (((NamedEntityComboBox)cboProduct).SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un producto");
            }
            else
            {
                string sProduct = ((NamedEntity)cboProduct.SelectedItem).Name;
                int nProduct = Convert.ToInt32(cboProduct.SelectedValue);
                int i = sProduct.IndexOf("[ Valor = ") + "[ Valor = ".Length;
                string sValue = sProduct.Substring(i);

                i = sValue.IndexOf(" ]");
                sValue = sValue.Substring(0, i);
                i = sValue.IndexOf(".");
                decimal dblValueProduct = 0;
                if (i <= 0) i = sValue.IndexOf(",");
                if (i <= 0)
                    dblValueProduct = Convert.ToDecimal(sValue);
                else
                {
                    dblValueProduct = Convert.ToDecimal(sValue.Substring(0, i)) + Convert.ToDecimal(sValue.Substring(i + 1)) / 100;
                }

                i = sProduct.IndexOf("[ Unid. = ") + "[ Unid. = ".Length;
                string sUnitPackage = sProduct.Substring(i);
                sUnitPackage = sUnitPackage.Substring(0, sUnitPackage.Length - 2);
                double dblUnitPackage = Convert.ToInt32(sUnitPackage);

                int nAmmount = Convert.ToInt32(Math.Ceiling((nDose * (24 / nHours) * nDay) / dblUnitPackage));
                decimal dblValues = dblValueProduct * nAmmount;

                MySolicitude.UserSolicitudesModify.Add(
                    new SolicitudeProductsOfUserModifyResult()
                    {
                        Ammount = nAmmount,
                        ProductID = nProduct,
                        Name = sProduct,
                        Dose = nDose,
                        Hours = nHours,
                        Days = nDay,
                        Value = dblValues
                    }

               );
              

            }
        }
 
        //syed 28 oct handling dtPrecsriptiondate event based on treatment selection

        private void TreatmentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dtPrescriptionDate_SelectedDateChanged(sender, e);
        }

        private void dtPrescriptionDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (((ModifySolicitude)DataContext).TreatmentID  == 1)
            if (Convert.ToInt32(((NamedEntityComboBox)cboTreatment).SelectedValue) == 1)
            {
                dtPrescriptionEndDate.SelectedDate = dtPrescriptionDate.SelectedDate;
            }
        }

        private void chkTreatmentChange_Unchecked(object sender, RoutedEventArgs e)
        {
            grdChildren.IsEnabled = true;
            cboLocationCombo.IsEnabled = true;
            cmdRelativeActiveOfUser.IsEnabled = true;
            cboTreatment.IsEnabled = true;
            txtObservation.IsEnabled = true;
            dtPrescriptionDate.IsEnabled = true;
            dtPrescriptionEndDate.IsEnabled = true;
            txtDay.IsEnabled = true;
            txtDose.IsEnabled = true;
            txtHours.IsEnabled = true;
            
        }

     
       
    }
}
