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
{//luis - 2012-07-05

    [OperationContract]
    public IEnumerable<NamedEntity> LocationGetAll()
    {
        //CheckAccess("LocationPage");
        return Location.SelectAll();
    }
    [OperationContract]
    public IEnumerable<Location> LocationGet(Location e)
    {
        CheckAccess("LocationPage");
        return e.Select();
    }
    [OperationContract]
    public Int32? LocationSave(Location e)
    {
        CheckAccess("LocationPage");
        return e.Save();
    }
    [OperationContract]
    public void LocationDelete(Location e)
    {
        CheckAccess("LocationPage");
        e.Delete();
    }
}
