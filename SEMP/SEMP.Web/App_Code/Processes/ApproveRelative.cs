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
{//FRL - 2012-06-15
    [OperationContract]
    public Int32? ApproveRelativeProcess(IList<ListedRelative> e)
    {
        CheckAccess("ApproveRelativePage");
        return Relative.SetAsApproved(e);
    }
}
