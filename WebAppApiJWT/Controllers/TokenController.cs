using JwtMiddleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebAppApiJWT.Model;

namespace WebAppApiJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IConfiguration _config;
        private readonly MyJwtService myJwtService;

        public TokenController(IConfiguration config, MyJwtService myJwtService)
        {
            _config = config;
            this.myJwtService = myJwtService;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public JsonResult Echo() {
            var user = this.User.Identities.First().Claims.FirstOrDefault();
            return new JsonResult( new { user = user.Value, time = DateTime.Now });
        }    
        [HttpPost("login")]
        [AllowAnonymous]
        public JsonResult GetRandomToken (string username, string password)
        {
            var jwt = this.myJwtService;

            if (username == "gsantos" && password == "password")
            {
                var token = jwt.GenerateSecurityToken("gabrielosantos@email.com");
                return new JsonResult(new UserResponse { OK = true, Data = new  UserLogin{ Token = token, Expire= 3200, Email ="gsantos@ondiss.com.ar",UserName="gsantos" } });
            }


            return new JsonResult(new {token="", ok=false,message="no autorizado"});
        }
    }
}
    