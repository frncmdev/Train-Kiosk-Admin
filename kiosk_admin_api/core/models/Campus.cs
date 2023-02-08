using System;
using System.Collections.Generic;

namespace core.models;

public partial class Campus
{
    public int CampusId { get; set; }

    public string CampusName { get; set; } = null!;

    public bool IsSelected { get; set; }

    public int TrainstationId { get; set; }

    public virtual TrainStation Trainstation { get; set; } = null!;
}
