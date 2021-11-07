using ApiTraining.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BappsPermissionMVC.Controllers
{
    public class LogInController : Controller
    {
        public IConfiguration _config;
        private HttpClient httpClient = new HttpClient();

        public LogInController(IConfiguration configuration)
        {
            _config = configuration;
            string y = _config.GetValue<string>("APi");
            httpClient.BaseAddress = new Uri(y);
        }
        // GET: LogInController
        public ActionResult LogIn(Message Message = null)
        {
            LoginViewModel viewModel = new LoginViewModel();
            if (Message != null)
            {
                viewModel.Message = Message.Text;
            }

            return View(viewModel);
        }

        // GET: LogInController/Details/5
        public async Task<ActionResult> CreateToken(LoginViewModel model)
        {
            //For returning message if login failed 
            Message message = new Message();
            // If login succed? get login reposnse  in this model
            LoggedInModel LoginResp = new LoggedInModel();
            //Password hash maker
            model.user.Password = model.user.Password;
            //json converting user password and employee id object to store in session if login succeed
            string userdata = JsonConvert.SerializeObject(model.user);
            //string y = _config.GetValue<string>("APi");
            //httpClient.BaseAddress = new Uri(y);
            try
            {
                var res = await httpClient.PostAsJsonAsync<LoginModel>("Jwt", model.user);
                res.EnsureSuccessStatusCode();
                if (res.IsSuccessStatusCode)
                {
                    try
                    {
                        var x = await res.Content.ReadAsStringAsync();
                        LoginResp = await res.Content.ReadFromJsonAsync<LoggedInModel>();
                       
                        HttpContext.Session.SetString("Ani", userdata);
                        ApiConstant.LoginResp = LoginResp;
                        return RedirectToAction("Index", "Men");
                        
                    }
                    catch (Exception ex)
                    {
                        message.Text = "Invalid User";
                        return RedirectToAction("LogIn", message);
                    }
                }
                else
                {
                    message.Text = "Invalid User";
                    return RedirectToAction("LogIn", message);
                }
            }
            catch (Exception)
            {
                message.Text = "Invalid Credentials";
                return RedirectToAction("LogIn", message);
            }
        }

        // GET: LogInController/Create
        public ActionResult LogOut()
        {
            HttpContext.Session.Remove("Bepza");

            return RedirectToAction("LogIn");
        }
    }
}
