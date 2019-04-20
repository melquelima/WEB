using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidade;
using System;
using System.Data.Entity;
using PIST.Negocio;
using PIST.NegocioInterface;

namespace PIST.Negocio
{
    internal partial class OsNegocio : IOsNegocio
    {
        public String Get()
        {
            String saida = null;
            using (RCDBContext context = new RCDBContext())
            {
                saida = context.TB_API_OS.FirstOrDefault().OS.ToString();
            }
            return saida;
        }

        public void Delete(string OS)
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
        }

        public List<Pedido> Get_pedidos(string OS)
        {
            List<Pedido> saida = new List<Pedido>();
            using (RCDBContext context = new RCDBContext())
            {
                saida = context.TB_API_PEDIDOS.Where(u => u.OS == OS).ToList();
            }
            return saida;
        }


        public Pedido Save(List<Pedido> entrada)
        {
            using (RCDBContext context = new RCDBContext())
            {
                try
                {
                    for (int i = 0; i < entrada.Count();i++) {
                        context.TB_API_PEDIDOS.Add(entrada[i]);
                    }
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    string aux = ex.Message;
                    throw;
                }
            }
            return entrada[0];
        }
        public string Update_status(string Os,string entrada,DateTime data)
        {
            using (RCDBContext context = new RCDBContext())
            {
                var object_update = context.TB_API_PEDIDOS.Where(u => u.OS == Os);

                if (object_update != null)
                {
                    foreach (var x in object_update)
                    {
                        context.Entry(x).Property(u => u.STATUS).CurrentValue = entrada;
                        context.Entry(x).Property(u => u.DATA_ENTREGA).CurrentValue = data;
                    }
                    context.SaveChanges();
                }
                else
                {
                    entrada = null;
                }
            }

            return entrada;

        }
    }
}
