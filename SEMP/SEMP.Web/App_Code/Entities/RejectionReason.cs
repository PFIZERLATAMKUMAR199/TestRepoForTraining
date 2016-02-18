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
{//FRL - 2011-10-24
    [OperationContract]
    public IEnumerable<NamedEntity> RejectionReasonGetAll()
    {
   //     CheckAccess("RejectionReasonPage");
        return RejectionReason.SelectAll();
    }
    [OperationContract]
    public IEnumerable<RejectionReason> RejectionReasonGet(RejectionReason e)
    {
        CheckAccess("RejectionReasonPage");
        return e.Select();
    }
    [OperationContract]
    public Int32? RejectionReasonSave(RejectionReason e)
    {
        CheckAccess("RejectionReasonPage");
        return e.Save();
    }
    [OperationContract]
    public void RejectionReasonDelete(RejectionReason e)
    {
        CheckAccess("RejectionReasonPage");
        e.Delete();
    }
}
