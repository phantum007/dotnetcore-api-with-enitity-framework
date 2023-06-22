using System;
namespace MASTEK.TEST.ENTITY
{
    public partial class BreweryBeersMapping
    {
        public int? BreweryId { get; set; }

        public int? BeerId { get; set; }

        public bool? Isdeleted { get; set; }

        public virtual Beer? Beer { get; set; }

        public virtual Bar? Brewery { get; set; }
    }
}

