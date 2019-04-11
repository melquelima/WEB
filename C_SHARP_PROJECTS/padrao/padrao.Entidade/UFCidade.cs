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
    [Table("TBL_UF_CIDADE")]
    [DataContract]
    public class UfCidade
    {
        [Key]
        [Column("ID_UF_CIDADE")]
        [DataMember]
        public int ID_UF_CIDADE { get; set; }

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