using Entidade;
using PIST.NegocioInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIST.Negocio
{
    internal partial class PrecosNegocio : IPrecosNegocio
    {
        public List<Precos> Get()
        {
            List<Precos> saida = new List<Precos>();
            using (RCDBContext context = new RCDBContext())
            {
                saida = context.TB_PRECOS.ToList();
            }
            return saida;
        }
        public List<PrecosDetalhado> Detalhado()
        {
            List<PrecosDetalhado> saida = new List<PrecosDetalhado>();
            using (RCDBContext context = new RCDBContext())
            {
                saida = context.TB_PRECOS_DETALHADO.ToList();
            }
            return saida;
        }

    }
}
