using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void OnMouseClick(object sender, MouseEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
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
                            string[] returnedUsers = { user.UserName, user.FirstName, user.LastName, user.Email };
                            var listViewItems = new ListViewItem(returnedUsers);
                            listaImena.Items.Add(listViewItems);
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
