using DefaultPlanners;
using Microsoft.Win32;
using PLuginsData.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Windows;
using System.Windows.Controls;

namespace PlaningSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<List<MachinDetail>> MachinesDetails = new();
        Microsoft.Win32.OpenFileDialog openFileDllSolverPluginDialog = new();
        Microsoft.Win32.SaveFileDialog saveFileDialog = new();
        IPlanner? currentPlanner = null;
        Dictionary<string, IPlanner> extensionsPlanner = new();

        public MainWindow()
        {
            InitializeComponent();

            openFileDllSolverPluginDialog.Filter = "dinamicaly lib solver plugin|*.dll";

            foreach (var solverPluginDll in Directory.GetFiles(Plugins.SolverDir, "*.dll"))
            {
                LoadExtension(solverPluginDll);
            }

            if(extensionsPlanner.Count > 0)
            {
                MenuItem_SolverPlugins.Visibility = Visibility.Visible;
            }


            // TODO Future: Read ./plugins/load and neasted dirs


            MachinesDetails.Add(new List<MachinDetail>
            {
                new MachinDetail
                {
                    detail = new Detail{ Name = "Деталь 1", ColorBrush = Color.FromArgb(255, 0, 0)},
                    Duration = 10
                },
                
            });
        }

        private void LoadExtension(string solverPluginDll)
        {
            AssemblyLoadContext assemblyLoadContext = new AssemblyLoadContext(solverPluginDll);
                assemblyLoadContext.Resolving += AssemblyLoadContext_Resolving;
                Assembly assembly = assemblyLoadContext.LoadFromAssemblyPath(solverPluginDll);
                IPlanner plannerPlugin = Activator.CreateInstance(assembly.GetTypes()
                                                                .Where(type => type.GetInterfaces()
                                                                                .Any(i => i == typeof(IPlanner))).First()) as IPlanner;

                string pluginName = plannerPlugin.Name;
                extensionsPlanner.Add(pluginName, plannerPlugin);

                MenuItem extensionMenuItem = new MenuItem();
                extensionMenuItem.Header = pluginName;
                extensionMenuItem.Click += ExtensionMenuItem_Click;
                MenuItem_SolverPlugins.Items.Add(extensionMenuItem);
        }

        private Assembly? AssemblyLoadContext_Resolving(AssemblyLoadContext arg1, AssemblyName arg2)
        {
            AssemblyLoadContext assemblyLoadContext = new AssemblyLoadContext(null);
            return assemblyLoadContext.LoadFromAssemblyPath(Path.Combine(Plugins.SolverDir, $"{arg2.Name}.dll"));
        }

        private void MenuItem_DataSettings_Click(object sender, RoutedEventArgs e)
        {
            new DataWindow(MachinesDetails).ShowDialog();
            foreach (var machine in MachinesDetails)
            {
                foreach (var machinDetail in machine)
                {
                    machinDetail.StartUnit = null;
                }
            }
        }


        private void MenuItem_Upload_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_DownloadData_Click(object sender, RoutedEventArgs e)
        {

        }






        private void MenuItem_AddPlugin_Click(object sender, RoutedEventArgs e)
        {
            var dialogResult = openFileDllSolverPluginDialog.ShowDialog();
            if (dialogResult==null || dialogResult == false)
                return;


            foreach (var filepath in openFileDllSolverPluginDialog.FileNames)
            {
                string filename = filepath.Substring(filepath.LastIndexOf('\\')+1);
                string dllPath = Path.Combine(Plugins.SolverDir, filename);
                File.Copy(filepath, dllPath);
                LoadExtension(dllPath);
            }

            if (extensionsPlanner.Count > 0)
            {
                MenuItem_SolverPlugins.Visibility = Visibility.Visible;
            }
        }

        void Reset()
        {
            foreach (var item in MachinesDetails)
            {
                foreach (var detail in item)
                {
                    detail.StartUnit = null;
                }
            }
        }

        private void ExtensionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItemSender = sender as MenuItem;
            Reset();
            if (currentPlanner == null || currentPlanner != extensionsPlanner[menuItemSender.Header.ToString()])
            {
                currentPlanner = extensionsPlanner[menuItemSender.Header.ToString()];
            }
            currentPlanner.MachinesDetails = MachinesDetails;
            var order = currentPlanner.Plan;
            PlannerChart_Gant.SetData(MachinesDetails, order);
        }

        private void MenuItem_NoPlanning_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            if (currentPlanner == null || currentPlanner is not DirectPlanner)
            {
                currentPlanner = new DirectPlanner();
            }
            currentPlanner.MachinesDetails = MachinesDetails;
            var order = currentPlanner.Plan;
            PlannerChart_Gant.SetData(MachinesDetails, order);
        }

        private void MenuItem_MinProc_FirstMAchine_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            if (currentPlanner == null || currentPlanner is not MinProc_FirstMachinePlanner)
            {
                currentPlanner = new MinProc_FirstMachinePlanner();
            }
            currentPlanner.MachinesDetails = MachinesDetails;
            var order = currentPlanner.Plan;
            PlannerChart_Gant.SetData(MachinesDetails, order);
        }

        private void MenuItem_MaxProc_LastMachine_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            if (currentPlanner == null || currentPlanner is not MaxProc_LastMachinePlanner)
            {
                currentPlanner = new MaxProc_LastMachinePlanner();
            }
            currentPlanner.MachinesDetails = MachinesDetails;
            var order = currentPlanner.Plan;
            PlannerChart_Gant.SetData(MachinesDetails, order);
        }

        private void MenuItem_Bottelneck_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            if (currentPlanner == null || currentPlanner is not BottelneckPlanner)
            {
                currentPlanner = new BottelneckPlanner();
            }
            currentPlanner.MachinesDetails = MachinesDetails;
            var order = currentPlanner.Plan;
            PlannerChart_Gant.SetData(MachinesDetails, order);
        }
    }
}
