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
    public partial class ArtistaNegocio //: IEntradaInterface
    {
        public List<Artistas> Get()
        {
            List<Artistas> saida = new List<Artistas>();
            using (RCDBContext context = new RCDBContext())
            {
                saida = context.TBL_ARTISTAS.ToList();
            }
            return saida;
        }
        public Artistas Get(String CPF)
        {
            using (RCDBContext context = new RCDBContext())
            {
                return context.TBL_ARTISTAS.Where(u => u.CPF == CPF).FirstOrDefault();
            }
        }
        public List<ArtistasVW> GetVW()
        {
            List<ArtistasVW> saida = new List<ArtistasVW>();
            using (RCDBContext context = new RCDBContext())
            {
                saida = context.TBL_ARTISTASVW.ToList();
            }
            return saida;
        }
        public ArtistasVW GetVW(String CPF)
        {
            using (RCDBContext context = new RCDBContext())
            {
                return context.TBL_ARTISTASVW.Where(u => u.CPF == CPF).FirstOrDefault();
            }
        }
        public void Save(List<Artistas> artistas)
        {
            using (RCDBContext context = new RCDBContext())
            {
                foreach (var artista in artistas)
                {
                    context.TBL_ARTISTAS.Add(artista);
                }
                context.SaveChanges();
            }
        }
        public void Delete(List<Artistas> artistas)
        {
            ObraNegocio ObraNegocio = new ObraNegocio();
            ObraNegocio.Delete(artistas);

            using (RCDBContext context = new RCDBContext())
            {
                foreach (var artista in artistas)
                {
                    var object_update = context.TBL_ARTISTAS.Where(u => u.CPF == artista.CPF);

                    if (object_update != null)
                    {
                        foreach (var x in object_update)
                        {
                            context.Entry(x).State = EntityState.Deleted;
                        }
                    }
                }
                context.SaveChanges();
            }
        }
    }
}

