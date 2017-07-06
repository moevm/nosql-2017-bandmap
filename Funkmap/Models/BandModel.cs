﻿using System.Collections.Generic;
using Funkmap.Data.Entities;

namespace Funkmap.Models
{
    public class BandModel
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public ICollection<Styles> Styles { get; set; }
        public ICollection<InstrumentType> DesiredInstruments { get; set; }
        public ICollection<string> VideoLinks { get; set; }
        public ICollection<string> Musicians { get; set; } //todo на превью
    }

    public class BandModelPreview
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public byte[] Avatar { get; set; }

        public ICollection<Styles> Styles { get; set; }
        public string Description { get; set; }

        public string VkLink { get; set; }
        public string YouTubeLink { get; set; }
        public string FacebookLink { get; set; }

        public ICollection<InstrumentType> DesiredInstruments { get; set; }
    }
}
