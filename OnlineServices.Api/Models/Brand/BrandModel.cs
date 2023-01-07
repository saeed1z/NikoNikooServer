﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Api.Models
{
    public class BrandModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string EnTitle { get; set; }
        public string Description { get; set; }
        public string BrandFile { get; set; }
        public bool IsActive { get; set; }
        
    }
}