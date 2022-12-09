using System;

namespace AppRazorWeb.Framework.Dataservices.Models
{
    public class UserWriteModel
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string DNI { get; set; }

        public int Age { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

    }

    public class UserReadModel : UserWriteModel
    {

    }
}
