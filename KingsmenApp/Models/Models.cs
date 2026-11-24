using System;

namespace KingsmenApp.Models
{
    public class User
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string TopSize { get; set; }
        public string BottomSize { get; set; }
        public int? ShoeSize { get; set; }
    }

    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ServiceType { get; set; }
        public string ImagePath { get; set; }
    }
}
