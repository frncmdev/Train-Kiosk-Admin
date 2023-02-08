using System;
using System.Collections.Generic;

namespace core.models;

public partial class TrainStation
{
    public int TrainstationId { get; set; }

    public string? TrainstationName { get; set; }

    public int TravelTime { get; set; }

    public virtual ICollection<Campus> Campuses { get; } = new List<Campus>();
}
