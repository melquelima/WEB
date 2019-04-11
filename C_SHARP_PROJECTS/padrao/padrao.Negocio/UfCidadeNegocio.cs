using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidade;
using System;
using System.Data.Entity;
using padrao.Interface;

namespace padrao.Negocio
{
    public partial class UfCidadeNegocio
    {
        public List<UfCidade> Get()
        {
            List<UfCidade> saida = new List<UfCidade>();
            using (RCDBContext context = new RCDBContext())
            {
                saida = context.TBL_UF_CIDADE.ToList();
            }
            return saida;
        }
    }
}

