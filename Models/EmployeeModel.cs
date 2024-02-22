using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Models
{
    
      public class EmployeeModel
    {
        public int c_empid{get; set;}

        public string c_empname{get; set;}

        public string c_date{get; set;}

        public int c_gsalary{get; set;}

        public string c_designation{get; set;}

        public string c_gender{get; set;}

        public double c_basic{get; set;}

        public double c_da{get; set;}

        public double c_hra{get; set;}

        public double c_tax{get; set;}

        public double c_taxable{get; set;}

        public double c_takehome{get; set;}
    }
}