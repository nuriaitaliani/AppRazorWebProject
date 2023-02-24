using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppRazorWeb.Migrations.Models
{
    /// <summary>
    /// User's class
    /// </summary>
    [Table("user")]
    public class User
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
        /// Last name
        /// </summary>
        [Required]
        [Column("last_name", Order = 2)]
        public string LastName { get; set; }

        /// <summary>
        /// DNI
        /// </summary>
        [Required]
        [Column("dni", Order = 3)]
        public string DNI { get; set; }

        /// <summary>
        /// Age
        /// </summary>
        [Required]
        [Column("age", Order = 4)]
        public int Age { get; set; }

        /// <summary>
        /// Phone number
        /// </summary>
        [Required]
        [Column("phone_number", Order = 5)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress)]
        [Column("email", Order = 6)]
        public string Email { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        [Required]
        [Column("address", Order = 7)]
        public string Address { get; set; }

        /// <summary>
        /// Creation date
        /// </summary>
        [Column("creation_date", Order = 8)]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Update date
        /// </summary>
        [Column("update_date", Order = 9)]
        public DateTime? UpdateDate { get; set; }

        //public string FullName
        //{
        //    get { return string.Format("{0} {1}", Name, LastName); }
        //}

        #region Foreing Relations

        /// <summary>
        /// <see cref="UserSchedule"/>
        /// </summary>
        public virtual ICollection<UserSchedule> UserScheduleRelation { get; set; }

        #endregion Foreing Relations

    }
}
