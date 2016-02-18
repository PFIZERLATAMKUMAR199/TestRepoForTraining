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
{
    [OperationContract]
    public IEnumerable<Language> LanguageGetAll()
    {
        return Language.SelectAll();
    }
    [OperationContract]
    public IEnumerable<Word> LanguageGetWords(Language e)
    {
        HttpContext.Current.Session["LangID"] = e.ID;
        return e.GetWords();
    }
}
