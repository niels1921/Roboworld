using System;
using System.Collections.Generic;
using System.Linq;

namespace Models {
    interface IUpdatable
    {
        /// <summary>
        /// Blauwdruk voor de update methode voor alle modellen die updatable zijn
        /// </summary>
        /// <param name="tick">aantal scherm updates per minuut</param>
        /// <returns>int</returns>
        bool Update(int tick);
    }
}
