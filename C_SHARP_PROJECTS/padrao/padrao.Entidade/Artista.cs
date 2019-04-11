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
    [Table("TBL_ARTISTAS")]
    [DataContract]
    public class Artistas
    {
        [Key]
        [Column("CPF")]
        [DataMember]
        [StringLength(11)]
        public string CPF { get; set; }

        [Column("NOME")]
        [DataMember]
        [StringLength(200)]
        public string NOME { get; set; }

        [Column("DNS")]
        [DataMember]
        public DateTime DNS { get; set; }

        [Column("ID_UF_CIDADE")]
        [DataMember]
        public int ID_UF_CIDADE { get; set; }

    }


    [Table("VW_ARTISTAS")]
    [DataContract]
    public class ArtistasVW
    {
        [Key]
        [Column("CPF")]
        [DataMember]
        [StringLength(11)]
        public string CPF { get; set; }

        [Column("NOME")]
        [DataMember]
        [StringLength(200)]
        public string NOME { get; set; }

        [Column("DNS")]
        [DataMember]
        public DateTime DNS { get; set; }

        [Column("UF")]
        [DataMember]
        [StringLength(2)]
        public String UF { get; set; }

        [Column("CIDADE")]
        [DataMember]
        [StringLength(100)]
        public String CIDADE { get; set; }

    }
}