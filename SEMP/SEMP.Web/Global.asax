<%@ Application Language="C#" %>
<script runat="server">
    void Application_Error(Object Sender, EventArgs Args) { }
    void Application_Start(Object Sender, EventArgs Args) { }
    void Session_End(Object Sender, EventArgs Args) { Session.RemoveAll();}
    void Application_End(Object Sender, EventArgs Args) { Application.RemoveAll(); }
    void Session_Start(Object Sender, EventArgs Args)
    {
        
        HttpContext.Current.Session["UserID"] = 0;
        HttpContext.Current.Session["EmployeeID"] = 0;
        HttpContext.Current.Session["ProfileID"] = 0;
        
        try {Application["ConnectionString"] = ConfigurationSettings.AppSettings["ConnectionString"].ToString();}  catch {}
        try {Application["ModSegConnectionString"] = ConfigurationSettings.AppSettings["ModSegConnectionString"].ToString();} catch {}   
        
        if (Application["ModSegConnectionString"] == null)
        {
            var notFound = "ModSegNotFoundException";
            Net2003RPCClient.Facade _facade = new Net2003RPCClient.Facade();
            String _ipClient = ConfigurationSettings.AppSettings["IPCLIENT"].ToString();
            String _appCode = ConfigurationSettings.AppSettings["MODULE"].ToString();
            String _connString = String.Empty;
            String _dbServer = String.Empty;
            String _dbName = String.Empty;
            String _dbUser = String.Empty;
            String _dbPass = String.Empty;
            String[] result = _facade.FindDataFromWS(_appCode, String.Empty, _ipClient);
            if ((result[(Int32)Net2003RPCClient.Facade.Resultados.ConnectionString] != null)
            && (result[(Int32)Net2003RPCClient.Facade.Resultados.ConnectionString].ToString() != String.Empty))
            {
                _connString = result[(Int32)Net2003RPCClient.Facade.Resultados.ConnectionString].ToString().Replace("\0", "");
                _dbServer = result[(Int32)Net2003RPCClient.Facade.Resultados.ServerName].ToString().Replace("\0", "");
                _dbName = result[(Int32)Net2003RPCClient.Facade.Resultados.DataBaseName].ToString().Replace("\0", "");
                _dbUser = result[(Int32)Net2003RPCClient.Facade.Resultados.UserName].ToString().Replace("\0", "");
                _dbPass = result[(Int32)Net2003RPCClient.Facade.Resultados.Password].ToString().Replace("\0", "");
            }
            else throw new Exception(notFound);
            Application["ConnectionString"] = _connString;
            Application["dbServer"] = _dbServer;
            Application["dbName"] = _dbName;
            Application["dbUser"] = _dbUser;
            Application["dbPass"] = _dbPass;
            _appCode = ConfigurationSettings.AppSettings["MODSEGMODULE"].ToString();
            _connString = String.Empty;
            result = _facade.FindDataFromWS(_appCode, String.Empty, _ipClient);
            if ((result[(Int32)Net2003RPCClient.Facade.Resultados.ConnectionString] != null)
            && (result[(Int32)Net2003RPCClient.Facade.Resultados.ConnectionString].ToString() != String.Empty))
                _connString = result[(Int32)Net2003RPCClient.Facade.Resultados.ConnectionString].ToString().Replace("\0", "");
            else throw new Exception(notFound);
            Application["ModSegConnectionString"] = _connString;
            _facade = null;
        }
    }
</script>
