using OpenLibrary.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenLibrary.Web.Models
{
    public class ChangeRolViewModel
    {
        public string Email { get; set; }
        public string Name { get; set; }

        public UserType Rol { get; set; }

    }
}
