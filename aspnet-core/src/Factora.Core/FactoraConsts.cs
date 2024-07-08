using Factora.Debugging;

namespace Factora
{
    public class FactoraConsts
    {
        public const string LocalizationSourceName = "Factora";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "6a929ea09cd34fc0abbec297156174c0";
    }
}
