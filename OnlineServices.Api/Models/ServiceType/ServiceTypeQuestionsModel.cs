﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Api.Models
{
    public class ServiceTypeQuestionsModel
    {
        public int Id { get; set; }
        public string ServiceTypeTitle { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

    }
}
