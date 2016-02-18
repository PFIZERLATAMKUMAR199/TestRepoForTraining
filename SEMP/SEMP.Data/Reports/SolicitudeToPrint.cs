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
{//luis - 2011-11-09

    public class SolicitudeToPrint : SolicitudePrint
    {
        public DateTime? dtBegin { get; set; }
        public DateTime? dtEnd { get; set; }
        public int? nSolicitudeBegin { get; set; }
        public int? nSolicitudeEnd { get; set; }
        public String sSolicitor { get; set; }
        public String sNroDoc { get; set; }
        public int? nSolicitudeStatusID { get; set; }

        public override List<SolicitudeToPrintResult> ToList()
        {
            using (var db = new BridgeDataContext())
            {
                return db.SolicitudeToPrint(dtBegin, dtEnd, nSolicitudeBegin, nSolicitudeEnd, sSolicitor, sNroDoc, nSolicitudeStatusID).ToList();
            }
        }
    }
}

