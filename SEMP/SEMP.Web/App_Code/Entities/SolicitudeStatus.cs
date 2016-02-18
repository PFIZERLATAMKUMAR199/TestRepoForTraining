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
    public IEnumerable<NamedEntity> SolicitudeStatusGetAll()
    {
   //     CheckAccess("SolicitudeToPrintPage");
        return SolicitudeStatus.SelectAll();
    }
    [OperationContract]
    public IEnumerable<SolicitudeStatus> SolicitudeStatusGet(SolicitudeStatus e)
    {
        CheckAccess("SolicitudeToPrintPage");
        return e.Select();
    }
}
