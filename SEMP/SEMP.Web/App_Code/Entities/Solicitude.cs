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
{// FRL - 2011-11-01
    [OperationContract]
    public IEnumerable<NamedEntity> SolicitudeGetAll()
    {
    //    CheckAccess("SolicitudePage");
        return Solicitude.SelectAll();
    }
    [OperationContract]
    public IEnumerable<Solicitude> SolicitudeGet(Solicitude e)
    {
        // FRL - 2011-11-10
        CheckAccess("SolicitudePage", "ProcessSolicitudePage");
        return e.Select();
    }
    [OperationContract]
    public IEnumerable<Solicitude> SolicitudeGetExpired()
    {// FRL - 2011-11-08
        CheckAccess("RejectSolicitudePage");
        return Solicitude.GetExpired();
    }

    [OperationContract]
    public IEnumerable<Solicitude> SolicitudeGetNullable(int? nStatusID, DateTime? dtBegin, DateTime? dtEnd, int? nSolicitorUserID)
    {// FRL - 2011-11-18
        CheckAccess("RejectSolicitudePage");
        return Solicitude.GetNullable(nStatusID, dtBegin, dtEnd, nSolicitorUserID);
    }

    [OperationContract]
    public IEnumerable<SolicitudeItem> SolicitudeGetLastItems(Solicitude e)
    {// FRL - 2011-11-21
        CheckAccess("SolicitudePage");
        return e.GetLastItems();
    }

    [OperationContract]
    public int? SolicitudeSave(Solicitude e)
    {
        // FRL - 2011-11-21
        CheckAccess("SolicitudePage");
        return e.Save();
        
    }
    
    //syed 28th october
    [OperationContract]
    public int? SolicitudeModifySave(ModifySolicitude e, int SolicitudeID,int flag)
    {
        // FRL - 2011-11-21
        CheckAccess("ModifySolicitudePage");
        return e.ModifiedSave(SolicitudeID,flag);
        
    }


    [OperationContract]
    public IEnumerable<Solicitude> SolicitudeGetToDeliver()
    {
        // Neha 22-10-2013
        //Changed again -28Oct-Removing Dispatch
        var e = new Solicitude() { StatusID = 3 };
        CheckAccess("SolicitudePage", "DeliverSolicitudePage");
        List<Solicitude> test =  e.Select().ToList();

        return test;
    }

    //Neha 22-10-2013
    //Commented as dispatch functionality removed
    //[OperationContract]
    //public IEnumerable<Solicitude> SolicitudeGetToDispatch()
    //{
    //    // FRL - 2011-12-06
    //    var e = new Solicitude() { StatusID = 3 };
    //    CheckAccess("SolicitudePage", "DispatchSolicitudePage");
    //    return e.Select();
    //}
    [OperationContract]
    public IEnumerable<SolicitudeProductsOfUserResult> SolicitudeProductsOfUser(int UserID)
    {
        // FRL - 2012-01-04
        return Solicitude.SolicitudeProductsOfUser(UserID);
    }
    //SYED 23rd oct 2013 userid is solicitud ID
    [OperationContract]
    public IEnumerable<SolicitudeProductsOfUserModifyResult> SolicitudeProductsOfUserModify(int UserID)
    {
        // FRL - 2012-01-04
        return Solicitude.SolicitudeProductsOfUserModify(UserID);
    }
    //syed 25 oct 2013
    [OperationContract]
    public IEnumerable<SolicitudeFieldsToModifyResult> SolicitudeFieldsToModify(int SolicitorID)
    {
        // FRL - 2012-01-04
        return Solicitude.SolicitudeFieldsToModify(SolicitorID);
    }
    [OperationContract]
    public IEnumerable<SolicitudeItem> SolicitudeGetItems(Solicitude e)
    {// FRL - 2012-06-25
        CheckAccess("SolicitudePage");
        return e.GetItems();
    }

    //syed 21st syed

    [OperationContract]
    public IEnumerable<SolicitudeItem> SolicitudeGetItemsModify(Solicitude e)
    {// FRL - 2012-06-25
        CheckAccess("ModifySolicitudePage");
        return e.GetItemsModify();
    }

    [OperationContract]
    public IEnumerable<Solicitude> SolicitudeGetToApprove()
    {
        // FRL - 2012-06-25
        var e = new Solicitude() { StatusID = 1 };
        CheckAccess("ApproveSolicitudePage");
        return e.Select();
    }

    //syed
    [OperationContract]
    public IEnumerable<Solicitude> SolicitudeGetToModify()
    {
        // FRL - 2012-06-25
        var e = new Solicitude() { StatusID = 2 };
        CheckAccess("ModifySolicitudePage");
        return e.Select_Modify();
    }
    

    [OperationContract]
    public IEnumerable<Solicitude> SolicitudeGetToProcess()
    {
        // FRL - 2012-06-26
        var e = new Solicitude() { StatusID = 2 };
        CheckAccess("SolicitudePage", "ProcessSolicitudePage");
        return e.Select();
    }

    [OperationContract]
    public IEnumerable<SolicitudeItem> SolicitudeGetRejectable(DateTime? dtBegin, DateTime? dtEnd, int? nSolicitorUserID)
    {// FRL - 2012-06-29
        CheckAccess("RejectSolicitudePage");
        return Solicitude.GetRejectable(dtBegin, dtEnd, nSolicitorUserID);
    }

}
