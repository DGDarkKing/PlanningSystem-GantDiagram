using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaningSystem.Models
{
    public class MachinDetail
    {
        public Detail detail { get; set; }
        public double? StartUnit { get; set; } = null;
        public double Duration { get; set; }
    }
}
