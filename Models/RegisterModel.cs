using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Models
{
    public class RegisterModel 
    {
        public int c_id{get; set;}
        
        public string c_name{get; set;}
        
        public string c_email{get; set;}

        public string c_password{get; set;}

        public int c_status {get; set;}
    }
}