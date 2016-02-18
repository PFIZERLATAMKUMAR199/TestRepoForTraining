using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Collections;

namespace SEMP.Controls
{
    public abstract class ABMPageControl<T> : UserControl, IABMPage, IPage where T : IPageData, new()
    {
        public ABMPageControl() { NewAction(); }
        public void Clear() { NewAction(); }
        // FRL - 2011-10-25 - Added the VIRTUAL option to allow overriding of Load method
        public virtual void Load(CommandsMenu cmdMnu)
        {
            cmdMnu.NewAction = NewAction;
            cmdMnu.FindAction = FindAction;
            cmdMnu.SaveAction = SaveAction;
            cmdMnu.CloneAction = CloneAction;
            cmdMnu.DeleteAction = DeleteAction;
            LoadPage();
        }
        protected virtual void LoadPage() { }
        public virtual void ConfigColumn(DataGridAutoGeneratingColumnEventArgs Args) { }
        public virtual void ItemFoundSelected(Object ItemFound) { DataContext = ItemFound; }

        public event EventHandler<ItemFoundEventArgs> ItemFound;
        protected void RaiseItemFound(Object Sender, ItemFoundEventArgs Args)
        {
            var handler = ItemFound;
            if (handler != null) handler(Sender, Args);
        }

        protected T DataItem { get { return (T)DataContext; } }
        private void NewAction() { DataContext = new T(); }
        private void FindAction() { DataItem.Find(); }
        private void SaveAction()
        {
            //FRL - 2011-11-30 : try catch added to automatically handle programmer made exceptions
            try
            {
                DataItem.Save();
            }
            catch (Exception e)
            {
                if (e.InnerException == null)
                {
                    throw new Exception(LanguageDictionary.Current[e.Message].ToString());
                }
                throw e;
            }
        }
        private void CloneAction() { DataContext = DataItem.Clone(); }
        private void DeleteAction() { DataItem.Delete(); NewAction(); }

        protected abstract Dictionary<String, String> Filters { get; }
        protected void WCF_Get(Object Sender, IEventArgs<ObservableCollection<T>> Args)
        {
            RaiseItemFound(this, new ItemFoundEventArgs(Args.Result, Filters));
        }
        protected void WCF_Save(Object Sender, IEventArgs<Int32?> Args)
        {
            ((IPageData)DataContext).ID = Args.Result;
        }
        protected void WCF_Delete(Object Sender, AsyncCompletedEventArgs Args)
        {
            //if (Args.Error != null) throw Args.Error;
        }
    }
}
