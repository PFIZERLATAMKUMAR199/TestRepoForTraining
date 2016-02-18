using System;
using System.Web;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Collections.Generic;
using System.Text;
using SEMP.Data;

public partial class ORMService
{//luis - 2012-06-21

    [OperationContract]
    public IEnumerable<NamedEntity> TreatmentGetAll()
    {
    //    CheckAccess("SolicitudePage");
        return Treatment.SelectAll();
    }

    [OperationContract]
    public IEnumerable<Treatment> TreatmentGet(Treatment e)
    {
        CheckAccess("SolicitudePage");
        var eee = e.Select();
        return eee;
    }
    //[OperationContract]
    //public Int32? TreatmentSave(Treatment e)
    //{
    //    CheckAccess("TreatmentPage");
    //    return e.Save();
    //}
    //[OperationContract]
    //public void TreatmentDelete(Treatment e)
    //{
    //    CheckAccess("TreatmentPage");
    //    e.Delete();
    //}

}
