using Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIST.NegocioInterface
{
    public partial interface ITccNegocio
    {
        Tcc Save(Tcc entrada);
        List<Tcc> Get();
    }
}
