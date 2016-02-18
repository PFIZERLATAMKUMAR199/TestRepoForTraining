using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Data.Linq;

namespace SEMP.Data
{//FRL - 2012-06-11

    partial class Kinship
    {
        public static IEnumerable<NamedEntity> SelectAll()
        {
            using (var db = new BridgeDataContext())
            {
                return db.KinshipSelectAsNamedEntity(null, null, null, Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString())).ToList();
            }
        }
        public IEnumerable<Kinship> Select()
        {
            using (var db = new BridgeDataContext())
            {
                return db.KinshipSelect(ID, Name, KinshipKindID, Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString())).ToList();
            }
        }
        public Int32? Save()
        {
            using (var db = new BridgeDataContext())
            {
                return db.KinshipSave(ID, Name, KinshipKindID, (Int32)HttpContext.Current.Session["LangID"], Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString()));
            }
        }
        public void Delete()
        {
            using (var db = new BridgeDataContext())
            {
                db.KinshipDelete(ID, (Int32)HttpContext.Current.Session["LangID"], Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString()));
            }
        }
        public static IEnumerable<NamedEntity> GetKinds()
        {
            using (var db = new BridgeDataContext())
            {
                return db.KinshipKindSelectAsNamedEntity(Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString())).ToList();
            }
        }

    }
}
