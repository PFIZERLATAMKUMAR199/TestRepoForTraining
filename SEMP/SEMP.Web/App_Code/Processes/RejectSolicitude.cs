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
{//FRL - 2012-06-28
    [OperationContract]
    public Int32? RejectSolicitudeReject(IList<SolicitudeItem> e, int nRejectionReasonID, String sNullificationObs)
    {
        CheckAccess("RejectSolicitudePage");
        return Solicitude.Reject(e, nRejectionReasonID, sNullificationObs);
    }
}
