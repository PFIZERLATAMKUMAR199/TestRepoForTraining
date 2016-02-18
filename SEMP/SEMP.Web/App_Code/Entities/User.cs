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
{//luis - 2011-10-24

    [OperationContract]
    public IEnumerable<NamedEntity> UserActiveGetAll()
    {//luis - 2011-12-01
        //CheckAccess("UserPage");
        return User.SelectActiveAll();
    }
    [OperationContract]
    public IEnumerable<NamedEntity> UserGetAll()
    {
  //      CheckAccess("UserPage");
        return User.SelectAll();
    }
    [OperationContract]
    public IEnumerable<User> UserGet(User e)
    {
        CheckAccess("UserPage");
        return e.Select();
    }
    [OperationContract]
    public Int32? UserSave(User e)
    {
        CheckAccess("UserPage");
        return e.Save();
    }
    [OperationContract]
    public Int32? UserDelete(User e)
    {
        CheckAccess("UserPage");
        return e.Delete();
    }
}
