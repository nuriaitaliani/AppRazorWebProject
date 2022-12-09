using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppRazorWeb.Migrations.Models
{
    [Table("userschedule")]
    public class UserSchedule
    {
        [Key]
        [Column("id", Order = 0)]
        public Guid Id { get; set; }

        /// <summary>
        /// User id
        /// </summary>
        [ForeignKey("User")]
        [Column("user_id", Order = 1)]
        public Guid UserId { get; set; }

        /// <summary>
        /// Activity id
        /// </summary>
        [ForeignKey("Schedule")]
        [Column("schedule_id", Order = 3)]
        public Guid ScheduleId { get; set; }

        #region Foreing relations

        /// <summary>
        /// <see cref="Schedule"/>
        /// </summary>
        public virtual Schedule Schedule { get; set; }

        /// <summary>
        /// <see cref="User"/>
        /// </summary>
        public virtual User User { get; set; }

        #endregion Foreing relations

    }
}
