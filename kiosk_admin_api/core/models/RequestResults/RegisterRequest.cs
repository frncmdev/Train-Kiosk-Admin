using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace core.models.RequestResults
{
    public record RegisterRequest (
        string Username, 
        string Password,
        string AuthedBy
        );
}