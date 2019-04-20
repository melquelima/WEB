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
    [Table("TCC_MIS_SENSORES")]
    [DataContract]
    public class Tcc
    {
        [Key]
        [Column("DATA")]
        [DataMember]
        public DateTime Data { get; set; }

        [Column("TEMPERATURA")]
        [DataMember]
        public double Temperatura { get; set; }

        [Column("BATIMENTOS")]
        [DataMember]
        public double Batimentos { get; set; }

        [Column("OXIMETRIA")]
        [DataMember]
        public double Oximetria { get; set; }

    }

}
