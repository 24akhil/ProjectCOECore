using WeatherAppApi.Models;
using WeatherAppApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using WeatherAppApi.Controllers;
using System.Web.Http;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Net.Mail;

namespace WeatherAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherAppUserController : ControllerBase
    {
        private readonly WeatherService _weatherService;
       


        public WeatherAppUserController(WeatherService weatherservice)
        {
            _weatherService = weatherservice;
        }
         
        /// <summary>
         ///Get Detail Of All Users.
          /// </summary> 
        [HttpGet,Route("AllUser")]
       // [Authorize]
        public ActionResult<List<WeatherAppUser>> Get() 
        {
          // Console.WriteLine("Hi");
          // Console.WriteLine(c.Type);
          //  string userId = User.Claims.FirstOrDefault(c => c.Type == "Email").Value;
           // Console.WriteLine("hellow"+userId);
            
           return  _weatherService.Get();
        }

         [HttpGet("{Email}")]
         public ActionResult<WeatherAppUser> GetUserByEmail(string Email)
         {  
              Console.WriteLine(Email);
             var weatherappuser = _weatherService.Get(Email);

             if (weatherappuser == null)
             {
                 return NotFound();
             }
                              
             return weatherappuser;
         }

         [HttpPost, Route("SignUp")]  
        public  ActionResult CreateUser(LoginResponse login)
        {
          
            string Token="";

            var weatherappuser1 = _weatherService.Get(login.Email);
          //  Console.WriteLine("After Check"+login.Email);
            if (weatherappuser1==null)
            {      
                if(IsValidEmail(login.Email))
                {  
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("abhishek.anandjpr@gmail.com");
                mail.To.Add(login.Email);
                mail.Subject = "Signup Confirmation from The WeatherMan App";
                mail.Body = "This is for testing SMTP mail from GMAIL";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("abhishek.anandjpr@gmail.com", "ABhi0912");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);

                Console.WriteLine("mail Send");
           
                 _weatherService.CreateUser(login);
                     AuthController obj=new AuthController();
                    Token=obj.Login(login.Email);
                     return Ok(new {
                Email = login.Email,
                FirstName=login.FirstName,
                LastName=login.LastName,
                Token = Token
               });
                }
                else
                {
                    return BadRequest(new { message = "Email Does not Exists please tru with different Email" });
                }
            }
            else
            {
                 WeatherAppUser weatherappuserexists=new WeatherAppUser();   
                  return BadRequest(new { message = "UserName Already Exists" }); 
                                                 
            }
            
           // return weatherappuser;
        }

        [HttpPost, Route("login")]  
        public ActionResult CreateLogin(LoginResponse login)
        {   
           
           string Token="";
             Console.WriteLine(login.Email);
            var weatherappuser1 = _weatherService.GetByName(login.Email);
             Console.WriteLine(login.Password);
            var weatherappuser2 = _weatherService.GetByName1(login.Password);
            if ((weatherappuser1 == null ||  weatherappuser2==null ))
             {
                 Console.WriteLine("Already Exists");
                return BadRequest(new { message = "Username or password is incorrect" });
             }   
             else
             {
                 
            AuthController obj=new AuthController();
             Token=obj.Login(login.Email);
             }
           return Ok(new {
                Email = weatherappuser1.Email,
                FirstName=weatherappuser1.FirstName,
                LastName=weatherappuser1.LastName,
                Token = Token
            });
        }



        bool IsValidEmail(string email)
         {
       try {
                var addr = new System.Net.Mail.MailAddress(email);
        return true;
    }
    catch {
        return false;
    }
       }
       // [AllowAnonymous]
        


        // [HttpPut("{Email}")]
        // public IActionResult Update(string Email, WeatherAppUser weatherappuserIn)
        // {
        //     var weatherappuser = _weatherService.Get(Email);

        //     if (weatherappuser == null)
        //     {
        //         return NotFound();
        //     }

        //     _weatherService.Update(Email, weatherappuserIn);

        //     return NoContent();
        // }

        // [HttpDelete("{Email:length(24)}")]
        // public IActionResult Delete(string Email)
        // {
        //     var weatherappuser = _weatherService.Get(Email);

        //     if (weatherappuser == null)
        //     {
        //         return NotFound();
        //     }

        //     _weatherService.Remove(weatherappuser.Email);

        //     return NoContent();
        // }     

        //  [HttpPost]
        // [Route("Login1")]
        // //POST : /api/ApplicationUser/Login
        // public async Task<IActionResult> Login(LoginResponse login)
        // {
        //     // var key = Encoding.UTF8.GetBytes("1234567891234567");
        //    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567891234567")); 
        //      var weatherappuser1 = _weatherService.GetByName(login.Email);
        //     if (weatherappuser1 != null )
        //     {
        //         var tokenDescriptor = new SecurityTokenDescriptor
        //         {
        //             Subject = new ClaimsIdentity(new Claim[]
        //             {
        //                 new Claim("Email",weatherappuser1.Email.ToString())
                        
        //             }),
        //             Expires = DateTime.UtcNow.AddMinutes(5),
        //              SigningCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
        //         };
        //         var tokenHandler = new JwtSecurityTokenHandler();
        //         var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        //         var token = tokenHandler.WriteToken(securityToken);
        //        // string userId = User.Claims.First(c => c.Type == "UserID").Value;
        //        // Console.WriteLine("Hi"+userId);
        //         return Ok(new { token });
        //     }
        //     else
        //         return BadRequest(new { message = "Username or password is incorrect." });
        // }


        // [HttpGet]
        // [Authorize]
        // //GET : /api/UserProfile
        // public string GetUserProfile() {
        //     Console.WriteLine("Hi");
        //     string userId = User.Claims.First(c => c.Type == "UserID").Value;
        //     Console.WriteLine("Hi"+userId);
        //    // var user = await _userManager.FindByIdAsync(userId);
        //    // return new
        //    // {
        //      //    user.FullName,
        //     //     user.Email,
        //     //     user.UserName
        //    // };
        //    return "Abhi";
        // }
        
    }
}