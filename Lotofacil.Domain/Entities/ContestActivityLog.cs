using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lotofacil.Domain.Entities
{
    public class ContestActivityLog : ContestBaseEntity
    {
        /// <summary>
        /// Default constructor required by Entity Framework.
        /// </summary>
        public ContestActivityLog() { }

        /// <summary>
        /// Initializes a new instance of <see cref="ContestActivityLog"/> with contest 
        /// and base contest details.
        /// </summary>
        /// <param name="name">The name of the Contest.</param>
        /// <param name="numbers">The numbers associated with the Contest.</param>
        /// <param name="data">The date of the Contest.</param>
        /// <param name="bcName">The name of the BaseContest.</param>
        /// <param name="bcNumbers">The numbers associated with the BaseContest.</param>
        public ContestActivityLog(string name, string numbers, DateTime data, string bcName, string bcNumbers) 
        {
            Name = name;
            Numbers = numbers;
            Data = data;
            BaseContestName = bcName;
            BaseContestNumbers = bcNumbers;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// Gets The name of the BaseContest associated with the Contest.
        /// </summary>
        public string BaseContestName { get; private set; }

        /// <summary>
        /// Gets The numbers associated with the BaseContest.
        /// </summary>
        public string BaseContestNumbers { get; private set; }

        /// <summary>
        /// Gets The timestamp when the log entry was created.
        /// </summary>
        public DateTime CreateTime { get; private set; }
    }
}
