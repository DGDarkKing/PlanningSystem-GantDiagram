using PLuginsData.Models;
using System.Collections.Generic;
using System.Linq;

namespace PlaningSystem.Logic
{
    class MaxProc_LastMachinePlanner : IPlanner
    {
        List<List<MachinDetail>>? _machinesDetails;
        public List<List<MachinDetail>>? MachinesDetails { get => _machinesDetails; set => _machinesDetails = value; }

        public List<int> Plan
        {
            get
            {
                var currentMachine = _machinesDetails[_machinesDetails.Count - 1];
                var detailsLastMachine = new List<MachinDetail>(currentMachine);
                detailsLastMachine = detailsLastMachine.OrderByDescending(detail => detail.Duration).ToList();

                List<int> order = new();
                foreach (var item in detailsLastMachine)
                {
                    order.Add(currentMachine.IndexOf(item));
                }

                return order;
            }
        }
    }
}
