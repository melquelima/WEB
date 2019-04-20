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
    internal partial class PagosNegocio : IPagosNegocio
    {
        public List<Pago> Get()
        {
            using (RCDBContext context = new RCDBContext())
            {
                return context.TB_API_NOTAS_PAGAS.ToList();
            }
        }

        public Pago Save(List<Pago> entrada)
        {
            using (RCDBContext context = new RCDBContext())
            {
                try
                {
                    for (int i = 0; i < entrada.Count(); i++)
                    {
                        context.TB_API_NOTAS_PAGAS.Add(entrada[i]);
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

        public Boolean Delete(List<Pago> itens)
        {
            try
            {
                using (RCDBContext context = new RCDBContext())
                {
                    foreach (var i in itens)
                    {
                        var object_update = context.TB_API_NOTAS_PAGAS.Where(u => u.ID == i.ID);
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
