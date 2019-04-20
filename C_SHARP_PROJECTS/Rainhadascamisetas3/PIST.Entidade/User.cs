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
    [Table("TB_USUARIO")]
    [DataContract]
    public class User
    {
        [Key]
        [Column("ID")]
        [DataMember]
        public Int32 ID { get; set; }

        [Column("CPF")]
        [DataMember]
        [StringLength(14)]
        public string CPF { get; set; }

        [Column("DATA")]
        [DataMember]
        public DateTime DataCadastro { get; set; }

        [Column("NOME")]
        [DataMember]
        [StringLength(100)]
        public string Nome { get; set; }

        [Column("EMPRESA")]
        [DataMember]
        [StringLength(100)]
        public string NomeEmpresa { get; set; }

        [Column("CNPJ")]
        [DataMember]
        [StringLength(50)]
        public string CNPJ { get; set; }

        [Column("TELEFONE1")]
        [DataMember]
        [StringLength(15)]
        public string Tel1 { get; set; }

        [Column("TELEFONE2")]
        [DataMember]
        [StringLength(15)]
        public string Tel2 { get; set; }

        [Column("TELEFONE3")]
        [DataMember]
        [StringLength(15)]
        public string Tel3 { get; set; }

        [Column("EMAIL")]
        [DataMember]
        [StringLength(50)]
        public string Email { get; set; }

        [Column("CEP")]
        [DataMember]
        [StringLength(100)]
        public string CEP { get; set; }

        [Column("UF")]
        [DataMember]
        [StringLength(2)]
        public string UF { get; set; }

        [Column("LOGADOURO")]
        [DataMember]
        [StringLength(100)]
        public string Endereco { get; set; }

        [Column("NUMERO")]
        [DataMember]
        [StringLength(6)]
        public string Numero { get; set; }

        [Column("CIDADE")]
        [DataMember]
        [StringLength(50)]
        public string Cidade { get; set; }

        [Column("BAIRRO")]
        [DataMember]
        [StringLength(100)]
        public string Bairro { get; set; }

        [Column("COMPLEMENTO")]
        [DataMember]
        [StringLength(50)]
        public string Complemento { get; set; }

        [Column("SENHA")]
        [DataMember]
        [StringLength(20)]
        public string senha { get; set; }
    }

}
