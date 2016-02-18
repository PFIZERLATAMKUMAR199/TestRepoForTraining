using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEMP.Data
{//luis - 2011-11-02
    partial class ProductOrigin
    {
        public static IEnumerable<NamedEntityS> SelectAll()
        {
            using (var db = new BridgeDataContext())
            {
                db.CommandTimeout = db.CommandTimeout * 3;
                return db.ProductOriginSelectAsNamedEntityS(null, null, 
                    (Int32)HttpContext.Current.Session["LangID"], 
                    HttpContext.Current.Session["NetUser"].ToString(), 
                    Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString())).ToList();
            }
        }
    }
}
