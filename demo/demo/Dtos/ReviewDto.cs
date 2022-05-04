using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using demo.Models;

namespace demo.Dtos
{
    public class ReviewDto
    {
        public Review review { get; set; }
        public int orderId { get; set; }
    }
}