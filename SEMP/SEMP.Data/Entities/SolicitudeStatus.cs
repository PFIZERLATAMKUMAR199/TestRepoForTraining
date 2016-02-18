using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Data.Linq;

namespace SEMP.Data
{//FRL - 2011-11-11

    partial class SolicitudeStatus
    {
        public static IEnumerable<NamedEntity> SelectAll()
        {
            using (var db = new BridgeDataContext())
            {
                return db.SolicitudeStatusSelectAsNamedEntity(null, null).ToList();
            }
        }
        public IEnumerable<SolicitudeStatus> Select()
        {
            using (var db = new BridgeDataContext())
            {
                return db.SolicitudeStatusSelect(ID, Name).ToList();
            }
        }
    }
}
