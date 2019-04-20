
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
    [Table("VW_NUM_OS")]
    [DataContract]
    public class Os
    {
        [Key]
        [Column("OS")]
        [DataMember]
        public Int64 OS { get; set; }
        

    }


}
