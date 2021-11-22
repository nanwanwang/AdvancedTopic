﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChannelSample
{
    internal class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Age { get; set; }

        public override string ToString()
        {
           return JsonSerializer.Serialize(this);   
        }
    }
}
