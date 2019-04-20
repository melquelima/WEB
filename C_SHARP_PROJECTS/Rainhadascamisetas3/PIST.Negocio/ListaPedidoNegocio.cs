using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidade;
using System;
using System.Data.Entity;
using PIST.Negocio;

namespace PIST.Negocio
{
    public class ListaPedidoNegocio
    {

        public List<Pedido> Get(string OS)
        {
            List<Pedido> saida = null;
            using (RCDBContext context = new RCDBContext())
            {
                saida = context.TB_API_PEDIDOS.Where(u=> u.OS == OS).ToList();
            }
            return saida;
        }

        public Boolean Delete(string OS)
        {

            try
            {
                using (RCDBContext context = new RCDBContext())
                {
                    var object_update = context.TB_API_PEDIDOS.Where(u => u.OS == OS);

                    if (object_update != null)
                    {
                        foreach (var x in object_update)
                        {
                            context.Entry(x).State = EntityState.Deleted;
                        }
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
