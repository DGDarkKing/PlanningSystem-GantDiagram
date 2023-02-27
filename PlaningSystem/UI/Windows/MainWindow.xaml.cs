using PlaningSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PlaningSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<List<MachinDetail>> MachinDetails = new List<List<MachinDetail>>();

        public MainWindow()
        {
            InitializeComponent();

            MachinDetails.Add(new List<MachinDetail>
            {
                new MachinDetail
                {
                    detail = new Detail{ Name = "Hello"},
                    Duration = 10
                },
                new MachinDetail
                {
                    detail = new Detail{ Name = "World"},
                    Duration = 2
                },
            });
        }

        private void MenuItem_DataSettings_Click(object sender, RoutedEventArgs e)
        {
            new DataWindow(MachinDetails).ShowDialog();
        }

        private void MenuItem_LoadData_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
