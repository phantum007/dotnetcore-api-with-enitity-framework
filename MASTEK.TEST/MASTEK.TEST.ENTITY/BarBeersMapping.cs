using System;
using System.Collections.Generic;

namespace MASTEK.TEST.ENTITY;

public partial class BarBeersMapping
{
    public int BarId { get; set; }

    public int BeerId { get; set; }

    public bool? Isdeleted { get; set; }

    public uint Id { get; set; }

    public virtual Bar? Bar { get; set; }

    public virtual Beer? Beer { get; set; }
}
