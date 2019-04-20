using Entidade;
using PIST.NegocioInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIST.Negocio
{
    internal partial class DevedoresOsNegocio : IDevedoresOsNegocio
    {
        public List<DevedoresOs> Get()
        {
            List<DevedoresOs> saida = new List<DevedoresOs>();
            using (RCDBContext context = new RCDBContext())
            {
                saida = context.TB_API_DEVEDORES_OS.ToList();
            }
            return saida;
        }
        public List<DevedoresOs> Get(string CPF)
        {
            List<DevedoresOs> saida = new List<DevedoresOs>();
            using (RCDBContext context = new RCDBContext())
            {
                saida = context.TB_API_DEVEDORES_OS.Where(u => u.CLIENTE_CPF == CPF).ToList();
            }
            return saida;
        }

        

    }
}
