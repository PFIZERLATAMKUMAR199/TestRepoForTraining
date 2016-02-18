using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Data.Linq;

namespace SEMP.Data
{
    partial class SolicitorKind
    { //FRL - 2011-10-24
        public static IEnumerable<NamedEntity> SelectAll()
        {
            using (var db = new BridgeDataContext())
            {//luis - 2011-11-21
                return db.SolicitorKindSelectAsNamedEntity(null, null, null, null, null, null, null, 
                    Convert.ToInt32(HttpContext.Current.Session["EmployeeID"])).ToList();
            }
        }
        public IEnumerable<SolicitorKind> Select()
        {
            using (var db = new BridgeDataContext())
            {//luis - 2011-11-21
                return db.SolicitorKindSelect(ID, Name, RequestsWithoutStock, MaxProductAmmountPerRequest, MaxProductAmmountPerMonth, MaxValidTime, MaxRequestsPerMonth, 
                    Convert.ToInt32(HttpContext.Current.Session["EmployeeID"])).ToList();
            }
        }
        public Int32? Save()
        {
            using (var db = new BridgeDataContext())
            {//luis - 2011-11-21
                return db.SolicitorKindSave(ID, Name, RequestsWithoutStock, MaxProductAmmountPerRequest, MaxProductAmmountPerMonth, MaxValidTime, MaxRequestsPerMonth, Serializer.Serialize(Detail), 
                    (Int32)HttpContext.Current.Session["LangID"]);
            }
        }
        public void Delete()
        {//luis - 2011-11-02
            using (var db = new BridgeDataContext())
            {
                db.SolicitorKindDelete(ID, (Int32)HttpContext.Current.Session["LangID"]);
            }
        }

        // <<-- FRL - 2011-11-22
        public List<ProductSolicitorKind> Detail { get; set; }

        public IEnumerable<ProductSolicitorKind> GetChildren()
        {
            using (var db = new BridgeDataContext())
            {
                return db.ProductSolicitorKindSelect(ID).ToList();
            }
        }
        // -- >>
    }
}
