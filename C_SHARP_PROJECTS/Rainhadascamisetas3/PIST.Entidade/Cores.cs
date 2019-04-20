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
	[Table("cores")]
	[DataContract]
	public class Cor
    {
		[Key]
		[Column("COR")]
		[DataMember]
		[StringLength(100)]
		public string COR { get; set; }
	}

}
