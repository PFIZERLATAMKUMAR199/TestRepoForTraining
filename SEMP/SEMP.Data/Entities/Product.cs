using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEMP.Data
{//luis - 2012-06-13
    partial class Product
    {
        public static IEnumerable<NamedEntity> SelectActiveOfUserAll(int? nUser, string sName)
        {
            using (var db = new BridgeDataContext())
            {
                db.CommandTimeout = db.CommandTimeout * 2;
                return db.ProductGetActiveOfUserSelectAsNamedEntity(
                        Convert.ToInt32(HttpContext.Current.Session["EmployeeID"]), sName).ToList();
            }
        }

        public static IEnumerable<NamedEntity> SelectActiveAll()
        {
            using (var db = new BridgeDataContext())
            {
                db.CommandTimeout = db.CommandTimeout * 2;
                return db.ProductSelectAsNamedEntity(null, null, null, true, null, null, null, null, null, null,
                        (Int32)HttpContext.Current.Session["LangID"], HttpContext.Current.Session["NetUser"].ToString(),
                        null).ToList();
            }
        }
        public static IEnumerable<NamedEntity> SelectAll()
        {
            using (var db = new BridgeDataContext())
            {
                db.CommandTimeout = db.CommandTimeout * 2;
                return db.ProductSelectAsNamedEntity(null, null, null, null, null, null, null, null, null, null,
                        (Int32)HttpContext.Current.Session["LangID"], HttpContext.Current.Session["NetUser"].ToString(), 
                        null).ToList();
            }
        }
        public IEnumerable<Product> Select()
        {
            using (var db = new BridgeDataContext())
            {
                db.CommandTimeout = db.CommandTimeout * 2;
                return db.ProductSelect(ID, Name, ProductOrigin, Active, MonthlyAmount, AnnualAmount, 
                    ColdChain, ReplenishPoint, MaxAge, UnitPackage,
                    (Int32)HttpContext.Current.Session["LangID"], HttpContext.Current.Session["NetUser"].ToString(), 
                    Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString())).ToList();
            }
        }

        public Int32? Save()
        {
            using (var db = new BridgeDataContext())
            {
                return db.ProductSave(ID, Name, ProductOrigin, Active, MonthlyAmount, AnnualAmount,
                    ColdChain, ReplenishPoint, MaxAge, UnitPackage,
                    (Int32)HttpContext.Current.Session["LangID"], 
                    HttpContext.Current.Session["NetUser"].ToString(),
                    Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString()));
            }
        }
        public void Delete()
        {
            using (var db = new BridgeDataContext())
            {
                db.ProductDelete(ID, 
                        (Int32)HttpContext.Current.Session["LangID"], 
                        HttpContext.Current.Session["EmployeeID"].ToString());
            }
        }

    }
}
