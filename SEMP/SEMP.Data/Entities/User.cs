using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEMP.Data
{
    partial class User
    {
        public static IEnumerable<NamedEntity> SelectActiveAll()
        {
            using (var db = new BridgeDataContext())
            {
                return db.UserSelectAsNamedEntity(null, null, null, null, null, null, null, true, 
                                    (Int32)HttpContext.Current.Session["LangID"],
                                    Convert.ToInt32( HttpContext.Current.Session["EmployeeID"].ToString())).ToList();
            }
        }
        public static IEnumerable<NamedEntity> SelectAll()
        {
            using (var db = new BridgeDataContext())
            {
                return db.UserSelectAsNamedEntity(null, null, null, null, null, null, null, null,
                                    (Int32)HttpContext.Current.Session["LangID"], 
                                    Convert.ToInt32( HttpContext.Current.Session["EmployeeID"].ToString())).ToList();
            }
        }
        public IEnumerable<User> Select()
        {
            using (var db = new BridgeDataContext())
            {
                return db.UserSelect(ID, FirstName, LastName, EMail, RequestsForOtherUsers, Phone, EmplID, Active, 
                    (Int32)HttpContext.Current.Session["LangID"], 
                    Convert.ToInt32( HttpContext.Current.Session["EmployeeID"].ToString())).ToList();
            }
        }
        public Int32? Save()
        {
            using (var db = new BridgeDataContext())
            {
                return db.UserSave(ID, RequestsForOtherUsers, Active, 
                        (Int32)HttpContext.Current.Session["LangID"],
                        Convert.ToInt32(HttpContext.Current.Session["EmployeeID"].ToString()));
            }
        }
        public Int32? Delete()
        {
            using (var db = new BridgeDataContext())
            {
                return db.UserDelete(ID, 
                    (Int32)HttpContext.Current.Session["LangID"],
                    Convert.ToInt32(HttpContext.Current.Session["EmployeeID"].ToString()));
            }
        }
    }
}
