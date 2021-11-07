using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTraining.Models
{
    public class LoginModel
    {
        public string EmployeeId { get; set; }
        public string Password { get; set; }
        
    }
    public class LoginViewModel
    {
        public LoginModel user { get; set; }
        public string Message { get; set; }
    }
    public class MenViewModel
    {
      public List<Men> mens { get; set; }
      public Message Message { get; set; }
    }
}
