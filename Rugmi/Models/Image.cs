using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rugmi.Models
{
    public class Image
    {
        public int Id { get; set; }

        // name of the image
        public string Name { get; set; }


        // UPLOAD: this is the Url of the image to put into the source tag
        public string Url { get; set; }
    }
}
