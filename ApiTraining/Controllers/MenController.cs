using ApiTraining.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApiTraining.Controllers
{
    public class MenController : Controller
    {
        public IConfiguration _config;
        private HttpClient httpClient = new HttpClient();

        public MenController(IConfiguration configuration)
        {
            _config = configuration;
            string y = _config.GetValue<string>("APi");
            httpClient.BaseAddress = new Uri(y);
        }
        // GET: MenController
        public async Task<ActionResult> IndexAsync(Message message )
        {
            MenViewModel viewModel = new MenViewModel();
            viewModel.Message = message;
            var s = HttpContext.Session.GetString("Ani");

            if (s == String.Empty || s == null)
            {
                return RedirectToAction("LogIn", "LogIn");
            }

            if (ApiConstant.LoginResp.Token == String.Empty || (ApiConstant.LoginResp.Expiration < DateTime.UtcNow))
            {
                await ApiConstant.CreateToken(HttpContext, _config);
            }
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiConstant.LoginResp.Token);
            viewModel.mens = await httpClient.GetFromJsonAsync<List<Men>>("Men");
            return View(viewModel);
        }

        // GET: MenController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MenController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Men men)
        {
            var s = HttpContext.Session.GetString("Ani");

            if (s == String.Empty || s == null)
            {
                return RedirectToAction("LogIn", "LogIn");
            }

            if (ApiConstant.LoginResp.Token == String.Empty || (ApiConstant.LoginResp.Expiration < DateTime.UtcNow))
            {
                await ApiConstant.CreateToken(HttpContext, _config);
            }
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiConstant.LoginResp.Token);
            try
            {
                var res = await httpClient.PostAsJsonAsync<Men>("Men", men);
                res.EnsureSuccessStatusCode();
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", new Message { Text = "Successfully Added"});
                }
                else
                {
                    return RedirectToAction("Index", new Message { Text = "Failed To Add" });
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: MenController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MenController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: MenController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MenController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}
