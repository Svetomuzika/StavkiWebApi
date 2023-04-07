using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stavki.Infrastructure.EF.Domains.Base
{
    public class BaseStavkiDomain : BaseDomain
    {
        public string City { get; set; }

        public int? Distance { get; set; }
    }
}
