using System;

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

    }

    public class UserWriteModel : UserHeader
    {

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
                Email = Email
            };
        }

    }

    public class User : UserHeader
    {

    }

}
