using ArandaLogic.General;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace ArandaWebApi.Controllers
{
    public class AuthenticationController : ApiController
    {
        [HttpPost]
        public IHttpActionResult GetToken([FromBody] LoginToken userData)
        {
            //IGeneral generalM;
            TokenLogic TLogic = new TokenLogic();

            if (TLogic.ValidatedCredials(userData))
            {
                var key = ConfigurationManager.AppSettings["JwtKey"];
                var issuer = ConfigurationManager.AppSettings["JwtIssuer"];

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                //Create a List of Claims, Keep claims name short    
                var permClaims = new List<Claim>();
                permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                permClaims.Add(new Claim("userid", "userId"));

                //Create Security Token object by giving required parameters    
                var token = new JwtSecurityToken(issuer, //Issure    
                                issuer,  //Audience    
                                permClaims,
                                expires: DateTime.Now.AddHours(1),
                                signingCredentials: credentials);
                var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(jwt_token);
            }
            else
                return BadRequest("Credenciales incorrectas");
        }

    }
}