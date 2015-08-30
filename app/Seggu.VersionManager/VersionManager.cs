﻿using Newtonsoft.Json;
using Seggu.Data;
using Seggu.VersionManager.Interfaces;
using Seggu.VersionManager.Properties;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Seggu.VersionManager
{
    public class VersionManager : IVersionManager
    {
        private List<Version> versions;
        
        public VersionManager()
        {
            this.versions = GetVersions();
        }

        private List<Version> GetVersions()
        {
            return JsonConvert
                .DeserializeObject<List<Version>>(File.ReadAllText(Settings.Default.VersionsFile))
                .OrderBy(x => x.Id).ToList();
        }

        public void RunAllVersions(Action delegateAction)
        {
            // Validate if the database is created
            if (delegateAction != null)
            {
                delegateAction();
            }
            if (!IsDatabaseCreated())
            {
                RunFirstVersion();
                if (delegateAction != null)
                {
                    delegateAction();
                }
            }

            RunRemainingVersions(delegateAction);
        }

        public void RunAllVersions()
        {
            RunAllVersions(null);
        }

        private void RunRemainingVersions(Action action)
        {
            if (this.versions.Count > 1)
            {
                for (int i = 1; i < this.versions.Count; i++)
                {
                    var version = this.versions[i];
                    var versioningService = GetVersioningService(version.Name);
                    versioningService.ApplyVersion();

                    if (action != null)
                    {
                        action();
                    }
                }
            }
        }

        private void RunFirstVersion()
        {
            var firstVersion = this.versions.FirstOrDefault();
            if (firstVersion == null)
            {
                throw new Exception("El archivo de versiones no existe.");
            }
            else
            {
                var versioningService = GetVersioningService(firstVersion.Name);
                versioningService.ApplyVersion();
            }
        }

        private IVersioningService GetVersioningService(string versionName)
        {
            var fullTypeName = Settings.Default.VersioningServicesNamespace + '.' + versionName;
            
            // Initialize Versioning Service
            var versioningServiceType = Type.GetType(fullTypeName);
            var versioningService = (IVersioningService)Activator.CreateInstance(versioningServiceType);
            return versioningService;
        }

        private bool IsDatabaseCreated()
        {
            return File.Exists(Settings.Default.DatabaseFileName);
        }

        public int GetVersionCountToRun()
        {
            return this.versions.Count;
        }
    }
}
