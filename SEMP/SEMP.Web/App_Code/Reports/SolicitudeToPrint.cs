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
{//luis - 2011-11-09

    [OperationContract]
    public string SolicitudeToPrintReport(SolicitudeToPrint e)
    {
        CheckAccess("SolicitudeToPrintPage");
        return e.Report();
    }
}