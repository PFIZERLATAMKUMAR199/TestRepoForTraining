using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Data.Linq;

namespace SEMP.Data
{// FRL - 2011-11-01

    partial class ModifySolicitude 
    {
        public Int32? ModifiedSave(int SolicitudeID,int flag)
        {
            using (var db = new BridgeDataContext())
            {//luis - 2012-07-06
                return db.SolicitudeModifySave(SolicitudeID, flag, SolicitorID, RelativeID, TreatmentID, BeginDate, EndDate, Observations, LocationID,
                    Serializer.Serialize(Detail),
                    (Int32)HttpContext.Current.Session["LangID"],
                    Convert.ToInt64(HttpContext.Current.Session["EmployeeID"].ToString()));
            }
        }
        public List<SolicitudeItem> Detail { get; set; }
    }
    
}
