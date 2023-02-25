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
        public MainWindow()
        {
            InitializeComponent();

            var firstDetail = new Detail
            {
                Name = "HelloWorld",
                Brush = new SolidColorBrush(Color.FromRgb(200, 50, 2))
            };
            var secondDetail = new Detail
            {
                Name = "Lyayayayayayaya",
                Brush = new SolidColorBrush(Color.FromRgb(0, 200, 2))
            };

            Planner.MachinDetails = new List<List<MachinDetail>>
            {

                new List<MachinDetail> {
                    new MachinDetail
                    {
                        detail = firstDetail,
                        Duration = 30,
                    },
                    new MachinDetail
                    {
                        detail = secondDetail,
                        Duration =9,
                    }
                },

                new List<MachinDetail> {
                    new MachinDetail
                    {
                        detail = firstDetail,
                        Duration = 3,
                    },
                    new MachinDetail
                    {
                        detail = secondDetail,
                        Duration =4,
                    }
                }
            };

            Planner.MachineNames = new List<string> { "First" };

            //MPlanner.Details = new List<MachinDetail> { new MachinDetail {
            //    detail = new Detail
            //    {
            //        Name = "HelloWorld",
            //        Brush = new SolidColorBrush(Color.FromRgb(200, 50, 2))
            //    },
            //    Duration = 3,
            //    StartUnit = 1
            //},
            //new MachinDetail {
            //    detail = new Detail
            //    {
            //        Name = "Lyayayayayayaya",
            //        Brush = new SolidColorBrush(Color.FromRgb(0, 200, 2))
            //    },
            //    Duration =9,
            //    StartUnit = 5
            //}
            //};
        }
    }
}
