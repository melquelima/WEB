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
    [Table("PEDIDOS")]
    [DataContract]
    public class Pedido
    {
        [Key]
        [Column("ID")]
        [DataMember]
        public Int32 ID { get; set; }

        [Column("OS")]
        [DataMember]
        public string OS { get; set; }

        [Column("DATA")]
        [DataMember]
        public DateTime DATA { get; set; }

        [Column("CLIENTE_CPF")]
        [DataMember]
        public string CPF { get; set; }

        [Column("MALHA")]
        [DataMember]
        public string MALHA { get; set; }

        [Column("CATEGORIA_PROD")]
        [DataMember]
        public string CAT_PRODUTO { get; set; }

        [Column("PRODUTO")]
        [DataMember]
        public string PRODUTO { get; set; }

        [Column("COR")]
        [DataMember]
        public string COR { get; set; }

        [Column("CATEGORIA_TAM")]
        [DataMember]
        public string CAT_TAMANHO { get; set; }

        [Column("TAMANHO")]
        [DataMember]
        public string TAMANHO { get; set; }

        [Column("VALOR")]
        [DataMember]
        public double VALOR { get; set; }

        [Column("QTD")]
        [DataMember]
        public int QUANTIDADE { get; set; }

        [Column("DT_ENTREGA")]
        [DataMember]
        public DateTime DATA_ENTREGA { get; set; }

        [Column("ORCAMENTO")]
        [DataMember]
        public int ORCAMENTO { get; set; }

        [Column("STATUS")]
        [DataMember]
        public string STATUS { get; set; }


    }
}
