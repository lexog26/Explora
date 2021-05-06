using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Explora.Web.Models
{
    /// <summary>
    /// Model for SelectFilePartialView
    /// </summary>
    public class SelectFileViewModel
    {
        public string Method { get; set; }

        public string Action { get; set; }

    }
}
