using System.IO;
using static HostileTakeover.Domains.Structures;

namespace HostileTakeover.Domains
{
    public class BaseObject
    {
        public string Name { get; set; }
        public FileStream Sprite { get; set; }
        public bool IsStatic { get; set; }
        public Position Position { get; set; }

    }
}
