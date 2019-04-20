using Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIST.NegocioInterface
{
    public partial interface IPrecosNegocio
    {
        List<Precos> Get();
        List<PrecosDetalhado> Detalhado();
    }
}
