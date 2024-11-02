using Lotofacil.Domain.Entities;

namespace Lotofacil.Application.ViewsModel
{
    public class HomeViewModel
    {
        public Contest Contest { get; set; }

        public List<BaseContest> BaseContestList { get; set; }
    }
}
