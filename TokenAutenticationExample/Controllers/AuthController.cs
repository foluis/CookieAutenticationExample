using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TokenAutenticationExample.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController :  ControllerBase
    {
        //Get Anonymous Resource
        [HttpPost("[action]")]
        [AllowAnonymous]
        public string GetValues()
        {
            return "Resource";
        }

        //Get Authorized Resource
        //Log in (authenticate)
        //Get Authorized Resource Rol Admin
        //Get Authorized Resource Rol Supervisor
    }
}