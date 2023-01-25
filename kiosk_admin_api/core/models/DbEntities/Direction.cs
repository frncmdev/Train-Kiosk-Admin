using System;
using System.Collections.Generic;

namespace core.models.DbEntities;

public partial class Direction
{
    public int DirectionId { get; set; }

    public string? DirectionName { get; set; }

    public int? TrainstationId { get; set; }

    public virtual TrainStation? Trainstation { get; set; }
}
