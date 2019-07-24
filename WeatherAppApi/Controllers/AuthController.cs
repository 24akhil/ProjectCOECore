using WeatherAppApi.Models;
using WeatherAppApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel;
using System.IdentityModel.Tokens;
using System;
using System.Globalization;
using System.Security.Claims;
using  System.IdentityModel.Tokens.Jwt;
using  System.Text;

namespace WeatherAppApi.Controllers
{
  //  [Route("api/[controller]")]
  //  [ApiController]  
      public class AuthController : ControllerBase
    {  
         
      //  public  ActionResult Login()  
        public  string Login(string username)
        {  
                 var now = DateTime.UtcNow;
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeyForSignInSecret@1234"));  
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);  
  
                var token_Handler = new JwtSecurityTokenHandler();
                var securitytokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                        {
                            new Claim("UserID", username)
                        }),
                
                Expires = now.AddDays(Convert.ToInt32(1)),
                //  signingCredentials = signinCredentials  
                SigningCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
            };


                var tokeOptions = new JwtSecurityToken(  
                   // issuer: "http://localhost:5000",  
                   // audience: "http://localhost:5000",  
                   // 
                   // claims: new List<Claim>(),  
                  //  expires: DateTime.Now.AddDays(1),  
                   // signingCredentials: signinCredentials  
              );  
                    var stoken = token_Handler.CreateToken(securitytokenDescriptor);

                   var token = token_Handler.WriteToken(stoken);

             //  var tokenString = token_Handler.WriteToken(tokeOptions); 
              //  return tokenString;

               return token;
               // return Ok(new { Token = token });  
          
        }       
    }  
}