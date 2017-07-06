﻿using System.Collections.Generic;
using Funkmap.Data.Entities;

namespace Funkmap.Data.Parameters
{
    public class MusicianFilterParameter
    {
        public InstrumentType Instrument { get; set; }
        public ExpirienceType Expirience { get; set; }
        public List<Styles> Styles { get; set; }
    }
}
