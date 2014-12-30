using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Common.Logging;


namespace SharpDomain.Utilities
{
    public static class SharpHelpers
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public static void PreLoadDeployedAssemblies()
        {
            Log.Debug("Loading deployed assemblies");
            foreach (var path in GetBinFolders())
            {
                PreLoadAssembliesFromPath(path);
            }
        }
        private static void PreLoadAssembliesFromPath(string path)
        {
            Log.TraceFormat("Loading deployed assemblies from path '{0}'", path);
            try
            {

                FileInfo[] files = null;

                files = new DirectoryInfo(path).GetFiles("*.dll", SearchOption.TopDirectoryOnly);

                AssemblyName name = null;
                string fileName = null;
                foreach (var fi in files)
                {
                    try
                    {
                        fileName = fi.FullName;

                        name = AssemblyName.GetAssemblyName(fileName);

                        if (!AppDomain.CurrentDomain.GetAssemblies().Any(assembly =>
                            AssemblyName.ReferenceMatchesDefinition(name, assembly.GetName())))
                        {
                            Log.TraceFormat("Loading assembly '{0}'", name);

                            Assembly.Load(name);
                        }
                        else
                        {
                            Log.TraceFormat("Assembly already loaded '{0}'", name);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message, ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }
        private static IEnumerable<string> GetBinFolders()
        {
            List<string> toReturn = new List<string>();

            string binDirectory;
            if (GetHttpRuntimeBinDirectory(out binDirectory))
            {
                toReturn.Add(binDirectory);
            }
            else
            {
                toReturn.Add(AppDomain.CurrentDomain.BaseDirectory);
                toReturn.Add(AppDomain.CurrentDomain.RelativeSearchPath);
                toReturn.Add(AppDomain.CurrentDomain.DynamicDirectory);
            }

            return toReturn;
        }

        public static bool GetHttpRuntimeBinDirectory(out string binDirectory)
        {
            binDirectory = null;
            
            var systemWebAsm = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == "System.Web");
            if (systemWebAsm == null) return false;
            
            var httpContextType = systemWebAsm.GetType("System.Web.HttpContext", false);
            if (httpContextType == null) return false;
            var httpContextInstance = httpContextType.InvokeMember("Current", BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Static, null, null, null);
            if (httpContextInstance == null) return false;

            var httpRuntimeType = systemWebAsm.GetType("System.Web.HttpRuntime", false);
            if (httpRuntimeType == null) return false;
            binDirectory = (string) httpRuntimeType.InvokeMember("BinDirectory", BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Static, null, null, null);

            return binDirectory != null && File.Exists(binDirectory);
        }

    }
}
