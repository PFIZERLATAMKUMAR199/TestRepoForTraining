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
{//FRL - 2012-06-12

    [OperationContract]
    public IEnumerable<NamedEntity> RelativeGetActiveOfUserAll(int? nUser)
    {//luis - 2012-06-19
  //      CheckAccess("RelativePage", "SolicitudePage");
        return Relative.SelectActiveOfUserAll(nUser);
    }
    [OperationContract]
    public IEnumerable<NamedEntity> RelativeGetAll()
    {
 //       CheckAccess("RelativePage");
        return Relative.SelectAll();
    }

    [OperationContract]
    public IEnumerable<Relative> RelativeGet(Relative e)
    {
        CheckAccess("RelativePage");
        var eee = e.Select();
        return eee;
    }
    [OperationContract]
    public Int32? RelativeSave(Relative e)
    {
        CheckAccess("RelativePage");
        return e.Save();
    }
    [OperationContract]
    public void RelativeDelete(Relative e)
    {
        CheckAccess("RelativePage");
        e.Delete();
    }
    [OperationContract]
    public IEnumerable<ListedRelative> RelativeGetPending()
    {
        // FRL - 2012-06-15
        var e = new Relative() { Status = "P" };
        CheckAccess("ApproveRelativePage");
        return e.List();
    }

}
