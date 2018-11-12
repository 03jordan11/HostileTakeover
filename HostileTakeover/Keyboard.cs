using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostileTakeover {

    public class Keyboard {

        private Dictionary<System.Windows.Forms.Keys, Boolean> keys;

        public Keyboard() {
            keys = new Dictionary<System.Windows.Forms.Keys, Boolean>();
        }

        public void setKey(System.Windows.Forms.Keys key, Boolean pressed) {
            keys[key] = pressed;
        }

        public Boolean isDown(System.Windows.Forms.Keys key) {
            if (keys.ContainsKey(key))
                return keys[key];
            return false;
        }

    }
}
