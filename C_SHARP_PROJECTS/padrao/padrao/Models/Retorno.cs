using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace padrao.Models
{
    public class Retorno
    {
        public Boolean Status { get; set; }
        public String Message { get; set; }
        public  Object Response { get; set; }
    }
}