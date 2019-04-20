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
    public class CPF_OSNegocio
    {

        public string Get_Cliente_Os(string OS)
        {
            string saida = "";
            try
            {
                using (RCDBContext context = new RCDBContext())
                {
                    saida = context.TB_API_PEDIDOS.Where(u => u.OS == OS).FirstOrDefault().CPF.ToString();
                }
               
            }
            catch (Exception)
            {
            }

            return saida;
        }
    }
}
