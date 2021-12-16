﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MovieAPI.Models
{ /// <summary>
  /// Класс Movie 
  /// Содержит данные контекса
  /// </summary>
    public class Movie: MongoDocument
    {
        public string Name { get; set; }
        public List<string> Actors { get; set; }
        public decimal? Budget { get; set; }
        public string Description { get; set; }
    }
}
