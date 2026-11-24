using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using KingsmenApp.Models;

namespace KingsmenApp.Services
{
    // Хранилище в памяти. Позже можно заменить на базу данных.
    public static class DataStore
    {
        public static List<User> Users { get; } = new List<User>();
        public static ObservableCollection<Service> Services { get; } = new ObservableCollection<Service>();
        private static int _nextServiceId = 1;

        static DataStore()
        {
            Users.Add(new User
            {
                Login = "admin",
                PasswordHash = PasswordHelper.Hash("admin"),
                LastName = "Администратор",
                FirstName = "Admin"
            });

            AddService(new Service
            {
                Name = "Пошив костюма",
                Description = "Индивидуальный пошив по меркам",
                Price = 45000,
                ServiceType = ServiceTypes.All[0]
            });
        }

        public static User FindUser(string login) =>
            Users.FirstOrDefault(u => u.Login.Equals(login, StringComparison.OrdinalIgnoreCase));

        public static void AddUser(User user) => Users.Add(user);

        public static Service AddService(Service service)
        {
            service.Id = _nextServiceId++;
            Services.Add(service);
            return service;
        }

        public static void DeleteService(Service service) => Services.Remove(service);
    }

    public static class ServiceTypes
    {
        public static readonly string[] All = { "Пошив", "Ремонт", "Подгонка", "Консультация" };
    }

    public static class PasswordHelper
    {
        public static string Hash(string input)
        {
            using (var sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
