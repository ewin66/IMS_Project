using System.Globalization;
using System.Threading;


namespace IMS.Resources
{
    public class ResourceHandler
    {
        private const string assemblyRootNamespace = "IMS.Resources";

        public string GetResourceName(string resource)
        {
            return assemblyRootNamespace + "." + resource;
        }
    }
}
