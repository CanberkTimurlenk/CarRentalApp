﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Models
{
    public class CarImage : IImageEntity
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }



    }
}