using Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIST.NegocioInterface
{
    public partial interface IUsuarioNegocio2
    {

        List<User> Get();
        User Get(string doc);
        User Save(User entrada);
    }
}
