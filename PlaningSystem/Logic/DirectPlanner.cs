using PLuginsData.Models;
using System.Collections.Generic;
using System.Linq;

namespace PlaningSystem.Logic
{
    class DirectPlanner : IPlanner
    {
        List<List<MachinDetail>>? _machinesDetails;
        public List<List<MachinDetail>>? MachinesDetails { get => _machinesDetails; set => _machinesDetails = value; }

        public List<int> Plan => new (Enumerable.Range(0, MachinesDetails[0].Count));
            
}
}
