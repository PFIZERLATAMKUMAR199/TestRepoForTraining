using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DocumentFormat.OpenXml.Packaging;
using Ap = DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using Wp = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using Pic = DocumentFormat.OpenXml.Drawing.Pictures;
using V = DocumentFormat.OpenXml.Vml;
using Ovml = DocumentFormat.OpenXml.Vml.Office;
using M = DocumentFormat.OpenXml.Math;

using System.Web;
using System.Configuration;

namespace SEMP.Data
{//luis - 2011-12-19

    public class SolicitudeToPrintQuery : SolicitudePrint
    {
        public List<PendingSolicitude> Detail { get; set; } 

        public override List<SolicitudeToPrintResult> ToList()
        {
            using (var db = new BridgeDataContext())
            {
                return db.SolicitudeToPrintQuery(Serializer.Serialize(Detail)).ToList();
            }
        }
    }
}

