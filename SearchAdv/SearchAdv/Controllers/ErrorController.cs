using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchAdv.Controllers
{
    public class ErrorController : Controller
    {
        public string PageNotFound()
        {
            return "Page Not Found";
        }
    }
}
