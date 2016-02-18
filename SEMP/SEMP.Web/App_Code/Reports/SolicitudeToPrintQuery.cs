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
{//luis - 2011-12-09

    [OperationContract]
    public string SolicitudeToPrintQueryReport(SolicitudeToPrintQuery e)
    {
        CheckAccess("SolicitudeToPrintQueryPage");
        return e.Report();
    }
}