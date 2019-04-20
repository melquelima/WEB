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
    public partial class VisitantesNegocio
    {
        public List<Visitantes> Get()
        {
            List<Visitantes> saida = null;
            using (RCDBContext context = new RCDBContext())
            {
                saida = context.TB_API_VISITANTES.ToList();
            }
            return saida;
        }

        public void atd(DateTime data)
        {
            using (RCDBContext context = new RCDBContext())
            {
                var object_update = context.TB_API_VISITANTES.Where(u => u.DATA == data.Date).FirstOrDefault();

                if (object_update != null)
                {
                    var valor = context.Entry(object_update).Property(u => u.QTD).CurrentValue;
                    context.Entry(object_update).Property(u => u.QTD).CurrentValue = valor + 1;
                }
                else
                {
                    Visitantes x = new Visitantes();
                    x.DATA = data.Date;
                    x.QTD = 1;
                    context.TB_API_VISITANTES.Add(x);
                }
                context.SaveChanges();
            }

        }
    }
}
