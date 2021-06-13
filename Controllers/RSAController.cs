using HotelServer.Models;
using HotelServer.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HotelServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RSAController : ControllerBase
    {
        [HttpPost]
        public string rsa(Keys key) 
        {
            RSAservice rsa = new RSAservice(key.key);

            

            return rsa.GetPublicKey();
        }


        [HttpGet]
        public string rsa()
        {

            return "test";
        }

    }
}
