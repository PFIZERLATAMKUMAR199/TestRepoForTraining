using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEMP.Data
{
    partial class Language
    {
        public static IEnumerable<Language> SelectAll()
        {
            using (var db = new BridgeDataContext())
            {
                return db.LanguageSelect(null).ToList();
            }
        }
        public IEnumerable<Word> GetWords()
        {
            using (var db = new BridgeDataContext())
            {
                return db.WordSelect(ID).ToList();
            }
        }
    }
}
