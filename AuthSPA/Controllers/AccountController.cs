using BaseSPA.Controllers;
using DbContextSPA.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ModelsSPA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthSPA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {

        public AccountController(
             ExampleDbContext context,
             RoleManager<IdentityRole> roleManager,
             UserManager<ApplicationUser> userManager,
             IConfiguration configuration

            ) : base(context, roleManager, userManager, configuration)
        {

        }

        [HttpPost("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AddUser([FromBody]UserViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                //if (!await _roleManager.RoleExistsAsync("User"))
                //{
                //    await _roleManager.CreateAsync(new IdentityRole("User"));
                //    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                //}
                user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    UserImg = model.imgUrl
                };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    ICollection<string> role = await _userManager.GetRolesAsync(user);
                    return new JsonResult(role);
                }
                else return BadRequest(result.Errors);

            }
            else
            {
                ICollection<string> role = await _userManager.GetRolesAsync(user);
                return new JsonResult(role);
            }
        }

        [HttpGet]
        public string Add()
        {
            return "Add";
        }
    }
}
