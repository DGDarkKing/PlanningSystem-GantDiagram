using PLuginsData.Models;

namespace SumDurations
{
    public class SumDurationPlanner : IPlanner
    {
        List<List<MachinDetail>>? _machinesDetails;
        public List<List<MachinDetail>>? MachinesDetails { get => _machinesDetails; set => _machinesDetails = value; }

        public class Double
        {
            public double Value { get; set; }
        }

        public List<int> Plan
        {
            get
            {
                List<Double> summs = new List<Double>();
                foreach (var item in _machinesDetails[0])
                {
                    summs.Add(new Double { Value = item.Duration });
                }
                for (int i = 1; i < _machinesDetails.Count; i++)
                {
                    for (int j = 0; j < _machinesDetails[i].Count; j++)
                    {
                        summs[j].Value += _machinesDetails[i][j].Duration;
                    }
                }

                List<Double> copy = new List<Double>(summs);
                copy.OrderByDescending(x => x.Value);

                List<int> order = new();
                foreach (var item in copy)
                {
                    order.Add(summs.IndexOf(item));
                }

                return order;
            }
        }

        public string Name { get => "Суммарное время"; }
    }
}