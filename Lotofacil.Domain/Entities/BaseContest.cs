using System.Text;

namespace Lotofacil.Domain.Entities
{
    /// <summary>
    /// Represents the base contest, which serves as the reference point for comparisons with daily and recurring contests. 
    /// It acts as the main contest for generating analyses, with all comparisons and insights derived from its perspective.
    /// </summary>
    public class BaseContest : ContestBaseEntity
    {
        public BaseContest(string name, DateTime data, string numbers)
        {
            Name = name;
            Data = data;
            Numbers = numbers;
            ContestsAbove11 = new List<Contest>();
            CreatedAt = DateTime.Now;
            TopTenNumbers = "";
        }

        /// <summary>
        /// Gets the count of contests where 11 matches were achieved.
        /// </summary>
        public int Hit11 { get; private set; }
        /// <summary>
        /// Gets the count of contests where 12 matches were achieved.
        /// </summary>
        public int Hit12 { get; private set; }
        /// <summary>
        /// Gets the count of contests where 13 matches were achieved.
        /// </summary>
        public int Hit13 { get; private set; }
        /// <summary>
        /// Gets the count of contests where 14 matches were achieved.
        /// </summary>
        public int Hit14 { get; private set; }
        /// <summary>
        /// Gets the count of contests where 15 matches were achieved.
        /// </summary>
        public int Hit15 { get; private set; }

        /// <summary>
        /// Gets the date and time when the contest record was created.
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Gets or sets the top Ten contest's numbers.
        /// </summary>
        public string TopTenNumbers { get; set; }

        /// <summary>
        /// Gets or sets the collection of contests where 11 or more matches were achieved.
        /// </summary>
        public virtual ICollection<Contest> ContestsAbove11 { get; set; }

        /// <summary>
        /// Increments the current count of contests with 11 matches by 1.
        /// </summary>
        public void AddHit11()
        {
            Hit11 += 1;
        }
        /// <summary>
        /// Increments the current count of contests with 12 matches by 1.
        /// </summary>
        public void AddHit12()
        {
            Hit12 += 1;
        }
        /// <summary>
        /// Increments the current count of contests with 13 matches by 1.
        /// </summary>
        public void AddHit13()
        {
            Hit13 += 1;
        }
        /// <summary>
        /// Increments the current count of contests with 14 matches by 1.
        /// </summary>
        public void AddHit14()
        {
            Hit14 += 1;
        }
        /// <summary>
        /// Increments the current count of contests with 15 matches by 1.
        /// </summary>
        public void AddHit15()
        {
            Hit15 += 1;
        }
    }
}
