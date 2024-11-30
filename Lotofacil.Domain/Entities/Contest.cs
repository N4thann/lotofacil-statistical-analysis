﻿using System.Text;

namespace Lotofacil.Domain.Entities
{
    public class Contest : ContestBaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Contest"/> class with the specified name, date, and numbers.
        /// </summary>
        public Contest(string name, DateTime data, string numbers)
        {
            Name = name;
            Data = data;
            Numbers = numbers;
            BaseContests = new List<BaseContest>();
        }
        /// <summary>
        /// Gets or sets the date of the contest when it was last processed.
        /// </summary>
        public DateTime? LastProcessed { get; set; }

        // Navegação muitos-para-muitos
        public virtual ICollection<BaseContest> BaseContests { get; set; }
    }
}
