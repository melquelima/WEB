using Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace padrao.Interface
{
    public partial interface IEntradaInterface
    {
        List<Artistas> Get();
    }
}