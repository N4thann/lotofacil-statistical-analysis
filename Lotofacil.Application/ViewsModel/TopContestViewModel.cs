using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotofacil.Application.ViewsModel
{
    public class TopContestViewModel
    {
        public string Name { get; set; }

        public DateTime Data { get; set; }

        public string Number { get; set; }

        public List<NumberOccurencesViewModel> NumberOccurences { get; set; }
    }
}
