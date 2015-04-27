using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maciej_Galos
{
    class MGTexture
    {
        public string name { get; set; }
        public string adress { get; set; }

        public MGTexture(string _name)
        {
            name = _name;
            adress = "Maciej_Galos/Resources/" + name + ".jpg";
        }
    }
}
