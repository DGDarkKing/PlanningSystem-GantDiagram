using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PlaningSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            CreatePluginPath();
        }

        /// <summary>
        /// Create plugins' directories
        /// </summary>
        /// <remarks>The "./plugins" directory is the main directrory where plugins can be placed 
        /// (its located in the directory with executable file [.exe])</remarks>
        /// <returns>.plugins/solver - planner plugins</returns>
        /// <returns>.plugins/load - file loader plugins (Plugins can download and upload)</returns>
        /// <returns>.plugins/load/download - file download plugins</returns>
        /// <returns>.plugins/load/upload - file upload plugins</returns>
        private void CreatePluginPath()
        {
            Directory.CreateDirectory(Plugins.SolverDir);

            Directory.CreateDirectory(Plugins.FileLoader.RootDir);
            Directory.CreateDirectory(Plugins.FileLoader.Download);
            Directory.CreateDirectory(Plugins.FileLoader.Upload);
        }
    }

    public static class Plugins
    {
        public static readonly string RootDir = Path.Combine(Environment.CurrentDirectory, "plugins");
        // Directory of planning solver plugins
        public static readonly string SolverDir = Path.Combine(RootDir, "solver");


        // Directory of loading plugins of files 
        public static class FileLoader
        {
            // Plugins can download and upload
            public static readonly string RootDir = Path.Combine(Plugins.RootDir, "load");
            // Only download (import)
            public static readonly string Download = Path.Combine(RootDir, "download");
            // Only upload (export)
            public static readonly string Upload = Path.Combine(RootDir, "upload");

        }
    }


}
