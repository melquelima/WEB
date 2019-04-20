using Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIST.NegocioInterface
{
    public partial interface IPagosNegocio
    {

        List<Pago> Get();
        Pago Save(List<Pago> entrada);
        Boolean Delete(List<Pago> itens);
    }
}
