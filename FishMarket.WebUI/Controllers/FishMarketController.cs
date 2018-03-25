using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FishMarket.WebUI.Models.FishDefinition;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FishMarket.WebUI.Controllers
{
    [Authorize]
    public class FishMarketController : Controller
    {
        private string baseAddr = "http://localhost:8081/api/fishes/v1";

        [AllowAnonymous]
        public IActionResult Index()
        {

            /// TODO : Need to write FishMarket Api client 
            var resp = GetHttpClient().GetAsync($"{baseAddr}/list").Result;

            resp.EnsureSuccessStatusCode();

            var str = resp.Content.ReadAsStringAsync().Result;

            return View(JsonConvert.DeserializeObject<List<FishDefinitionViewModel>>(str));
        }


        #region Helper Methods

        private HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddr);
            return client;
        }

        #endregion
    }
}