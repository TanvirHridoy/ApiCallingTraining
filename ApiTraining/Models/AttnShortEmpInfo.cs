using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTraining.Models
{
    
    public class AttnShortEmpInfo
    {
        public string EmpID { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public int? SortOrder { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int EmployementStatusInt { get; set; }
        public string EmployementStatusStr { get; set; }
        public int ZoneId { get; set; }
        public string ZoneCode { get; set; }
        public bool AttendanceStatus { get; set; }


    }
}
