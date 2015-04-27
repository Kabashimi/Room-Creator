using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maciej_Galos
{
    class MGTextureViewModel
    {
        private List<MGTexture> textures = new List<MGTexture>();
        public List<MGTexture> Textures
        {
            get
            {
                return textures;
            }
        }

        public MGTextureViewModel(List<MGTexture> list)
        {
            textures = list;
        }
        public MGTextureViewModel()
        {
            
        }
        public void AddToList(MGTexture texture)
        {
            textures.Add(texture);
        }
    }
}
