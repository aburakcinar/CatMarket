using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FishMarket.WebUI.Models.FishDefinition;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FishMarket.WebUI.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class FishesController : Controller
    {
        private string baseAddr = "http://localhost:8081/api/fishes/v1";
        private string baseUploadAddr = "http://localhost:8081/api/upload/v1";

        // GET: FishDefinition
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var resp = GetHttpClient().GetAsync($"{baseAddr}/list").Result;

            resp.EnsureSuccessStatusCode();

            var str = resp.Content.ReadAsStringAsync().Result;

            return View(JsonConvert.DeserializeObject<List<FishDefinitionViewModel>>(str));
        }

        // GET: FishDefinition/Details/5
        [HttpGet("{id}")]
        public ActionResult UploadPicture(int id)
        {
            return View();
        }

        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult UploadPicture(int id, UploadPictureViewModel model)
        {
            using (var content = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString()))
            {
                var streamContent = new StreamContent(model.PictureFile.OpenReadStream());
                streamContent.Headers.Add("Content-Type", model.PictureFile.ContentType);
                streamContent.Headers.Add("Content-Disposition", $"form-data; name=\"file\"; filename=\"{model.PictureFile.Name}\"");
                content.Add(streamContent, "file");

                var msg = GetHttpClient().PutAsync($"{baseUploadAddr}/replace/{model.Id}", content).Result;

                msg.EnsureSuccessStatusCode();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}")]
        public ActionResult UpdatePrice(int id)
        {
            var resp = GetHttpClient().GetAsync($"{baseAddr}/get/{id}").Result;

            resp.EnsureSuccessStatusCode();

            var str = resp.Content.ReadAsStringAsync().Result;

            return View(JsonConvert.DeserializeObject<FishDefinitionViewModel>(str));
        }

        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePrice(int id, FishDefinitionViewModel model)
        {
            var priceContent = new StringContent(JsonConvert.SerializeObject(model.Price), System.Text.Encoding.UTF8, "application/json");
            var client = GetHttpClient();
            var msg = client.PutAsync($"{baseAddr}/price/{id}", priceContent).Result;


            //msg.EnsureSuccessStatusCode();

            if(!msg.IsSuccessStatusCode)
            {
                switch (msg.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        ModelState.AddModelError("Price", "Price must be changed");
                        return View(model);
                }
            }

            return RedirectToAction(nameof(Index));

        }

        // GET: FishDefinition/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FishDefinition/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FishDefinitionViewModel model)
        {
            try
            {
                HttpClient client = GetHttpClient();

                var myContent = JsonConvert.SerializeObject(model);

                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                using (var byteContent = new ByteArrayContent(buffer))
                {
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var result = client.PostAsync($"{baseAddr}/add", byteContent).Result;

                    result.EnsureSuccessStatusCode();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FishDefinition/Edit/5
        public ActionResult Edit(int id)
        {
            return View(new FishDefinitionViewModel { Id = id });
        }

        // POST: FishDefinition/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FishDefinitionViewModel model)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FishDefinition/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FishDefinition/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        private HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddr);
            return client;
        }

    }
}