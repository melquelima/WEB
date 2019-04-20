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
    internal partial class TccNegocio : ITccNegocio
    {
        public Tcc Save(Tcc entrada)
        {
            using (RCDBContext context = new RCDBContext())
            {
                try
                {
                    context.TB_API_TCC.Add(entrada);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    string aux = ex.Message;
                    throw;
                }
            }
            return entrada;
        }
        public List<Tcc> Get()
        {
            using (RCDBContext context = new RCDBContext())
            {
                return context.TB_API_TCC.OrderBy(x => x.Data).ToList();
            }
        }
    }
}
