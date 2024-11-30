using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotofacil.Domain.Entities
{
    /// <summary>
    /// Serves as the base entity for a contest, providing common properties shared across all related entities.
    /// </summary>
    public abstract class ContestBaseEntity
    {
        /// <summary>
        /// Gets or sets the contest's ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the contest's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the contest's data.
        /// </summary>
        public DateTime Data { get; set; }

        /// <summary>
        /// Gets or sets the contest's numbers.
        /// </summary>
        public string Numbers { get; set; }
    }
}


