using System;
using System.Windows.Controls;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SEMP.Controls
{
    public class ItemFoundEventArgs : EventArgs<IEnumerable>
    {
        public ItemFoundEventArgs(IEnumerable result, Dictionary<String, String> filters) : base(result) { Filters = filters; }
        public Dictionary<String, String> Filters { get; private set; }
    }
    public interface IPage
    {
        void Clear();
        Object Tag { get; set; }
        void Load(CommandsMenu cmdMnu);
    }
    public interface IABMPage : IPage
    {
        void ConfigColumn(DataGridAutoGeneratingColumnEventArgs Args);
        void ItemFoundSelected(Object ItemFound);
        event EventHandler<ItemFoundEventArgs> ItemFound;
    }
    public interface ICloneable { Object Clone(); }
    public interface IPageData : ICloneable
    {
        Int32? ID { get; set; }
        void Find();
        void Save();
        void Delete();
    }
    public interface IPageDataParent
    {
        IList this[String iChildren] { get; set; }
        void NewChild(String Children);
    }
}
