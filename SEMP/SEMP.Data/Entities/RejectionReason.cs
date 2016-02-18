using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Data.Linq;

namespace SEMP.Data
{//FRL - 2011-10-24

    partial class RejectionReason
    {
        public static IEnumerable<NamedEntity> SelectAll()
        {
            using (var db = new BridgeDataContext())
            {
                return db.RejectionReasonSelectAsNamedEntity(null, null, (Int32?)HttpContext.Current.Session["LangID"], (Int64?)HttpContext.Current.Session["UserID"]).ToList();
            }
        }
        public IEnumerable<RejectionReason> Select()
        {
            using (var db = new BridgeDataContext())
            {
                return db.RejectionReasonSelect(ID, Name, (Int32?)HttpContext.Current.Session["LangID"], (Int64?)HttpContext.Current.Session["UserID"]).ToList();
            }
        }
        public Int32? Save()
        {
            using (var db = new BridgeDataContext())
            {
                return db.RejectionReasonSave(ID, Name, (Int32?)HttpContext.Current.Session["LangID"], (Int64?)HttpContext.Current.Session["UserID"]);
            }
        }
        public void Delete()
        {
            using (var db = new BridgeDataContext())
            {
                db.RejectionReasonDelete(ID, (Int32)HttpContext.Current.Session["LangID"]);
            }
        }
    }
}
