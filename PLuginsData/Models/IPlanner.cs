namespace PLuginsData.Models
{
    public interface IPlanner
    {
        List<List<MachinDetail>>? MachinesDetails { get; set; }

        public List<int> Plan {get;}

        public string Name { get;}
    }
}
