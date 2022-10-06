using ClientApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ClientApp.Controllers
{
    public class ClientController : Controller
    {
        Uri url = new Uri("https://localhost:44305/api/Category1/");
        HttpClient client;
        public ClientController()
        {
            this.client = new HttpClient();
            client.BaseAddress = url;
        }
    
        public IActionResult Index()
        {
            HttpResponseMessage msg  = client.GetAsync(client.BaseAddress + "GetCategoryData").Result;
            var respone = msg.Content.ReadAsStringAsync().Result;   
            var data = JsonConvert.DeserializeObject<List<categories>>(respone);
            return View();
        }
        public string Add(categories model)
        {
            categories data = new categories() { Name = "shivaay",ImageUrl="jjj.png" };
            string dataobject = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(dataobject,Encoding.UTF8,"application/json");
            var gettoken = client.GetAsync(client.BaseAddress + "GetJwtToken").Result;
            var token = gettoken.Content.ReadAsStringAsync().Result;

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage msg = client.PostAsync(client.BaseAddress + "AddingCategory",content).Result;
            return msg.Content.ReadAsStringAsync().Result;
        }
        public string EditRecord(categories model)
        {
            categories data = new categories() { Id=3, Name = "shivaay", ImageUrl = "jjj.png" };
            string dataobject = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent (dataobject,Encoding.UTF8,"application/json");
            HttpResponseMessage msg = client.PostAsync(client.BaseAddress + "UpdateRecord", content).Result;
            return msg.Content.ReadAsStringAsync().Result;
        }
        public string DeleteRecord(int id)
        {
            
            var data = client.DeleteAsync(client.BaseAddress + "DeleteData/" + id).Result;
            return data.Content.ReadAsStringAsync().Result; 
        }
    }

}
