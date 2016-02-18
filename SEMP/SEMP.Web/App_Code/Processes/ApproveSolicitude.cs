﻿using System;
using System.Web;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Collections.Generic;
using System.Text;
using SEMP.Data;

public partial class ORMService
{//FRL - 2011-11-01
    [OperationContract]
    public Int32? ApproveSolicitudeApprove(IList<PendingSolicitude> e)
    {
        CheckAccess("ApproveSolicitudePage");
        return Solicitude.SetAsApproved(e);
    }
}
