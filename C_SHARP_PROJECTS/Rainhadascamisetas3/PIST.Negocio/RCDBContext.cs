using Entidade;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIST.Negocio
{
    public partial class RCDBContext : DbContext
    {
        public RCDBContext() : base("name=RCDBContext")
        {

        }
        public virtual DbSet<Cor> TB_API_CORES { get; set; }
        public virtual DbSet<User> TB_API_USUARIOS { get; set; }
        public virtual DbSet<Precos> TB_PRECOS { get; set; }
        public virtual DbSet<PrecosDetalhado> TB_PRECOS_DETALHADO { get; set; }
        public virtual DbSet<Tcc> TB_API_TCC { get; set; }
        public virtual DbSet<Os> TB_API_OS { get; set; }
        public virtual DbSet<Pedido> TB_API_PEDIDOS { get; set; }
        public virtual DbSet<DevedoresOs> TB_API_DEVEDORES_OS { get; set; }
        public virtual DbSet<Pago> TB_API_NOTAS_PAGAS { get; set; }
        public virtual DbSet<Visitantes> TB_API_VISITANTES { get; set; }
    }
}
