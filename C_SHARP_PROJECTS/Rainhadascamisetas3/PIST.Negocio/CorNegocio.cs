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
    internal partial class CorNegocio : ICorNegocio
    {
        public List<Cor> Get()
        {
            List<Cor> saida = new List<Cor>();
            using (RCDBContext context = new RCDBContext())
            {
                saida = context.TB_API_CORES.ToList();
            }
            return saida;
        }
        public Cor Save(Cor entrada)
        {
            using (RCDBContext context = new RCDBContext())
            {
                context.TB_API_CORES.Add(entrada);
                context.SaveChanges();
                return entrada;
            }
        }
        public Cor Delete(Cor cor)
        {

            using (RCDBContext context = new RCDBContext())
            {
                var object_update = context.TB_API_CORES.Where(u => u.COR == cor.COR);

                if (object_update != null)
                {
                    foreach (var x in object_update)
                    {
                        context.Entry(x).State = EntityState.Deleted;
                    }
                    context.SaveChanges();
                }
            }
            return cor;
        }
    }
}
