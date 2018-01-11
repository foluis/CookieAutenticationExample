using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TokenAutenticationExample.Models;

namespace TokenAutenticationExample.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : ControllerBase
    {
        //Get Anonymous Resource
        [HttpGet("[action]")]
        [AllowAnonymous]
        public string AllowAnonymous()
        {
            return "-AllowAnonymous Resource";
        }

        //Get Authorized Resource
        [HttpGet("[action]")]
        [Authorize]
        public string Authorize()
        {
            return "Authorize Resource";
        }

        //Get Authorized Resource
        [HttpGet("[action]")]
        [Authorize]
        [Authorize(Policy = "SuperUser")]
        public string SuperUser()
        {
            return "Super user Resource";
        }

        //Get Authorized Resource
        [HttpGet("[action]")]
        [Authorize]
        [Authorize(Policy = "MustBeAdmin")]
        public string OnlyAdmins()
        {
            return "Only Admin users Resource";
        }

        //Login (authenticate)
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody]AppUser appUser)
        {
            if (appUser == null)
            {
                return Unauthorized();
            }

            switch (appUser.Email)
            {
                case "foluis@superuser.com":
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Email, appUser.Email),
                            new Claim(ClaimTypes.Role, "SuperUser"),
                            new Claim(ClaimTypes.Role, "Admin")
                        };

                        var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var principal = new ClaimsPrincipal(userIdentity);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        return Ok(new
                        {
                            UserName = appUser.Email
                        });
                    }

                case "foluis@admin.com":
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Email, appUser.Email),
                            new Claim(ClaimTypes.Role, "Admin")
                        };

                        var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var principal = new ClaimsPrincipal(userIdentity);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        return Ok(new
                        {
                            UserName = appUser.Email
                        });
                    }

                default:
                    return Unauthorized();
            }
        }

        //Logout
        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

        //Get Anonymous Resource
        [HttpGet("[action]")]
        [AllowAnonymous]
        public string AccessDeniedPath()
        {
            return "AccessDenied Message";
        }

        //Get Anonymous Resource
        [HttpGet("[action]")]
        [AllowAnonymous]
        public string LoginPath()
        {
            return "LoginPath Message";
        }
    }
}