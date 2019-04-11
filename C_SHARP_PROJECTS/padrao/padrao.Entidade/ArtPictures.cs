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
    [Table("TBL_ART_PICTURES")]
    [DataContract]
    public class ArtPictures
    {
        [Key]
        [Column("ID")]
        [DataMember]
        public int ID { get; set; }

        [Column("ID_OBRA")]
        [DataMember]
        public int ID_OBRA { get; set; }

        [Column("FILE")]
        [DataMember]
        [StringLength(100)]
        public string FILE { get; set; }


    }

}