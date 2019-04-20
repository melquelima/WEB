using Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIST.NegocioInterface
{
    public partial interface ICorNegocio
    {
        List<Cor> Get();
        Cor Delete(Cor cor);
        Cor Save(Cor entrada);
    }
}
