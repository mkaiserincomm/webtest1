using System;
using System.Reflection;

namespace webtest1.Models
{
    public class AboutViewModel
    {
        public string Version { 
            get {
                return Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyVersionAttribute>().Version;
            }
        }
    }
}
