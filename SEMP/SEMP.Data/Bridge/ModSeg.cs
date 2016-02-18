using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;

namespace SEMP.Data
{
    public static class ModSeg
    {
        public static Int64? GetProfileID(String User)
        {
            using (var db = new BridgeDataContext())
            {
                db.Connection.ConnectionString = HttpContext.Current.Application["ModSegConnectionString"].ToString();
                return db.ModSegGetProfileID(ConfigurationSettings.AppSettings["MODULE"].ToString(), User, null).Single().Value;
            }
        }
        public static Int64? GetEmployeeID(String User)
        {
            using (var db = new BridgeDataContext())
            {
                db.Connection.ConnectionString = HttpContext.Current.Application["ConnectionString"].ToString();
                return db.ModSegGetEmployeeID(User).Single().Value;
            }
        }
        public static Int64? GetUserID(String User)
        {
            using (var db = new BridgeDataContext())
            {
                db.Connection.ConnectionString = HttpContext.Current.Application["ModSegConnectionString"].ToString();
                return db.ModSegGetUserID(User).Single().Value;
            }
        }
        public static String GetUserName(String User)
        {
            using (var db = new BridgeDataContext())
            {
                //<< luis - 2012-07-16
                db.Connection.ConnectionString = HttpContext.Current.Application["ConnectionString"].ToString();
                //db.Connection.ConnectionString = HttpContext.Current.Application["ModSegConnectionString"].ToString();
                //>>
                return db.ModSegGetUserName(User).Single().Value;
            }
        }
        public static List<String> GetUserMenu(Int64? ProfileID)
        {
            using (var db = new BridgeDataContext())
            {
                db.Connection.ConnectionString = HttpContext.Current.Application["ModSegConnectionString"].ToString();
                var menu = db.ModSegGetUserMenu(ConfigurationSettings.AppSettings["MODULE"].ToString(), ProfileID);
                var menuNames = new List<String>();
                foreach (var option in menu)
                    menuNames.Add(option.Root);
                return menuNames;
            }
        }
        public static Int64? GetGenericProfileID(String User)
        {
            using (var db = new BridgeDataContext())
            {
                db.Connection.ConnectionString = HttpContext.Current.Application["ModSegConnectionString"].ToString();
                return db.ModSegGetGenericProfileID(ConfigurationSettings.AppSettings["MODULE"].ToString(), User, null).Single().Value;
            }
        }
    }
}
