using PLuginsData.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace PlaningSystem
{
    /// <summary>
    /// Interaction logic for DataWindow.xaml
    /// </summary>
    public partial class DataWindow : Window
    {
        List<List<MachinDetail>> MachinsDetails = null;
        Random random = new Random();
        
        public DataWindow(List<List<MachinDetail>> MachinsDetails)
        {
            if(MachinsDetails == null)
            {
                throw new ArgumentException("MachineDetails argument cant be value equals null");
            }
            this.MachinsDetails = MachinsDetails;


            InitializeComponent();

            SettingDataGrid.Columns.CollectionChanged += Columns_CollectionChanged;
            InitDataTable();
        }

        



        private void Columns_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(SettingDataGrid.Columns.Count != 0 && SettingDataGrid.Columns.Count == MachinsDetails[0].Count)
            {
                Style style;
                for (int i = 0; i < MachinsDetails[0].Count; i++)
                {
                    var detailColor = MachinsDetails[0][i].detail.ColorBrush;
                    style = new Style(typeof(DataGridColumnHeader));
                    style.Setters.Add(new Setter(DataGridColumnHeader.BackgroundProperty,
                                        new SolidColorBrush(Color.FromRgb(detailColor.R, detailColor.G, detailColor.B))));
                    SettingDataGrid.Columns[i].HeaderStyle = style;
                }
            }
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

                foreach (var machine in MachinsDetails)
                {
                    data.Rows.Add();
                    foreach (var machineDetail in machine)
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
            foreach (var item in MachinsDetails[0])
            {
                machinDetails.Add(new MachinDetail
                                    {
                                        detail = item.detail,
                                        Duration = 0
                                    });
            }
            MachinsDetails.Add(machinDetails);

            InitDataTable();
        }

        private void Btn_AddDetail_Click(object sender, RoutedEventArgs e)
        {
            var detail = new Detail
            {
                Name = $"Деталь {MachinsDetails[0].Count + 1}",
                ColorBrush = System.Drawing.Color.FromArgb((byte)random.Next(0, 266), (byte)random.Next(0, 266), (byte)random.Next(0, 266))
            };

            foreach (var item in MachinsDetails)
            {
                item.Add(new MachinDetail
                {
                    detail = detail,
                    Duration = 0
                });
            }

            InitDataTable();

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            for (int i = 0; i < SettingDataGrid.Items.Count; i++)
            {
                var rowData = ((DataRowView)SettingDataGrid.Items[i]).Row.ItemArray;
                for (int j = 0; j < rowData.Length; j++)
                {
                    double duration = (double)(rowData.GetValue(j));
                    MachinsDetails[i][j].Duration = duration;
                }
            }

        }

        private void SettingDataGrid_CellEditEnding(object sender, System.Windows.Controls.DataGridCellEditEndingEventArgs e)
        {
        }
    }
}
