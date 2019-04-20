

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
    [Table("TB_VISITANTES")]
    [DataContract]
    public class Visitantes
    {
        [Key]
        [Column("DATA")]
        [DataMember]
        public DateTime DATA { get; set; }

        [Column("QTD")]
        [DataMember]
        public int QTD { get; set; }


    }


}
