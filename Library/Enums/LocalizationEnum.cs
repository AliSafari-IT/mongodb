using System.Globalization;

namespace Library.Enums
{
    public class LocalizationEnum
    {
        private enum Locale
        {
            EN,
            NL,
            FR,
            JP,
            PR
        }

        private Locale _locale;

        private LocalizationEnum(Locale locale)
        {
            this._locale = locale;
        }

        public override string ToString()
        {
            switch (this._locale)
            {
                case Locale.NL: return "nl";
                case Locale.EN: return "en";
                case Locale.FR: return "fr";
                case Locale.JP: return "jp";
                case Locale.PR: return "pr";
                default:
                    break;
            }

            return "en";
        }

        public static string Dutch => new LocalizationEnum(Locale.NL).ToString();
        public static string English => new LocalizationEnum(Locale.EN).ToString();
        public static string French => new LocalizationEnum(Locale.FR).ToString();
        public static string Japanese => new LocalizationEnum(Locale.JP).ToString();
        public static string Persian => new LocalizationEnum(Locale.PR).ToString();

        public static string FormatIsoToLocale(string iso)
        {
            if (iso == "nl")
            {
                return LocalizationEnum.Dutch;
            }

            return iso.ToLower();
        }

        public static bool IsLocaleSupported(string locale)
        {
            List<string> supported =
            [
                Dutch,
                English,
                French,
                Japanese,
                Persian
            ];

            return supported.Contains(locale);
        }
    }
}
