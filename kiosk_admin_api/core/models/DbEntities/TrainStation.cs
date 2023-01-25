using System;
using System.Collections.Generic;

namespace core.models.DbEntities;

public partial class TrainStation
{
    public int TrainstationId { get; set; }

    public string? TrainstationName { get; set; }

    public virtual ICollection<Direction> Directions { get; } = new List<Direction>();
}
