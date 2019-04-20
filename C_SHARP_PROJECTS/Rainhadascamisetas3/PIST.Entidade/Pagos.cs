
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
    [Table("NOTAS_PAGAS")]
    [DataContract]
    public class Pago
    {
        [Key]
        [Column("ID")]
        [DataMember]
        public int ID { get; set; }

        [Column("DATA")]
        [DataMember]
        public DateTime DATA { get; set; }

        [Column("OS")]
        [DataMember]
        public string OS { get; set; }

        [Column("CPF")]
        [DataMember]
        public string CPF { get; set; }

        [Column("CATEGORIA")]
        [DataMember]
        public string CATEGORIA { get; set; }

        [Column("VALOR")]
        [DataMember]
        public double VALOR { get; set; }

        [Column("OBS")]
        [DataMember]
        public string OBS { get; set; }

        [Column("NOTA")]
        [DataMember]
        public string NOTA { get; set; }


    }


}
