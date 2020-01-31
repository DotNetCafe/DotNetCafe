using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DotNetCafe")]
[assembly: InternalsVisibleTo("DotNetCafe.Test")]

namespace DotNetCafe.Globalization
{
    internal static class SR
    {
        private const string BaseName = "DotNetCafe.Globalization.Resources.Strings";

        private static ResourceManager? strings;

        private static ResourceManager Resources =>
            strings ??= new ResourceManager(BaseName, 
                Assembly.GetExecutingAssembly());

        public static string ArgumentException_InvalidCnpjNumber =>
            GetString(nameof(ArgumentException_InvalidCnpjNumber));
        
        public static string ArgumentException_InvalidCpfNumber =>
            GetString(nameof(ArgumentException_InvalidCpfNumber));

        public static string FormatException_InvalidFormat =>
            GetString(nameof(FormatException_InvalidFormat));

        public static string FormatException_InvalidCnpjFormat =>
            GetString(nameof(FormatException_InvalidCnpjFormat));
        
        public static string FormatException_InvalidCpfFormat =>
            GetString(nameof(FormatException_InvalidCpfFormat));

        private static string GetString(string name) =>
            Resources.GetString(name);
    }
}
