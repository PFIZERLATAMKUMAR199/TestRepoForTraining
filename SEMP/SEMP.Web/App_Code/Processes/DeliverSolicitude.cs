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
{//FRL - 2011-11-14
    [OperationContract]
    public Int32? DeliverSolicitudeDeliver(IList<PendingSolicitude> e)
    {
        CheckAccess("DeliverSolicitudePage");
        return Solicitude.SetAsDelivered(e);
    }
}
