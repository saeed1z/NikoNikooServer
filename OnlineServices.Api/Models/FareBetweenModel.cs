using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace OnlineServices.Api.Models
{
    public class FareBetweenModel
    {
        public string Point { get; set; }
        public string PointA { get; set; }
        public string PointB { get; set; }
    }
}