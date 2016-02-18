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
{//luis - 2012-06-18

    [OperationContract]
    public string RequestsReport(Requests e)
    {
        CheckAccess("RequestsPage");
        return e.Report();
    }
}