namespace SEMP.Controls
{
    public class LanguageDictionary : ObservableDictionary
    {
        private LanguageDictionary() { }
        static LanguageDictionary() { Current = new LanguageDictionary(); }
        public static LanguageDictionary Current { get; private set; }
    }
}
