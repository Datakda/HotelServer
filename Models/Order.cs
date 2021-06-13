using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServer.Models
{
    public class Order
    {

        [Key]
        public int Id { set; get; }
        public DateTime StartDate { set; get; }

        public DateTime EndDate { set; get; }

        public string UserName { set; get; }


    }
}
