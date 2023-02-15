using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace core.models.RequestResults
{
    public class CampusDTO
    {
        public int CampusId { get; set; }

        public string CampusName { get; set; } = null!;

        public int TrainstationId { get; set; }

    }
}