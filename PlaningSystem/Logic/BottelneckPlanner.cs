using PLuginsData.Models;
using System.Collections.Generic;

namespace PlaningSystem.Logic
{

    class Pair<T, V>
    {
        public T Key;
        public V Value;

        public Pair(T key, V val) => (Key, Value) = (key, val);
    }


    class BottelneckPlanner : IPlanner
    {
        List<List<MachinDetail>>? _machinesDetails;
        public List<List<MachinDetail>>? MachinesDetails { get => _machinesDetails; set => _machinesDetails = value; }

        public List<int> Plan
        {
            get
            {
                List<Pair<int, double>> machineIndeces_duration = new List<Pair<int, double>>();
                foreach (var item in _machinesDetails[0])
                {
                    machineIndeces_duration.Add(new Pair<int, double>(0, item.Duration));
                }
                for (int i = 1; i < MachinesDetails.Count; i++)
                {
                    for (int j = 0; j < machineIndeces_duration.Count; j++)
                    {
                        if (MachinesDetails[i][j].Duration > machineIndeces_duration[j].Value)
                        {
                            machineIndeces_duration[j].Value = MachinesDetails[i][j].Duration;
                            machineIndeces_duration[j].Key = i;
                        }
                    }
                }

                var copy = new List<Pair<int, double>>(machineIndeces_duration);
                copy.Sort((x, y) =>
                            {
                                int res = x.Key.CompareTo(y.Key);
                                if(res == 0)
                                {
                                    res = x.Value.CompareTo(y.Value);
                                }
                                return -1 * res; // Reverse result
                            });

                List<int> order = new();
                foreach (var item in copy)
                {
                    order.Add(machineIndeces_duration.IndexOf(item));
                }

                return order;
            }
        }
    }
}
