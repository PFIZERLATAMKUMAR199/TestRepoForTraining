using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Data.Linq;

namespace SEMP.Data
{// FRL - 2011-11-01

    partial class Solicitude
    {
        public static IEnumerable<NamedEntity> SelectAll()
        {
            using (var db = new BridgeDataContext())
            {//luis - 2012-06-21
                int nUser = 0;
                if (HttpContext.Current.Session["UserID"] != null) nUser = Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString());
                return db.SolicitudeSelectAsNamedEntity(null, null, null, null, null, null, null, null, nUser).ToList();
            }
        }
        public IEnumerable<Solicitude> Select()
        {
            using (var db = new BridgeDataContext())
            {
                int nUser = 0;
                if (HttpContext.Current.Session["UserID"] != null) nUser = Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString());
                var result = db.SolicitudeSelect(ID, Name, StatusID, TreatmentID, BeginDate, EndDate, ReceptionDate, SolicitorID, nUser);
                return result.ToList();
            }
        }
        //syed
        public IEnumerable<Solicitude> Select_Modify()
        {
            using (var db = new BridgeDataContext())
            {
                
                int nUser = 0;
                //string nUser = null;
                  
               //if (HttpContext.Current.Session["UserID"] != null) nUser = Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString());
                if (HttpContext.Current.Session["EmployeeID"] != null) nUser = Convert.ToInt32(HttpContext.Current.Session["EmployeeID"].ToString());
                //if (HttpContext.Current.Session["UserName"] != null) nUser = (HttpContext.Current.Session["UserName"].ToString());
                var result = db.ModifySolicitudeSelect(ID, Name, StatusID, TreatmentID, BeginDate, EndDate, ReceptionDate, SolicitorID, nUser);
                return result.ToList();
            }
        }
        

        public static Int32? SetAsProcessed(IList<PendingSolicitude> e)
        {
            using (var db = new BridgeDataContext())
            {//luis - 2012-07-02
                return db.SolicitudeSetAsProcessed(
                    Serializer.Serialize(e.ToList().FindAll(x => x.Process == true)),
                    Convert.ToInt64(HttpContext.Current.Session["EmployeeID"].ToString()),
                    Convert.ToInt64(HttpContext.Current.Session["UserID"].ToString()),
                    (Int32)HttpContext.Current.Session["LangID"]);
            }
        }
        
        public static Int32? Disassemble(IList<PendingSolicitude> e)
        {// FRL - 2011-11-08
            using (var db = new BridgeDataContext())
            {
                return db.SolicitudeDisassemble(HttpContext.Current.Session["EmployeeID"].ToString(), Serializer.Serialize(e.ToList().FindAll(x => x.Process == true)), (Int32)HttpContext.Current.Session["LangID"]);
            }
        }
        public static IEnumerable<Solicitude> GetExpired()
        {//luis - 2012-06-11
            using (var db = new BridgeDataContext())
            {
                int nUser = 0;
                if (HttpContext.Current.Session["UserID"] != null) nUser = Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString());
                return db.SolicitudeGetExpired(nUser).ToList();
            }
        }
        
        // <<-- FRL - 2012-06-28
        //public static Int32? Nullify(IList<PendingSolicitude> e, int nNullificationReasonID, String sNullificationObs)
        //{
        //    using (var db = new BridgeDataContext())
        //    {
        //        return db.SolicitudeNullify(HttpContext.Current.Session["EmployeeID"].ToString(), Serializer.Serialize(e.ToList().FindAll(x => x.Process == true)), nNullificationReasonID, sNullificationObs, (Int32)HttpContext.Current.Session["LangID"]);
        //    }
        //}
        // -->>
        public static IEnumerable<Solicitude> GetNullable(int? nStatusID, DateTime? dtBegin, DateTime? dtEnd, int? nSolicitorUserID)
        {// FRL - 2012-06-18
            using (var db = new BridgeDataContext())
            {
                var reti = db.SolicitudeGetNullable(nStatusID, dtBegin, dtEnd, nSolicitorUserID, Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString())).ToList();
                return reti;
            }
        }

        public static Int32? SetAsDelivered(IList<PendingSolicitude> e)
        {// FRL - 2011-11-14
            using (var db = new BridgeDataContext())
            {
                return db.SolicitudeSetAsDelivered(HttpContext.Current.Session["EmployeeID"].ToString(), Serializer.Serialize(e.ToList().FindAll(x => x.Process == true)), (Int32)HttpContext.Current.Session["LangID"]);
            }
        }

        public List<SolicitudeItem> Detail { get; set; }

        public IEnumerable<SolicitudeItem> GetLastItems()
        {
            using (var db = new BridgeDataContext())
            {
                return db.SolicitudeLastItems(SolicitorID).ToList();
            }
        }

        public Int32? Save()
        {
            using (var db = new BridgeDataContext())
            {//luis - 2012-07-06
                return db.SolicitudeSave(SolicitorID, RelativeID, TreatmentID, BeginDate, EndDate, Observations, LocationID,
                    Serializer.Serialize(Detail),
                    (Int32)HttpContext.Current.Session["LangID"],
                    Convert.ToInt64(HttpContext.Current.Session["EmployeeID"].ToString()));
            }
        }
        
        public static IEnumerable<SolicitudeProductsOfUserResult> SolicitudeProductsOfUser(int UserID)
        {
            using (var db = new BridgeDataContext())
            {
                return db.SolicitudeProductsOfUser(UserID).ToList();
            }
           
        }
        //syed 25th october

        public static IEnumerable<SolicitudeFieldsToModifyResult> SolicitudeFieldsToModify(int SolicitorID)
        {
            using (var db = new BridgeDataContext())
            {
                return db.SolicitudeFieldsToModify(SolicitorID).ToList();
            }

        }

        //syed

        public static IEnumerable<SolicitudeProductsOfUserModifyResult> SolicitudeProductsOfUserModify(int UserID)
        {
            using (var db = new BridgeDataContext())
            {
                return db.SolicitudeProductsOfUserModify(UserID).ToList(); 
            }

        }


        public IEnumerable<SolicitudeItem> GetItems()
        {//FRL - 2012-06-25
            using (var db = new BridgeDataContext())
            {
                return db.SolicitudeItemsSelect(ID,
                    Convert.ToInt32(HttpContext.Current.Session["LangID"]),
                    Convert.ToInt32(HttpContext.Current.Session["UserID"])).ToList();
            }
        }

        //syed 21st october

        public IEnumerable<SolicitudeItem> GetItemsModify()
        {//FRL - 2012-06-25
            using (var db = new BridgeDataContext())
            {
                return db.SolicitudeItemsSelect(ID,
                    Convert.ToInt32(HttpContext.Current.Session["LangID"]),
                    Convert.ToInt32(HttpContext.Current.Session["UserID"])).ToList();
            }
        }
        //end code
        public static Int32? SetAsApproved(IList<PendingSolicitude> e)
        {//FRL - 2012-06-25
            using (var db = new BridgeDataContext())
            {
                return db.SolicitudeSetAsApproved(Serializer.Serialize(e.ToList().FindAll(x => x.Process == true)), (int?)HttpContext.Current.Session["LangID"], (long?)HttpContext.Current.Session["UserID"]);
            }
        }


        public static Int32? Reject(IList<SolicitudeItem> e, int nNullificationReasonID, String sNullificationObs)
        {// FRL - 2012-06-28
            using (var db = new BridgeDataContext())
            {
                return db.SolicitudeReject(Serializer.Serialize(e.ToList().FindAll(x => x.Process == true)), nNullificationReasonID, sNullificationObs, (Int32)HttpContext.Current.Session["LangID"], (long?)HttpContext.Current.Session["UserID"]);
            }
        }

        public static IEnumerable<SolicitudeItem> GetRejectable(DateTime? dtBegin, DateTime? dtEnd, int? nSolicitorUserID)
        {// FRL - 2012-06-29
            using (var db = new BridgeDataContext())
            {
                var reti = db.SolicitudeGetRejectable(dtBegin, dtEnd, nSolicitorUserID, (Int32)HttpContext.Current.Session["LangID"], (long?)HttpContext.Current.Session["UserID"]).ToList();
                return reti;
            }
        }

    }
    public partial class SolicitudeItem { public bool Process { get; set; } }
}
