using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppRazorWeb.Migrations.Models
{
    /// <summary>
    /// Activitie's class
    /// </summary>
    [Table("activity")]
    public class Activity
    {
        /// <summary>
        /// Identifier
        /// </summary>
        [Key]
        [Column("id", Order = 0)]
        public Guid Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [Required]
        [Column("name", Order = 1)]
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [Column("description", Order = 2)]
        public string Description { get; set; }

        [Required]
        [Column("place", Order = 3)]
        public string Place { get; set; }

        [Required]
        [Column("student_name", Order = 4)]
        public string StudentName { get; set; }

        [Column("daily_lesson", Order = 5)]
        public string DailyLesson { get; set; }

        [Column("student_course", Order = 6)]
        public int StudentCouse { get; set; }

        ///// <summary>
        ///// Total users number
        ///// </summary>
        //[Column("total_users_activity", Order = 5)] //Número total de usuarios en una actividad
        //public int TotalUsersNumber { get; set; }

        /// <summary>
        /// Creation date
        /// </summary>
        [Column("creation_date", Order = 7)]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Update date
        /// </summary>
        [Column("update_date", Order = 8)]
        public DateTime? UpdateDate { get; set; }

        #region Foreign Relations

        /// <summary>
        /// <see cref="Schedule"/>
        /// </summary>
        public virtual ICollection<Schedule> ScheduleRelation { get; set; }

        #endregion Foreign Relations
    }
}
