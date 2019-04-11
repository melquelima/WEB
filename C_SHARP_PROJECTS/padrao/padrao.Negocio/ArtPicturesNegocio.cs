using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidade;
using System;
using System.Web;
using System.Data.Entity;
using padrao.Interface;
using System.IO;

namespace padrao.Negocio
{
    public partial class ArtPicturesNegocio
    {
        public List<ArtPictures> Get()
        {
            List<ArtPictures> saida = new List<ArtPictures>();
            using (RCDBContext context = new RCDBContext())
            {
                saida = context.TBL_ART_PICTURES.ToList();
            }
            return saida;
        }
        public List<ArtPictures> GetByObra(int ID_OBRA)
        {
            using (RCDBContext context = new RCDBContext())
            {
                return context.TBL_ART_PICTURES.Where(u => u.ID_OBRA == ID_OBRA).ToList();
            }
        }
        public List<ArtPictures> GetByArte(int ID_ARTE)
        {
            using (RCDBContext context = new RCDBContext())
            {
                return context.TBL_ART_PICTURES.Where(u => u.ID == ID_ARTE).ToList();
            }
        }
        public List<ArtPictures> Get(List<Obras> obras)
        {
            using (RCDBContext context = new RCDBContext())
            {
                List<ArtPictures> art = new List<ArtPictures>();
                foreach (var obra in obras)
                {
                    art.AddRange(context.TBL_ART_PICTURES.Where(u => u.ID_OBRA == obra.ID_OBRA).ToList());
                }
                return art;
            }
        }
        public void Save(List<ArtPictures> artes)
        {
            using (RCDBContext context = new RCDBContext())
            {
                foreach (var arte in artes)
                {
                    context.TBL_ART_PICTURES.Add(arte);
                }
                context.SaveChanges();
            }
        }
        public void Save(ArtPictures arte)
        {
            using (RCDBContext context = new RCDBContext())
            {
                context.TBL_ART_PICTURES.Add(arte);

                context.SaveChanges();
            }
        }
        public void Delete()
        {
            using (RCDBContext context = new RCDBContext())
            {
                var object_update = context.TBL_ART_PICTURES;

                if (object_update != null)
                {
                    foreach (var x in object_update)
                    {
                        context.Entry(x).State = EntityState.Deleted;
                    }
                }
                context.SaveChanges();
            }
        }
        public void Delete(List<ArtPictures> artes)
        {
            using (RCDBContext context = new RCDBContext())
            {
                foreach (var arte in artes)
                {
                    var object_update = context.TBL_ART_PICTURES.Where(u => u.ID == arte.ID);

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
        public void DeleteByObra(List<Obras> obras)
        {
            using (RCDBContext context = new RCDBContext())
            {
                foreach (var obra in obras)
                {
                    var object_update = context.TBL_ART_PICTURES.Where(u => u.ID_OBRA == obra.ID_OBRA);

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
        public void DeleteByObra(int ID_OBRA)
        {
            using (RCDBContext context = new RCDBContext())
            {
               
                var object_update = context.TBL_ART_PICTURES.Where(u => u.ID_OBRA == ID_OBRA);

                if (object_update != null)
                {
                    foreach (var x in object_update)
                    {
                        context.Entry(x).State = EntityState.Deleted;
                    }
                    context.SaveChanges();
                }
            }
        }
        public void DeleteByArte(int ID_ARTE)
        {
            using (RCDBContext context = new RCDBContext())
            {

                var object_update = context.TBL_ART_PICTURES.Where(u => u.ID == ID_ARTE);

                if (object_update != null)
                {
                    foreach (var x in object_update)
                    {
                        context.Entry(x).State = EntityState.Deleted;
                    }
                    context.SaveChanges();
                }
            }
        }
    }

    
}

