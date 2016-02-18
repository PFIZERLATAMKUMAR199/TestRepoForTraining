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
    public IEnumerable<NamedEntity> KinshipGetAll()
    {
//        CheckAccess("KinshipPage");
        return Kinship.SelectAll();
    }

    [OperationContract]
    public IEnumerable<Kinship> KinshipGet(Kinship e)
    {
        CheckAccess("KinshipPage");
        return e.Select();
    }
    [OperationContract]
    public Int32? KinshipSave(Kinship e)
    {
        CheckAccess("KinshipPage");
        return e.Save();
    }
    [OperationContract]
    public void KinshipDelete(Kinship e)
    {
        CheckAccess("KinshipPage");
        e.Delete();
    }

    [OperationContract]
    public IEnumerable<NamedEntity> KinshipKindGetAll()
    {
        CheckAccess("KinshipPage");
        return Kinship.GetKinds();
    }

}
