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
    [Table("TBL_ACERVO")]
    [DataContract]
    public class Acervo
    {
        [Key]
        [Column("ID_ACERVO")]
        [DataMember]
        public int ID_ACERVO { get; set; }

        [Column("ID_EXPOSICAO")]
        [DataMember]
        public int ID_EXPOSICAO { get; set; }

        [Column("ID_OBRA")]
        [DataMember]
        public int ID_OBRA { get; set; }
    }

}