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
    public partial class AcervoNegocio
    {
        public List<Acervo> Get()
        {
            List<Acervo> saida = new List<Acervo>();
            using (RCDBContext context = new RCDBContext())
            {
                saida = context.TBL_ACERVO.ToList();
            }
            return saida;
        }
        public List<Acervo> GetByExpo(int ID_EXPOSICAO)
        {
            using (RCDBContext context = new RCDBContext())
            {
                return context.TBL_ACERVO.Where(u => u.ID_EXPOSICAO == ID_EXPOSICAO).ToList();
            }
        }
        public void Save(List<Acervo> acervos)
        {
            using (RCDBContext context = new RCDBContext())
            {
                foreach (var acervo in acervos)
                {
                    context.TBL_ACERVO.Add(acervo);
                }
                context.SaveChanges();
            }
        }
        public void Delete(List<Acervo> acervos)
        {
            using (RCDBContext context = new RCDBContext())
            {
                foreach (var acervo in acervos)
                {
                    var object_update = context.TBL_ACERVO.Where(u => u.ID_ACERVO == acervo.ID_ACERVO);

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
                    var object_update = context.TBL_ACERVO.Where(u => u.ID_OBRA == obra.ID_OBRA);

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

