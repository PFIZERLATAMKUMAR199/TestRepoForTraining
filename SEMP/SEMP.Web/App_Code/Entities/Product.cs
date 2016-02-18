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
{//luis - 2011-10-12
    [OperationContract]
    public IEnumerable<NamedEntity> ProductGetActiveOfUserAll(int? nUser, string sName)
    {//luis - 2011-12-21
   //     CheckAccess("ProductPage", "SolicitudePage");
        return Product.SelectActiveOfUserAll(nUser, sName);
    }

    [OperationContract]
    public IEnumerable<NamedEntity> ProductGetActiveAll()
    {//luis - 2011-11-17
    //    CheckAccess("ProductPage");
        return Product.SelectActiveAll();
    }

    [OperationContract]
    public IEnumerable<NamedEntity> ProductGetAll()
    {
    //    CheckAccess("ProductPage");
        return Product.SelectAll();
    }
    [OperationContract]
    public IEnumerable<Product> ProductGet(Product e)
    {
        CheckAccess("ProductPage");
        return e.Select();
    }
    [OperationContract]
    public Int32? ProductSave(Product e)
    {//luis - 2011-11-02
        CheckAccess("ProductPage");
        return e.Save();
    }
    [OperationContract]
    public void ProductDelete(Product e)
    {//luis - 2011-11-02
        CheckAccess("ProductPage");
        e.Delete();
    }

   

}
