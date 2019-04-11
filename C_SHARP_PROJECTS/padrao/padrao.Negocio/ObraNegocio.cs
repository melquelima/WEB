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
    public partial class ObraNegocio
    {
        public List<Obras> Get()
        {
            List<Obras> saida = new List<Obras>();
            using (RCDBContext context = new RCDBContext())
            {
                saida = context.TBL_OBRAS.ToList();
            }
            return saida;
        }
        public List<Obras> Get(String CPF)
        {
            using (RCDBContext context = new RCDBContext())
            {
                return context.TBL_OBRAS.Where(u => u.CPF == CPF).ToList();
            }
        }
        public List<Obras> GetByID(int ID)
        {
            using (RCDBContext context = new RCDBContext())
            {
                return context.TBL_OBRAS.Where(u => u.ID_OBRA == ID).ToList();
            }
        }
        public List<Obras> Get(List<Artistas> artistas)
        {
            using (RCDBContext context = new RCDBContext())
            {
                List<Obras> obras = new List<Obras>();
                foreach (var artista in artistas)
                {
                    obras.AddRange(context.TBL_OBRAS.Where(u => u.CPF == artista.CPF).ToList());
                }

                return obras;
            }
        }
        public List<ObrasVW> GetVW()
        {
            List<ObrasVW> saida = new List<ObrasVW>();
            using (RCDBContext context = new RCDBContext())
            {
                saida = context.TBL_OBRASVW.ToList();
            }
            return saida;
        }
        public List<ObrasVW> GetVW(String CPF)
        {
            using (RCDBContext context = new RCDBContext())
            {
                return context.TBL_OBRASVW.Where(u => u.CPF == CPF).ToList();
            }
        }
        public void Save(List<Obras> obras)
        {
            using (RCDBContext context = new RCDBContext())
            {
                foreach (var obra in obras)
                {
                    context.TBL_OBRAS.Add(obra);
                }
                context.SaveChanges();
            }
        }
        //DELETE OBRAS COM OBRAS POR PARAMETRO
        public void Delete(List<Obras> obras)
        {
            AcervoNegocio AcervoNegocio = new AcervoNegocio();
            ArtPicturesNegocio ArtPicturesNegocio = new ArtPicturesNegocio();
            AcervoNegocio.DeleteByObra(obras);
            ArtPicturesNegocio.DeleteByObra(obras);
            using (RCDBContext context = new RCDBContext())
            {
                foreach (var obra in obras)
                {
                    var object_update = context.TBL_OBRAS.Where(u => u.CPF == obra.CPF);

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
        //DELETE TODAS AS ORBAS DE UM ARTISTA
        public void Delete(List<Artistas> artistas)
        {
            AcervoNegocio AcervoNegocio = new AcervoNegocio();
            foreach (var artista in artistas)
            {
                List<Obras> obras = Get(artista.CPF);
                AcervoNegocio.DeleteByObra(obras);
                this.Delete(obras);
            }
        }
    }
}

