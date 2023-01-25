using System;
using System.Collections.Generic;

namespace core.models.DbEntities;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}
