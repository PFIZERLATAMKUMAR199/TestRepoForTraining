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
{//luis - 2011-10-11

    [OperationContract]
    public string ProductStockReport(ProductStock e)
    {
        CheckAccess("ProductStockPage");
        return e.Report();
    }
}