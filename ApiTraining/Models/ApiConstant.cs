using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApiTraining.Models
{

    public class ApiConstant
    {
        private static HttpContext _context;
        public static IConfiguration _config;

        private static HttpClient httpClient = new HttpClient();
        // to store token for user
        public static LoggedInModel LoginResp = new LoggedInModel();

        public static async Task CreateToken(HttpContext httpContext, IConfiguration configuration)
        {
            _context = httpContext;
            _config = configuration;
            LoginModel model = new LoginModel();
            //model.EmployeeId = _config.GetValue<string>("EmployeeId");
            //model.Password = _config.GetValue<string>("Password");
            string UserData = _context.Session.GetString("Ani");
            model = JsonConvert.DeserializeObject<LoginModel>(UserData);
            string y = _config.GetValue<string>("APi");
            httpClient.BaseAddress = new Uri(y);

            var res = await httpClient.PostAsJsonAsync<LoginModel>("Jwt", model);
            res.EnsureSuccessStatusCode();
            if (res.IsSuccessStatusCode)
            {
                try
                {
                    var x = await res.Content.ReadAsStringAsync();
                    LoginResp = await res.Content.ReadFromJsonAsync<LoggedInModel>();
                    //return result;
                }
                catch (Exception ex)
                {
                    var x = ex.Message;
                }

            }
        }
    }
}
