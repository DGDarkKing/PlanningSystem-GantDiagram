using PlaningSystem.Logic;
using PLuginsData.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;

namespace PlaningSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<List<MachinDetail>> MachinesDetails = new();
        Microsoft.Win32.OpenFileDialog openFileDialog = new();
        Microsoft.Win32.SaveFileDialog saveFileDialog = new();
        IPlanner? currentPlanner = null;

        public MainWindow()
        {
            InitializeComponent();
            MachinesDetails.Add(new List<MachinDetail>
            {
                new MachinDetail
                {
                    detail = new Detail{ Name = "Деталь 1", ColorBrush = Color.FromArgb(255, 0, 0)},
                    Duration = 10
                },
                
            });
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
