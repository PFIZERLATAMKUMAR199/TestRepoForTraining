using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Data.Linq;

namespace SEMP.Data
{//FRL - 2012-06-12

    partial class Relative
    {
        public bool Process { get; set; }
        public static IEnumerable<NamedEntity> SelectActiveOfUserAll(int? nUser)
        {//luis - 2012-06-19
            using (var db = new BridgeDataContext())
            {
                return db.RelativeGetActiveOfUserSelectAsNamedEntity(null, null, null, null, null, null,
                    nUser, 
                    (Int32)HttpContext.Current.Session["LangID"], 
                    Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString())).ToList();
            }
        }

        public static IEnumerable<NamedEntity> SelectAll()
        {
            using (var db = new BridgeDataContext())
            {
                return db.RelativeSelectAsNamedEntity(null, null, null, null, null, null,
                    null, 
                    (Int32)HttpContext.Current.Session["LangID"], 
                    Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString())).ToList();
            }
        }
        public IEnumerable<Relative> Select()
        {
            using (var db = new BridgeDataContext())
            {
                try
                {
                    var nia = db.RelativeSelect(ID, Name, RUT, BornDate, KinshipID, Status, SolicitorID,
                        Convert.ToInt32(HttpContext.Current.Session["LangID"]), 
                        Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString()));
                    return nia.ToList();
                }
                catch (Exception aa)
                {

                    throw aa;
                }
            }
        }
        public Int32? Save()
        {
            using (var db = new BridgeDataContext())
            {
                return db.RelativeSave(ID, Name, RUT, BornDate, KinshipID, SolicitorID, (Int32)HttpContext.Current.Session["LangID"], Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString()));
            }
        }
        public void Delete()
        {
            using (var db = new BridgeDataContext())
            {
                db.RelativeDelete(ID, (Int32)HttpContext.Current.Session["LangID"], Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString()));
            }
        }
        public IEnumerable<ListedRelative> List()
        {
            using (var db = new BridgeDataContext())
            {
                var aaa = db.RelativeList(ID, Name, RUT, BornDate, KinshipID, SolicitorID, Status, Convert.ToInt32(HttpContext.Current.Session["LangID"]), Convert.ToInt32(HttpContext.Current.Session["UserID"]));
                var dies = aaa.ToList<ListedRelative>();
                return dies;
            }
        }
        public static Int32? SetAsApproved(IList<ListedRelative> e)
        {
            using (var db = new BridgeDataContext())
            {
                return db.RelativeSetAsApproved(Serializer.Serialize(e.Where((x => x.Process == true)).ToList()), (Int32)HttpContext.Current.Session["LangID"], Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString()), Convert.ToInt32(HttpContext.Current.Session["EmployeeID"]));
            }
        }

    }
}
