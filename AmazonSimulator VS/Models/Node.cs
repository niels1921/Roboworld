using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Node
    {
        /// <summary>
        /// Node id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// X waarde van de node(breedte)
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// Y waarde van de node(hoogte)
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// Z waarde van de node(lengte)
        /// </summary>
        public double Z { get; set; }
        /// <summary>
        /// De shelf die op de specifieke node staat
        /// </summary>
        public Shelf Shelf { get; set; }
        /// <summary>
        /// Geeft door of er een shelf staat in de vorm van een bool
        /// </summary>
        public bool ShelfStatus { get; set; }
    }
}
