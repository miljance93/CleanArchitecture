using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
           // GetUsers();
           // CreateUser();
            Console.ReadLine();
        }

        public static async Task<IEnumerable<User>> GetUsers()
        {
            HttpClient httpClient= new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7209/api/user/");
            try
            {
                var response = await httpClient.GetAsync("getusers");
                if (response != null)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var users = JsonConvert.DeserializeObject<IEnumerable<User>>(result);

                    if (users != null)
                    {
                        foreach (var user in users)
                        {
                            Console.WriteLine($"First Name: {user.FirstName}\nLast Name: {user.LastName}\nEmail: {user.Email}\n\n----------");
                        }

                        return users;
                    }                    
                }              
                return null;    
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
                
            }
            
        }

        public static async Task<bool> CreateUser()
        {
            var user = UserInfo();
            
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7209/api/user/");

            try
            {
                var content = JsonConvert.SerializeObject(user);
                var data = new StringContent(content, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("createuser", data);
                Console.WriteLine($"\nUser is created: {response.IsSuccessStatusCode}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private static User UserInfo()
        {
            Console.Write("Enter a first name: ");
            string firstName = Console.ReadLine();
            Console.Write("\nEnter a last name: ");
            string lastName = Console.ReadLine();
            Console.Write("\nEnter a username: ");
            string userName = Console.ReadLine();
            Console.Write("\nEnter a password: ");
            string password = Console.ReadLine();
            Console.Write("\nEnter an email: ");
            string email = Console.ReadLine();

            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = userName,
                Email = email,
                Password = password
            };

            return user;
        }
    }
}
