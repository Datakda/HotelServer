using HotelServer.Models;
using HotelServer.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace HotelServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private ApplicationContext db;
        public OrderController(ApplicationContext context)
        {
            db = context;
        }

        [HttpPost]
        public string AddOrder(Crypt cr) 
        {
            RSAservice rsa = new RSAservice();

            var res = rsa.Decrypt(cr.code);

            Order order = JsonSerializer.Deserialize<Order>(res);

            db.Orders.Add(order);
            db.SaveChanges();


            return rsa.Encrypt("ЭТО БЫЛО СЛОЖНО!1111");
                
        }


       

    }
}
