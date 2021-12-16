using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncNode.Settings
{
   public interface IMovieAPISettings
    {
        public string[] Hosts { get; set; }
    }
}
