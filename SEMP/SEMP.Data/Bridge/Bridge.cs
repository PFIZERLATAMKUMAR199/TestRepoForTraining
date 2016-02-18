//luis - 2011-09-28


namespace SEMP.Data
{
    using System;
    using System.Linq;
    using System.Configuration;
    using System.Web;
    using System.Text;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Collections;
    using System.Collections.Generic;
    public partial class BridgeDataContext
    {
        partial void OnCreated()
        {
            this.Connection.ConnectionString = HttpContext.Current.Application["ConnectionString"].ToString();
        }
    }
}

