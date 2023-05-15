using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stavki.Data.Data.Enums;

namespace Stavki.Infrastructure.EF.Domains.Base
{
    public abstract class BaseDomain
    {
        public int Id { get; set; }

        public CityType CityType { get; set; }
    }
}
