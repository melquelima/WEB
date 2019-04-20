using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entidade
{
    [Table("VW_DEVEDORES_OS")]
    [DataContract]
    public class DevedoresOs
    {
        
        [Column("DATA")]
        [DataMember]
        public DateTime DATA { get; set; }
        [Key]
        [Column("OS")]
        [DataMember]
        public string OS { get; set; }

        [Column("CLIENTE_CPF")]
        [DataMember]
        public string CLIENTE_CPF { get; set; }

        [Column("CLIENTE")]
        [DataMember]
        public string CLIENTE { get; set; }

        [Column("EMPRESA")]
        [DataMember]
        public string EMPRESA { get; set; }

        [Column("TOTAL")]
        [DataMember]
        public double VALOR { get; set; }

        [Column("PAGO")]
        [DataMember]
        public double PAGO { get; set; }

        [Column("RESTA")]
        [DataMember]
        public double RESTA { get; set; }

        [Column("STATUS")]
        [DataMember]
        public string STATUS { get; set; }

        [Column("DT_ENTREGA")]
        [DataMember]
        public DateTime DT_ENTREGA { get; set; }
    }
}