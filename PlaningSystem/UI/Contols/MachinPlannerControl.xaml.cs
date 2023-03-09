using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PLuginsData.Models;
using System.Windows.Shapes;

namespace PlaningSystem.Themes
{


    /// <summary>
    /// Interaction logic for MachinPlannerControl.xaml
    /// </summary>
    public partial class MachinPlannerControl : UserControl
    {
        private int unitSize = 20;

        public int UnitSize
        {
            get => unitSize;
            set
            {
                if (value < 1)
                    throw new ArgumentException("Non-valid value - value of UnitSize must be greater than 0");

                unitSize = value;
            }
        }

        List<MachinDetail> details = null;
        public List<MachinDetail> Details { 
            get => details;
            set
            {
                details = value;
                change = true;
                CalculateEndPoints();
                InvalidateVisual();
            }
        }

        List<int> order = null;
        public List<int> OrderDetailIndeces
        {
            get => order;
            set
            {
                order = value;
                if(order != null && details != null)
                {
                    change = true;
                    CalculateEndPoints();
                    InvalidateVisual();
                }
            }
        }

        public void SetDetailsOrder(List<MachinDetail> details, List<int> order)
        {
            this.details = details;
            this.order = order;
            change = true;
            CalculateEndPoints();
            InvalidateVisual();
        }

       

        List<double> endPoints = new List<double>();
        public List<double> EndPoints { get => endPoints; }

        bool change = false;

        public MachinPlannerControl()
        {
            InitializeComponent();
        }

        public string MachineName { get => Name.Text; set => Name.Text = value; }
        public TextBlock MachineNameBox { get => Name; }

        List<Rectangle> Bars = new List<Rectangle>();

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);


            if (change)
            {
                ChangeData();
                change = false;
            }

            MinWidth = Name.ActualWidth + EndPoints.LastOrDefault(0);
        }

        private void ChangeData()
        {
            if(details != null && details.Count != 0)
            {
                foreach (var bar in Bars)
                {
                    MainGrid.Children.Add(bar);
                    Grid.SetColumn(bar, 1);
                }
            }
        }

        // TODO: Consider the case when the next machine has not previous detail
        private void CalculateEndPoints()
        {
            Bars = new List<Rectangle>();
            endPoints = new List<double>();

            if (order == null || order.Count == 0)
            {
                order = new(Enumerable.Range(0, details.Count));
            }

            foreach (var index in order)
            {
                var bar = CreateBar(details[index]);
                if (bar != null)
                {
                    Bars.Add(bar);
                }
            }
        }


        private Rectangle CreateBar(MachinDetail machinDetail)
        {
            if(machinDetail.Duration == 0)
            {
                if(endPoints.Count == 0)
                    endPoints.Add(0);
                else
                    endPoints.Add(endPoints[endPoints.Count-1]);
                return null;
            }

            Rectangle rectangle = new Rectangle();
            Point start = new Point(0, 0);
            if(endPoints.Count > 0)
            {
                start.X = endPoints[endPoints.Count - 1];
            }
         
            if (machinDetail.StartUnit != null && machinDetail.StartUnit > 0)
            {
                if(start.X < (double)machinDetail.StartUnit * unitSize)
                    start.X = (double)machinDetail.StartUnit * unitSize;
            }
            rectangle.RenderTransform = new TranslateTransform(start.X, start.Y);
            rectangle.HorizontalAlignment = HorizontalAlignment.Left;
            rectangle.Width = machinDetail.Duration * unitSize;
            var color = machinDetail.detail.ColorBrush;
            rectangle.Fill = new SolidColorBrush(Color.FromRgb(color.R, color.G, color.B));
            rectangle.ToolTip = machinDetail.detail.Name;

            endPoints.Add(start.X + machinDetail.Duration * unitSize);
            return rectangle;
        }
    }
}
