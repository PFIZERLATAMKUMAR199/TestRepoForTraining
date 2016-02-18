using System;
using System.Web;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using System.Collections.Generic;
using System.Text;
using SEMP.Data;
using System.Security.Principal;
using System.Configuration;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public partial class ORMService
{
    private void CheckAccess(params String[] Menu)
    {
        var UserMenu = ((List<String>)HttpContext.Current.Session["UserMenu"]);
        if (!(UserMenu == null || Menu.Any(x => UserMenu.Contains(x))))
            throw new Exception("InvalidAccessException");
    }
    [OperationContract]
    public String GetQVlink()
    {
        CheckAccess("TestFFUpdaterPage", "TestFFGaugePage");
        return ConfigurationSettings.AppSettings["QVLINK"].ToString();
    }
    [OperationContract]
    public String ClearSession()
    {
        HttpContext.Current.Session.Clear();
        return HttpContext.Current.User.Identity.Name;
    }
    [OperationContract]
    public List<String> GetUserMenu(String Domain, String User, String Pass)
    {
        // TODO : Uncomment while deploying this code
        //CARPNTUserCheck.NTUserCheckClass userChecker = new CARPNTUserCheck.NTUserCheckClass();
        //Boolean isValid =  userChecker.UserCheck(ref User, ref Domain, ref Pass); 
        //userChecker = null;
        bool isValid = true;
        User = "PATELR50"; //luis - usuario que loguea
        if (!isValid) throw new Exception("InvalidUserException"); 
        long? UserID;
        try
        {
            UserID = ModSeg.GetUserID(User);
        }
        catch
        {
            UserID = ModSeg.GetUserID("USERGEN"); 
        }
        var EmployeeID = ModSeg.GetEmployeeID(User);
        HttpContext.Current.Session["EmployeeID"] = EmployeeID;
        HttpContext.Current.Session["LangID"] = 1;
        long? ProfileID;
        try
        {
            ProfileID = ModSeg.GetProfileID(User);
        }
        catch
        {
            ProfileID = ModSeg.GetGenericProfileID(User);
        }
        var UserName = ModSeg.GetUserName(User);
        var UserMenu = ModSeg.GetUserMenu(ProfileID);
        HttpContext.Current.Session["ProfileID"] = ProfileID;
        HttpContext.Current.Session["UserID"] = UserID;
        //HttpContext.Current.Session["EmployeeID"] = EmployeeID;
        HttpContext.Current.Session["NetUser"] = User;
        HttpContext.Current.Session["UserName"] = UserName;
        HttpContext.Current.Session["UserMenu"] = UserMenu;
        //HttpContext.Current.Session["LangID"] = 1;
        // -->>
        var result = new List<String>(UserMenu);
        result.Insert(0, UserName);
        return result;
    }
}
/*
[OperationBehavior(Impersonation=ImpersonationOption.Required)]
WindowsIdentity.GetCurrent().Name
ServiceSecurityContext.Current.WindowsIdentity.ImpersonationLevel
HttpContext.Current.User.Identity.Name
System.Threading.Thread.CurrentPrincipal.Identity.Name
ServiceSecurityContext.Current.PrimaryIdentity.Name
using (ServiceSecurityContext.Current.WindowsIdentity.Impersonate())
*/
