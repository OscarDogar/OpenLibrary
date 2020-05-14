using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenLibrary.Web.Data;
using OpenLibrary.Web.Data.Entities;

namespace OpenLibrary.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly DataContext _context;

        public GenderController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Gender
        [HttpGet]
        public IEnumerable<GenderEntity> GetGenders()
        {
            return _context.Genders;
        }

    }
}