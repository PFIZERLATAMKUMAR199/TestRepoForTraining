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
{// luis - 2012-06-12
    public partial class SolicitudePage : UserControl, IPage
    {
        
        public SolicitudePage()
            : base()
        {
            ORM.Current.LanguageGetWordsCompleted += Language_GetWordsCompleted;
            InitializeComponent();
            ORM.Current.SolicitudeSaveCompleted += Solicitude_Completed;
            // <<-- FRL - 2012-10-18
            dtPrescriptionEndDate.SetBinding(DatePicker.IsEnabledProperty, new Binding("TreatmentID")
            {
                Source = this.DataContext,
                Mode = BindingMode.OneWay,
                Converter = BooleanConverter.Current,
                ConverterParameter = new Predicate<Object>(x => (int) x != 1)
            });
            dtPrescriptionEndDate.DisplayDateStart = DateTime.Now;
            dtPrescriptionEndDate.DisplayDateEnd = DateTime.Now.AddMonths(6);
            // -->>
        }
        public void Load(CommandsMenu cmdMnu) { cmdMnu.SaveAction = Save_Click; cmdMnu.NewAction = New_Click; }
        public void Clear()
        {
            var MySolicitude = new Solicitude();
            DataContext = MySolicitude;
        }
        private void Language_GetWordsCompleted(Object Sender, LanguageGetWordsCompletedEventArgs Args)
        {//luis - 2012-06-26
            grdChildren.Columns[1].Header = App.Lang["Product"];
            grdChildren.Columns[2].Header = App.Lang["Dose"];
            grdChildren.Columns[3].Header = App.Lang["Hours"];
            grdChildren.Columns[4].Header = App.Lang["Days"];
            grdChildren.Columns[5].Header = App.Lang["Ammount"];
            grdChildren.Columns[6].Header = App.Lang["Value"];

            grdUserLastSolicitudes.Columns[0].Header = App.Lang["ReceptionDate"];
            grdUserLastSolicitudes.Columns[1].Header = App.Lang["Product"];
            grdUserLastSolicitudes.Columns[2].Header = App.Lang["Ammount"];
            grdUserLastSolicitudes.Columns[3].Header = App.Lang["Status"];
            grdUserLastSolicitudes.Columns[4].Header = App.Lang["Relative"];
            grdUserLastSolicitudes.Columns[5].Header = "Tratamiento";
            grdUserLastSolicitudes.Columns[6].Header = "Fecha Expiracion";
            grdUserLastSolicitudes.Columns[7].Header = "Receta Inicio Fecha";
            grdUserLastSolicitudes.Columns[8].Header = "Receta Fin Fecha";
       

        }

        private void Solicitude_Completed(Object Sender, SolicitudeSaveCompletedEventArgs Args)
        {//luis - 2012-06-29

            ((Solicitude)DataContext).ID = Args.Result;
            DataContext = new Solicitude();
            txtProduct.Text = "";
            txtDose.Text = "";
            txtHours.Text = "";
            txtDay.Text = "";
        }

        private void Save_Click()
        {
            grdChildren.SelectedIndex = -1; //THIS LINE MAKES SURE THE DATAGRID UPDATES THE LAST CHANGED ROW. (Datagrids don't update source until the editing row is deselected)
            ORM.Current.SolicitudeSaveAsync(((Solicitude)DataContext));
        }

        private void ClearChangedUser()
        {//luis - 2012-06-25
            var MySolicitude = ((Solicitude)DataContext);
            MySolicitude.Detail.Clear();
            ClearCombo(cmdRelativeActiveOfUser);
            ClearCombo(cboProduct);
            ProductActiveOfUser.nUser = Convert.ToInt32(cboSolicitor.SelectedValue);
            RelativeActiveOfUser.nUser = Convert.ToInt32(cboSolicitor.SelectedValue);
        }

        private void cmdProduct_Click(object sender, RoutedEventArgs e)
        {//luis - 2011-12-21
            txtDose.Text = "";
            txtHours.Text = "";
            txtDay.Text = "";
            ProductActiveOfUser.sName = txtProduct.Text;
        }

        private void cmdAddProduct_Click(object sender, RoutedEventArgs e)
        {//luis - 2011-12-21
            var MySolicitude = ((Solicitude)DataContext);
            int nDose = Convert.ToInt16((txtDose.Text == "" ? "0" : txtDose.Text));
            int nHours = Convert.ToInt16((txtHours.Text == "" ? "0" : txtHours.Text));
            int nDay = Convert.ToInt16((txtDay.Text == "" ? "0" : txtDay.Text));

            //<< luis - 2012-07-21
            if (nDose <= 0) throw new System.InvalidOperationException("La dosis no puede ser cero");
            if (nHours <= 0) throw new System.InvalidOperationException("Las horas no pueden ser cero");
            if (nDay <= 0) throw new System.InvalidOperationException("Los días no pueden ser cero");
            //>>

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
            
            MySolicitude.Detail.Add
                (new SolicitudeItem()
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

        private void cmdUserSolicitudes_Click(object sender, RoutedEventArgs e)
        {//luis - 2012-06-21
            ORM.Current.SolicitudeProductsOfUserCompleted += Current_SolicitudeProductsOfUserCompleted;
            ORM.Current.SolicitudeProductsOfUserAsync(Convert.ToInt32(cboSolicitor.SelectedValue));
        }

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {//FRL - 2012-01-04
            grdUserLastSolicitudes.Visibility = Visibility.Collapsed;
            cmdBack.Visibility = Visibility.Collapsed;
            grdInner.Visibility = Visibility.Visible;
            ((Solicitude)DataContext).UserSolicitudes = null;
        }
        private void Current_SolicitudeProductsOfUserCompleted(object sender, SolicitudeProductsOfUserCompletedEventArgs e)
        {//FRL - 2012-01-04
            grdInner.Visibility = Visibility.Collapsed;
            cmdBack.Visibility = Visibility.Visible;
            grdUserLastSolicitudes.Visibility = Visibility.Visible;
            ((Solicitude)DataContext).UserSolicitudes = e.Result;
            ORM.Current.SolicitudeProductsOfUserCompleted -= Current_SolicitudeProductsOfUserCompleted;
        }

        private void New_Click()
        {//FRL - 2012-01-06
            DataContext = new Solicitude() { SolicitorID = ((Solicitude)DataContext).SolicitorID };
            cboSolicitor.SelectedIndex = (cboSolicitor.Items.Count < 3 ? cboSolicitor.Items.Count - 1 : 0);
            txtDose.Text = "";
            txtHours.Text = "";
            txtDay.Text = "";
            ClearChangedUser();
        }

        private void cboSolicitor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearChangedUser();
        }

        private void ClearCombo(ComboBox cmb)
        {
            cmb.ItemsSource = NamedEntityHelper.DefaultList;
            cmb.SelectedIndex = 0;
        }

        private void TreatmentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dtPrescriptionDate_SelectedDateChanged(sender, e);
        }

        private void dtPrescriptionDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((Solicitude)DataContext).TreatmentID == 1)
            {
                dtPrescriptionEndDate.SelectedDate = dtPrescriptionDate.SelectedDate;
            }
        }
    }
}
