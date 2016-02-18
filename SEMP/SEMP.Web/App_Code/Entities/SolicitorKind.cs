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
    public IEnumerable<NamedEntity> SolicitorKindGetAll()
    {
        CheckAccess("SolicitorKindPage", "ProductAviableByKindPage");
        return SolicitorKind.SelectAll();
    }
    [OperationContract]
    public IEnumerable<SolicitorKind> SolicitorKindGet(SolicitorKind e)
    {
        CheckAccess("SolicitorKindPage");
        return e.Select();
    }
    [OperationContract]
    public Int32? SolicitorKindSave(SolicitorKind e)
    {
        CheckAccess("SolicitorKindPage");
        return e.Save();
    }
    [OperationContract]
    public void SolicitorKindDelete(SolicitorKind e)
    {//luis - 2011-11-21
        CheckAccess("SolicitorKindPage");
        e.Delete();
    }

    [OperationContract]
    public IEnumerable<ProductSolicitorKind> SolicitorKindGetChildren(SolicitorKind e)
    {//FRL - 2011-11-22
        CheckAccess("SolicitorKindPage");
        return e.GetChildren();
    }

}
