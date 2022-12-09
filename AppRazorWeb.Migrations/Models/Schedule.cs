using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppRazorWeb.Migrations.Models
{
    /// <summary>
    /// Schedule's class
    /// </summary>
    [Table("schedule")]
    public class Schedule
    {
        /// <summary>
        /// Identifier
        /// </summary>
        [Key]
        [Column("id", Order = 0)]
        public Guid Id { get; set; }

        /// <summary>
        /// Start
        /// </summary>
        [Required]
        [DataType(DataType.Time)]
        [Column("start", Order = 1)]
        public TimeSpan Start { get; set; }

        /// <summary>
        /// End
        /// </summary>
        [Required]
        [DataType(DataType.Time)]
        [Column("end", Order = 2)]
        public TimeSpan End { get; set; }

        /// <summary>
        /// Activitie id
        /// </summary>
        [Column("activity_id", Order = 3)]
        public Guid ActivityId { get; set; }

        /// <summary>
        /// Day of week
        /// </summary>
        [Required]
        [Column("day_of_week", Order = 4)]
        public byte DayOfWeek { get; set; }

        /// <summary>
        /// Creation date
        /// </summary>
        [Column("creation_date", Order = 5)]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Update date
        /// </summary>
        [Column("update_date", Order = 6)]
        public DateTime? UpdateDate { get; set; }

        #region Foreing relations

        /// <summary>
        /// <see cref="Activity"/>
        /// </summary>
        public virtual Activity Activity { get; set; }

        /// <summary>
        /// <see cref="UserSchedule"/>
        /// </summary>
        public virtual ICollection<UserSchedule> UserScheduleRelation { get; set; }


        #endregion Foreing relations

    }
}
