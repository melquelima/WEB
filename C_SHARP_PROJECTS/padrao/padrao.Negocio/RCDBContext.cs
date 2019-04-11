using Entidade;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace padrao.Negocio
{
    
    public partial class RCDBContext : DbContext
    {
        public RCDBContext() : base("name=RCDBContext")
        {

        }
        public virtual DbSet<Artistas> TBL_ARTISTAS { get; set; }
        public virtual DbSet<ArtistasVW> TBL_ARTISTASVW { get; set; }
        public virtual DbSet<Obras> TBL_OBRAS { get; set; }
        public virtual DbSet<ObrasVW> TBL_OBRASVW { get; set; }
        public virtual DbSet<Acervo> TBL_ACERVO { get; set; }
        public virtual DbSet<UfCidade> TBL_UF_CIDADE { get; set; }
        public virtual DbSet<ArtPictures> TBL_ART_PICTURES { get; set; }
        
    }
}
