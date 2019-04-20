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
    public class Usuario2Negocio
    {

        public User Get(string doc)
        {
            using (RCDBContext context = new RCDBContext())
            {
                return context.TB_API_USUARIOS.Where(u => u.CPF == doc).FirstOrDefault();
            }
        }
    }
}
