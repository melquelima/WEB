using Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIST.NegocioInterface
{
    public partial interface IOsNegocio
    {

        String Get();
        Pedido Save(List<Pedido> entrada);
        string Update_status(string Os, string entrada,DateTime data);
        List<Pedido> Get_pedidos(string OS);
        void Delete(string OS);
    }
}
