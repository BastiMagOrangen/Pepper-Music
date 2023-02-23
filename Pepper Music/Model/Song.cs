using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepper_Music.Model
{
    public class Song
    {
        public Song(string path, string name)
        {
            Path = path;
            Name = name;
        }

        public string Path { get; set; }
        public string Name { get; set; }
    }
}
