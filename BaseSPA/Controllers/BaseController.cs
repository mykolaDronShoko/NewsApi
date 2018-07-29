using DbContextSPA.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ModelsSPA.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseSPA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {


        protected ExampleDbContext _context { get; set; }
        protected RoleManager<IdentityRole> _roleManager { get; set; }
        protected UserManager<ApplicationUser> _userManager { get; set; }
        protected IConfiguration _configuration { get; set; }

        public BaseController(
             ExampleDbContext context,
             RoleManager<IdentityRole> roleManager,
             UserManager<ApplicationUser> userManager,
             IConfiguration configuration
            )
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }
        
    }
}
