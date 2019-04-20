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
	[Table("VW_TABELA_PRECOS_SIMPLES")]
	[DataContract]
	public class Precos
    {
		[Key]
		[Column("ID")]
		[DataMember]
		public Int64 ID { get; set; }

		[Column("MALHA")]
		[DataMember]
		[StringLength(50)]
		public string MALHA { get; set; }

		[Column("CATEGORIA_PROD")]
		[DataMember]
		[StringLength(50)]
		public string CATEGORIA_PROD { get; set; }

		[Column("ITEM")]
		[DataMember]
		[StringLength(100)]
		public string ITEM { get; set; }

		[Column("CATEGORIA_TAM")]
		[DataMember]
		[StringLength(50)]
		public string CATEGORIA_TAM { get; set; }

		[Column("BRANCO")]
		[DataMember]
		public double BRANCO { get; set; }

		[Column("COLORIDO")]
		[DataMember]
		public double COLORIDO { get; set; }

		[Column("TAMANHO")]
		[DataMember]
		[StringLength(200)]
		public string TAMANHO { get; set; }

	}

    [Table("VW_TABELA_PRECOS")]
    [DataContract]
    public class PrecosDetalhado
    {
        [Key]
        [Column("CONJ_ID")]
        [DataMember]
        public Int32 ID { get; set; }

        [Column("MALHA")]
        [DataMember]
        [StringLength(50)]
        public string MALHA { get; set; }

        [Column("CATEGORIA_PROD")]
        [DataMember]
        [StringLength(50)]
        public string CATEGORIA_PROD { get; set; }

        [Column("PRODUTO")]
        [DataMember]
        [StringLength(100)]
        public string ITEM { get; set; }

        [Column("CATEGORIA_TAM")]
        [DataMember]
        [StringLength(50)]
        public string CATEGORIA_TAM { get; set; }

        [Column("TAMANHO")]
        [DataMember]
        [StringLength(200)]
        public string TAMANHO { get; set; }

        [Column("BRANCO")]
        [DataMember]
        public double BRANCO { get; set; }

        [Column("COLORIDO")]
        [DataMember]
        public double COLORIDO { get; set; }

        [Column("COD_UNICO")]
        [DataMember]
        public string COD_UNICO { get; set; }


    }

}
