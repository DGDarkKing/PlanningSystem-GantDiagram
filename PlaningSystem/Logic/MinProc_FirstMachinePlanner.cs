using PLuginsData.Models;
using System.Collections.Generic;
using System.Linq;

namespace PlaningSystem.Logic
{
    class MinProc_FirstMachinePlanner : IPlanner
    {
        List<List<MachinDetail>>? _machinesDetails;
        public List<List<MachinDetail>>? MachinesDetails { get => _machinesDetails; set => _machinesDetails = value; }

        public List<int> Plan
        {
            get
            {
                var detailsFirstMachine = new List<MachinDetail>(_machinesDetails[0]);
                detailsFirstMachine = detailsFirstMachine.OrderBy(detail => detail.Duration).ToList();

                List<int> order = new();
                foreach (var item in detailsFirstMachine)
                {
                    order.Add(_machinesDetails[0].IndexOf(item));
                }

                return order;
            }
        }
    }
}
