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
    [Table("TBL_OBRAS")]
    [DataContract]
    public class Obras
    {
        [Key]
        [Column("ID_OBRA")]
        [DataMember]
        public int ID_OBRA { get; set; }

        [Column("CPF")]
        [DataMember]
        [StringLength(11)]
        public string CPF { get; set; }

        [Column("NOME")]
        [DataMember]
        [StringLength(200)]
        public string NOME { get; set; }

        [Column("DIMENSOES")]
        [DataMember]
        [StringLength(100)]
        public string DIMENSOES { get; set; }

        [Column("ESTADO")]
        [DataMember]
        [StringLength(1)]
        public string ESTADO { get; set; }

        [Column("ID_TEMATICA")]
        [DataMember]
        public int ID_TEMATICA { get; set; }

        [Column("DT_ENTRADA")]
        [DataMember]
        public DateTime DT_ENTRADA { get; set; }

        [Column("ID_UF_CIDADE")]
        [DataMember]
        public int ID_UF_CIDADE { get; set; }


    }

    [Table("VW_OBRAS")]
    [DataContract]
    public class ObrasVW
    {
        [Key]
        [Column("ID_OBRA")]
        [DataMember]
        public int ID_OBRA { get; set; }

        [Column("CPF")]
        [DataMember]
        [StringLength(11)]
        public string CPF { get; set; }

        [Column("NOME")]
        [DataMember]
        [StringLength(200)]
        public string NOME { get; set; }

        [Column("DT_ENTRADA")]
        [DataMember]
        public DateTime DT_ENTRADA { get; set; }

        [Column("DIMENSOES")]
        [DataMember]
        [StringLength(100)]
        public string DIMENSOES { get; set; }

        [Column("ESTADO")]
        [DataMember]
        [StringLength(1)]
        public string ESTADO { get; set; }

        [Column("TEMATICA")]
        [DataMember]
        [StringLength(100)]
        public string TEMATICA { get; set; }

        [Column("UF")]
        [DataMember]
        [StringLength(2)]
        public string UF { get; set; }

        [Column("CIDADE")]
        [DataMember]
        [StringLength(100)]
        public string CIDADE { get; set; }


    }
}