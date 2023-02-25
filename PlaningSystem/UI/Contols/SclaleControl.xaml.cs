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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Sclale : UserControl
    {
        private int unitSize = 20;
        private int unitHeight = 20;

        public int UnitSize {
            get => unitSize;
            set
            {
                if (value < 1)
                    throw new ArgumentException("Non-valid value - value of UnitSize must be greater than 0");

                unitSize = value;
            }
        }

        public int UnitHeight
        {
            get => unitHeight;
            set
            {
                if (value < 1)
                    throw new ArgumentException("Non-valid value - value of UnitHeight must be greater than 0");

                unitHeight = value;
                MinHeight = unitHeight;
            }
        }



        public enum SclaleType
        {
            Down = 0,
            Up = 1,
        }

        public SclaleType ScaleView { get; set; } = SclaleType.Down;

        public Sclale()
        {
            InitializeComponent();
            MinHeight = 20;
        }


        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            var width = ActualWidth;
            int yLine = unitHeight * (int)ScaleView;
            drawingContext.DrawLine(new Pen(new SolidColorBrush(Color.FromRgb(0, 0, 0)), 3)
                                    ,new Point(0, yLine), new Point(width, yLine));

            int height = unitHeight;
            for (int x = unitSize; x < width; x += unitSize) 
            {
                drawingContext.DrawLine(new Pen(new SolidColorBrush(Color.FromRgb(0, 0, 0)), 3)
                                    , new Point(x, 0), new Point(x, height));
            }
        }
    }
}
