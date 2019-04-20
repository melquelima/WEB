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
    internal partial class UsuarioNegocio : IUsuarioNegocio2
    {
        public List<User> Get()
        {
            List<User> saida = new List<User>();
            using (RCDBContext context = new RCDBContext())
            {
                saida = context.TB_API_USUARIOS.ToList();
            }
            return saida;
        }

        public User Get(string doc)
        {
            using (RCDBContext context = new RCDBContext())
            {
                return context.TB_API_USUARIOS.Where(u => u.CPF == doc).FirstOrDefault();
            }
        }

        public User Save(User entrada)
        {
            using (RCDBContext context = new RCDBContext())
            {
                try
                {
                    context.TB_API_USUARIOS.Add(entrada);
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
    }
}
