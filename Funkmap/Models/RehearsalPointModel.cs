﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funkmap.Models
{

    public class RehearsalPointModel
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public byte[] Avatar { get; set; }
        public string WorkingHoursDescription { get; set; }
        public string VkLink { get; set; }
        public string YouTubeLink { get; set; }
        public string FacebookLink { get; set; }
        public string SoundCloudLink { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }


    public class RehearsalPointPreviewModel
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public byte[] Avatar { get; set; }
        public string WorkingHoursDescription { get; set; }
        public string VkLink { get; set; }
        public string YouTubeLink { get; set; }
        public string FacebookLink { get; set; }
    }
}
