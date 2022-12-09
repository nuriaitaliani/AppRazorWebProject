using System;

namespace AppRazorWeb.API.ViewModels
{
	public class UserViewModel
	{

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string DNI { get; set; }

        public int Age { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

    }
}
