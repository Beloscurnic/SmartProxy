﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Settings
{
   public interface IMongoDbSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
}
