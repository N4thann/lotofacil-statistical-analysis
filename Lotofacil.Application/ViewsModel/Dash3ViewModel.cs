namespace Lotofacil.Application.ViewsModel
{
    public class Dash3ViewModel
    {
        public int TotalContests { get; set; }

        public int TotalBaseContests { get; set; }

        public Dictionary<string, int> Years { get; set; }

        public string LastContest { get; set; }

        public string FirstContest { get; set; }
    }
}
