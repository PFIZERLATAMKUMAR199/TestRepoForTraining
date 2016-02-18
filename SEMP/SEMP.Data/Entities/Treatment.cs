using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Data.Linq;

namespace SEMP.Data
{//luis - 2012-06-21

    partial class Treatment
    {
        
        public static IEnumerable<NamedEntity> SelectAll()
        {
            using (var db = new BridgeDataContext())
            {
                return db.TreatmentSelectAsNamedEntity(null, null,
                    (Int32)HttpContext.Current.Session["LangID"],
                    Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString()),
                    Convert.ToInt32(HttpContext.Current.Session["EmployeeID"].ToString())).ToList();
            }
        }
        public IEnumerable<Treatment> Select()
        {
            using (var db = new BridgeDataContext())
            {
                try
                {
                    var nia = db.TreatmentSelect(ID, Name,
                    (Int32)HttpContext.Current.Session["LangID"],
                    Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString()),
                    Convert.ToInt32(HttpContext.Current.Session["EmployeeID"].ToString()));
                    return nia.ToList();
                }
                catch (Exception aa)
                {

                    throw aa;
                }
            }
        }
        //public Int32? Save()
        //{
        //    using (var db = new BridgeDataContext())
        //    {
        //        return db.TreatmentSave(ID, Name, RUT, BornDate, KinshipID, (Int32)HttpContext.Current.Session["LangID"], Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString()));
        //    }
        //}
        //public void Delete()
        //{
        //    using (var db = new BridgeDataContext())
        //    {
        //        db.TreatmentDelete(ID, (Int32)HttpContext.Current.Session["LangID"], Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString()));
        //    }
        //}

    }
}
