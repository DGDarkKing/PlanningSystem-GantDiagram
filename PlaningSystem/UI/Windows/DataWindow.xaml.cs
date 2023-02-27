using PlaningSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace PlaningSystem
{
    /// <summary>
    /// Interaction logic for DataWindow.xaml
    /// </summary>
    public partial class DataWindow : Window
    {
        List<List<MachinDetail>> MachinsDetails = null;
        
        public DataWindow(List<List<MachinDetail>> MachinDetails)
        {
            if(MachinDetails == null)
            {
                throw new ArgumentException("MachineDetails argument cant be value equals null");
            }
            this.MachinsDetails = MachinDetails;


            InitializeComponent();


            InitDataTable();
        }

        void InitDataTable()
        {
            DataTable data = new DataTable();

            if (MachinsDetails.Count > 0 && MachinsDetails[0].Count > 0) 
            {
                foreach (var item in MachinsDetails[0])
                {
                    data.Columns.Add(item.detail.Name, typeof(double));
                }

                foreach (var machin in MachinsDetails)
                {
                    data.Rows.Add();
                    foreach (var machineDetail in machin)
                    {
                        data.Rows[data.Rows.Count - 1][machineDetail.detail.Name] = machineDetail.Duration;
                    }
                }
            }
            SettingDataGrid.ItemsSource = data.DefaultView;
        }



        private void Btn_AddMachine_Click(object sender, RoutedEventArgs e)
        {
            List<MachinDetail> machinDetails = new List<MachinDetail>();
        }

        private void Btn_AddDetail_Click(object sender, RoutedEventArgs e)
        {
            MachinDetail machinDetail = new MachinDetail();
        }

        
    }
}
