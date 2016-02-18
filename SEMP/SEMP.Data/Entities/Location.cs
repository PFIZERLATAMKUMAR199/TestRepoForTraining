using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEMP.Data
{//luis - 2012-07-05
    partial class Location
    {
        public static IEnumerable<NamedEntity> SelectAll()
        {
            using (var db = new BridgeDataContext())
            {
                return db.LocationSelectAsNamedEntity(null, null, true).ToList();
            }
        }
        public IEnumerable<Location> Select()
        {
            using (var db = new BridgeDataContext())
            {
                return db.LocationSelect(ID, Name, Active).ToList();
            }
        }
        public Int32? Save()
        {
            using (var db = new BridgeDataContext())
            {
                return db.LocationSave(ID, Name, Active, (Int32)HttpContext.Current.Session["LangID"]);
            }
        }
        public void Delete()
        {
            using (var db = new BridgeDataContext())
            {
                db.LocationDelete(ID, (Int32)HttpContext.Current.Session["LangID"]);
            }
        }
    }
}
