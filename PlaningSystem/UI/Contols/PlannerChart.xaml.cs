using PlaningSystem.Themes;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PLuginsData.Models;

namespace PlaningSystem
{
    /// <summary>
    /// Interaction logic for PlannerChart.xaml
    /// </summary>
    public partial class PlannerChart : UserControl
    {
        List<List<MachinDetail>> machineDetails = new List<List<MachinDetail>>();
        private int unitSize = 20;

        public List<List<MachinDetail>> MachinDetails
        {
            get => machineDetails;
            set
            {
                machineDetails = value;
                changed = true;
                InvalidateVisual();
            }
        }

        List<int> order = new List<int>();
        public List<int> Order { 
            get => order; 
            set 
            {
                order = value; 
                changed = true;
                InvalidateVisual();
            } 
        }
        public void SetData(List<List<MachinDetail>> machineDetails, List<int> order)
        {
            this.machineDetails = machineDetails;
            this.order = order;
            changed = true;
            InvalidateVisual();
        }

        List<string> machineNames = new List<string>();
        public List<string> MachineNames { 
            get => machineNames; 
            set
            {
                machineNames = value;
                changed = true;
                InvalidateVisual();
            }
        }
        List<MachinPlannerControl> machinPlanners = new List<MachinPlannerControl>();
        double maxNameWidth = 0;

        public PlannerChart()
        {
            InitializeComponent();

            
        }
        bool changed = false;
        int counter = 0;
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if(changed)
            {
                ChangeData();
                changed = false;
            }

        }

        private void ChangeData()
        {
            var controls = MainFrame.Children;
            controls.RemoveRange(1, controls.Count - 1);
            machinPlanners.Clear();
            counter = 0;
            if (machineDetails.Count == 0)
                return;

            List<double> endPoints = new List<double>();
            for (int i = 0; i < machineDetails.Count; i++)
            {
                var planner = new MachinPlannerControl();
                planner.MachineName = $"Машина {i + 1}";
                planner.SizeChanged += Planner_SizeChanged;
                planner.HorizontalAlignment = HorizontalAlignment.Left;
                if(i < machineNames.Count)
                {
                    planner.MachineName = machineNames[i];
                }
                machinPlanners.Add(planner);
                controls.Add(planner);

                var details = machineDetails[i];
                if(endPoints != null && endPoints.Count > 0)
                {
                    if(order!=null && order.Count > 0)
                    {
                        for (int j = 0; j < endPoints.Count; j++)
                        {
                            details[order[j]].StartUnit = endPoints[j] / unitSize;
                        }
                    }
                    else
                    {
                        for (int j = 0; j < endPoints.Count; j++)
                        {
                            details[j].StartUnit = endPoints[j] / unitSize;
                        }
                    }
                }
                planner.SetDetailsOrder(details, order);
                endPoints = planner.EndPoints;
                planner.MachineNameBox.SizeChanged += MachineNameBox_SizeChanged;
            }
        }

        private void Planner_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(e.WidthChanged)
            {
                var planner = sender as MachinPlannerControl;
                if(scale.ActualWidth < planner.ActualWidth)
                {
                    scale.MinWidth = planner.ActualWidth;
                }
            }
        }

        private void MachineNameBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(e.WidthChanged)
            {
                var box = sender as TextBlock;
                if(maxNameWidth < box.ActualWidth)
                    maxNameWidth = box.ActualWidth;
                counter++;
                
                if(counter == machineDetails.Count)
                {
                    scale.Margin = new Thickness(maxNameWidth, 0, 0, 5);
                    double width = maxNameWidth + scale.ActualWidth;
                    foreach (var item in machinPlanners)
                    {
                        item.MachineNameBox.MinWidth = maxNameWidth;
                    }
                }
            }
        }
    }
}
