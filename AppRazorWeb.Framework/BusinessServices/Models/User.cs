using System;
using System.Collections.Generic;

namespace AppRazorWeb.Framework.BusinessService.Models
{
    /// <summary>
    /// UserHeader model
    /// </summary>
    public class UserHeader
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string DNI { get; set; }

        public int Age { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

    }

    public class UserWriteModel : UserHeader
    {
        public List<Guid> Schedules { get; set; }

        public Dataservices.Models.UserWriteModel ToDataServiceModel()
        {
            return new Dataservices.Models.UserWriteModel()
            {
                Id = Id,
                Name = Name,
                LastName = LastName,
                DNI = DNI,
                Age = Age,
                PhoneNumber = PhoneNumber,
                Email = Email,
                Address = Address
            };
        }

    }

    public class User : UserHeader
    {
        public List<UserSchedule_User> UserSchedules { get; set; }
    }


    public class UserSchedule_User
    {
        public Guid ScheduleId { get; set; }
        public Guid ActivityId { get; set; }
        public string ActivityName { get; set; }
    }
}