using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Eternity.Core.Extensions;
using Eternity.Core.Utilities.Helpers;

namespace Eternity.Core.Driver
{
    public class DriverService
    {
        private Type DriverType = typeof (IEternityDriver);

        /// <summary>
        /// Adds a driver dll
        /// </summary>
        /// <param name="driverFilePath"></param>
        /// <returns>Error message</returns>
        public string AddDriver(string driverFilePath)
        {
            // 0. Clean the input
            driverFilePath = Path.GetFullPath(driverFilePath);
            var fileName = driverFilePath.Split('\\').Last();
            var fileType = driverFilePath.Split('.').Last();

            // 1. Locate driver
            if (driverFilePath.IsBlank() || !File.Exists(driverFilePath))
                return $"Driver at path `{driverFilePath}` could not be found";

            // 2. Validate driver
            if (fileType != "dll")
                return $"Unrecognised file type `{fileType}`. Please select a .dll";

            // Load the driver into memory now, if any invalid/malicious code causes runtime 
            // exceptions it is better to fail during the registration than at app start
            var assembly = Assembly.Load(File.ReadAllBytes(driverFilePath));

            var driverTypes = from type in assembly.GetTypes()
                                where type.GetInterfaces().Any(i => i.FullName == DriverType.FullName) 
                                select type;

            if (driverTypes.None())
                return $"Could not locate any implementations of `{DriverType.Name}` in {assembly.FullName}`";

            LoadDriver(driverFilePath, assembly);

            // 3. Save a copy of the assembly
            var appDataPath = AppDataHelper.FilePath("", "drivers");
            File.Copy(driverFilePath, Path.Combine(appDataPath, fileName), true);
            
            return null;
        }

        public IEnumerable<Assembly> GetDrivers()
        {
            var directoryPath = AppDataHelper.FilePath(directory: "drivers");
            var fileNames = Directory.GetFiles(directoryPath)
                            .Select(f=> f.PathFileName());

            return AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a=> fileNames.Any(f=> a.FullName.StartsWith(f + ",")));
        }

        public void LoadDriver(string assemblyPath, Assembly assembly = null)
        {
            if(assembly == null)
                assembly = Assembly.Load(File.ReadAllBytes(assemblyPath));

            // Exclude version no. details
            var assemblyName = assembly.FullName.Split(',').First();

            // Clear the existing assembly (if exists)
            var existingAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                .SingleOrDefault(a => a.FullName.StartsWith(assemblyName));

            if (existingAssemblies != null)
            {
                existingAssemblies = null;
            }
            else
            {
                // Copy the assembly into memory to prevent file-locks
                var assemblyBytes = File.ReadAllBytes(assemblyPath);
                AppDomain.CurrentDomain.Load(assemblyBytes);
            }

        }

        public void RemoveDriver(string assemblyName)
        {
            // 1. Remove from AppDomain
            var existingAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                .SingleOrDefault(a => a.FullName.StartsWith(assemblyName));

            if (existingAssemblies != null)
            {
                existingAssemblies = null;
            }

            // 2. Remove from disk
            var appDataPath = AppDataHelper.FilePath("", "drivers");

            // TODO this will not match assemblies with different names to the .dll
            File.Delete(Path.Combine(appDataPath, assemblyName + ".dll"));
        }
    }
}
