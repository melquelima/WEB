using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using static Rainhadascamisetas.Models.Util;

namespace Rainhadascamisetas.Models
{
    /// <summary>
    /// 
    /// </summary>
    public static class codeEnumExtensions
    {
        /// <summary>
        /// método para buscar a descrição do Enum [codeEnum]
        /// </summary>
        /// <param name="val">codeEnum</param>
        /// <returns>retorna o valor da dataanotation Description do atributo do Enum codeEnum </returns>
        public static string ToDescriptionString(this codeEnum val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }

    /// <summary>
    /// classe para utilidades globais
    /// </summary>
   
    public class Util
    {
        /// <summary>
        /// Enum para erros globais
        /// </summary>
       
        public enum codeEnum
        {
            /// <summary>
            /// Dados salvos com sucesso!
            /// </summary>
            [Description("Dados salvos com sucesso!")]
            ResulOk = 1,
            /// <summary>
            /// Não foram encontrado dados!
            /// </summary>
            [Description("Não foram encontrado dados!")]
            RetornoVazio = 2,
            /// <summary>
            /// Não foi possível atualizar os dados, registro não encontrado!
            /// </summary>
            [Description("Não foi possível atualizar os dados, registro não encontrado!")]
            RegistroNaoEncontrado = 3,
            /// <summary>
            /// Usuário ou senha inválidos!
            /// </summary>
            [Description("Usuário ou senha inválidos!")]
            ErroLogin = 4,
            /// <summary>
            /// Dados Inválidos!
            /// </summary>
            [Description("Dados Inválidos!")]
            DadosInvalidos = 5,
            /// <summary>
            /// Não existe registro no sistema para os critérios informados!
            /// </summary>
            [Description("Não existe registro no sistema para os critérios informados!")]
            RegistroNaoEncontradoConsulta = 6,
            /// <summary>
            /// Erro crítico, entre em contato com o administrador!
            /// </summary>
            [Description("Erro crítico, entre em contato com o administrador!")]
            ErroCritico = -1,
            /// <summary>
            /// Campos obrigatórios não preenchidos
            /// </summary>
            [Description("Campos obrigatórios não preenchidos")]
            CamposObrigatoriosNaoPreenchidos = 7,
            /// <summary>
            /// Dados em duplicidade
            /// </summary>
            [Description("Dados em duplicidade")]
            DadosDuplicados = 99,
            /// <summary>
            /// Aviso importante
            /// </summary>
            [Description("Aviso importante")]
            AvisoImportante = 98,
            /// <summary>
            /// Usuário ou senha inválidos!
            /// </summary>
            [Description("Refazer Login!")]
            RefazerLogin = 10,
            /// <summary>
            /// Erro  Usuário!
            /// </summary>
            [Description("Usuário inválido, não foram encontrados dados")]
            ErroUsuario = 11,
        };

        /// <summary>
        /// validar objeto dinamico
        /// </summary>
        /// <param name="obj">objeto dinâmico</param>
        /// <returns></returns>
        public static IEnumerable<ValidationResult> getValidationErros(object obj)
        {
            var resultadoValidacao = new List<ValidationResult>();
            var contexto = new ValidationContext(obj, null, null);
            Validator.TryValidateObject(obj, contexto, resultadoValidacao, true);
            return resultadoValidacao;
        }

        /// <summary>
        /// função global para validação de datas tipo [dd/MM/yyyy]
        /// </summary>
        /// <param name="data">string data</param>
        /// <returns>data convertida</returns>
        public static DateTime? ValidarData(string data)
        {
            data = data.Substring(0, 10);

            try
            {
                string[] formats = { "dd/MM/yyyy" };
                var dateTime = DateTime.ParseExact(data, formats, CultureInfo.InvariantCulture, DateTimeStyles.None);

                return dateTime;

            }
            catch (Exception ex)
            {
                string aux = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// função global para validação de datas tipo [dd/MM/yyyy]
        /// </summary>
        /// <param name="data">string data</param>
        /// <returns>data convertida</returns>
        public static DateTime? ValidarDataddmmyyyyhhmmss(string data)
        {
            try
            {
                string[] formats = { "dd/MM/yyyy H:m:s" };
                var dateTime = DateTime.ParseExact(data, formats, CultureInfo.InvariantCulture, DateTimeStyles.None);

                return dateTime;

            }
            catch (Exception ex)
            {
                string aux = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// função global para validação de datas tipo [M/d/yyyy H:m]
        /// </summary>
        /// <param name="data">string data</param>
        /// <returns>data convertida</returns>
        public static DateTime? ValidarDataMdYYYYHmt(string data)
        {
            try
            {
                string[] formats = { "M/d/yyyy H:m" };
                var dateTime = DateTime.ParseExact(data, formats, CultureInfo.InvariantCulture, DateTimeStyles.None);

                return dateTime;
            }
            catch (Exception ex)
            {
                string aux = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// função global para validação de datas tipo [M/d/yyyy H:m:s]
        /// </summary>
        /// <param name="data">string data</param>
        /// <returns>data convertida</returns>
        public static DateTime? ValidarDataMdYYYYHmts(string data)
        {
            try
            {
                string[] formats = { "M/d/yyyy H:m:s" };
                var dateTime = DateTime.ParseExact(data, formats, CultureInfo.InvariantCulture, DateTimeStyles.None);

                return dateTime;
            }
            catch (Exception ex)
            {
                string aux = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// função global para validação de datas tipo [M/d/yyyy]
        /// </summary>
        /// <param name="data">string data</param>
        /// <returns>data convertida</returns>
        public static DateTime? ValidarDataMdYYYY(string data)
        {
            try
            {
                string[] formats = { "M/d/yyyy" };
                var dateTime = DateTime.ParseExact(data, formats, CultureInfo.InvariantCulture, DateTimeStyles.None);

                return dateTime;
            }
            catch (Exception ex)
            {
                string aux = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// função global para remover caracteres
        /// </summary>
        /// <param name="str">string para remover caracteres</param>
        /// <returns>string sem caracters especias</returns>
        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Método que valida o CNPJ enviado pelo usuário
        /// </summary>
        /// <param name="cnpj">Recebe como parâmetro o CNPJ enviado pelo usuário</param>
        /// <returns>Retorna um Tuple(bool - True quando correto,string - Vazio quando correto) de acordo com o resultado do CNPJ enviado</returns>
        public static string validarCNPJ(string cnpj)
        {
            try
            {
                if (cnpj == null)
                    return "";

                string ErroCNPJ = "CNPJ Inválido";

                int[] mt1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] mt2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int soma; int resto; string digito; string TempCNPJ;

                cnpj = cnpj.Trim();
                cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "").Replace("_", "");

                if (cnpj.Length != 14)
                    return ErroCNPJ;

                if (cnpj == "00000000000000" || cnpj == "11111111111111" ||
                 cnpj == "22222222222222" || cnpj == "33333333333333" ||
                 cnpj == "44444444444444" || cnpj == "55555555555555" ||
                 cnpj == "66666666666666" || cnpj == "77777777777777" ||
                 cnpj == "88888888888888" || cnpj == "99999999999999")
                    return ErroCNPJ;

                TempCNPJ = cnpj.Substring(0, 12);
                soma = 0;

                for (int i = 0; i < 12; i++)
                    soma += int.Parse(TempCNPJ[i].ToString()) * mt1[i];

                resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito = resto.ToString();

                TempCNPJ = TempCNPJ + digito;
                soma = 0;
                for (int i = 0; i < 13; i++)
                    soma += int.Parse(TempCNPJ[i].ToString()) * mt2[i];

                resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = digito + resto.ToString();

                return "";
            }
            catch (Exception Ex)
            {
                string aux = Ex.Message;
                return "Erro crítico, entre em contato com o administrador!";
            }

        }

        /// <summary>
        /// Replace em tudo que for diferente de número
        /// </summary>
        /// <param name="Valor"></param>
        /// <param name="Tipo"></param>
        /// <returns></returns>
        public static string ReplaceLetras(string Valor, string Tipo)
        {
            try
            {
                string Result = Regex.Replace(Valor, "[^0-9.]", "").Replace(".","");

                if(Tipo == "CNPJ")
                {
                    if (Result.Length < 14)
                    {
                        return "CNPJ Inválido!";
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (Tipo == "Raiz")
                {
                    if (Result.Length < 8)
                    {
                        return "Raiz Inválida!";
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (Tipo == "RaizGrupo")
                {
                    if (Result.Length < 10)
                    {
                        return "Raiz Grupo Inválida!";
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }


            }
            catch (Exception Ex)
            {
                string aux = Ex.Message;
                return null;
            }
        }

        /// <summary>
        /// Validação de e-mail
        /// </summary>
        /// <param name="email">string e-mail</param>
        /// <returns>é válido? Sim / Não</returns>
        public bool ValidarEmail(string email)
        {
            return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }
        
    }

    /// <summary>
    /// objeto de retorno global
    /// todas as chamadas retornam este objeto
    /// o objeto definitivo das chamadas fica dentro do atributo data do ObjectRetorno
    /// </summary>
    [DataContract]
    public class ObjectRetorno
    {

        /// <summary>
        /// mensagem de erro critico
        /// </summary>
        [DataMember]
        public string messageError { get; set; }

        /// <summary>
        /// retorno da função
        /// </summary>
        [DataMember]
        public object data { get; set; }

        /// <summary>
        /// codigo de exececao
        /// </summary>
        [DataMember]
        public int code { get; set; }

        /// <summary>
        /// nome do codigo
        /// </summary>
        [DataMember]
        public string strCode { get; set; }

        /// <summary>
        /// erro ocorrido
        /// </summary>
        [DataMember]
        public int error { get; set; }

        /// <summary>
        /// mensagem de alerta para o usúario
        /// </summary>
        [DataMember]
        public string messageAlert { get; set; }

        /// <summary>
        /// instaciar o objeto com erro
        /// </summary>
        /// <param name="codeError">Código do erro</param>
        public ObjectRetorno(codeEnum codeError)
        {

            code = codeError.GetHashCode();
            strCode = codeError.ToString();
            messageAlert = codeError.ToDescriptionString();
            if(codeError == codeEnum.ErroCritico)
            {
                error = -1;
            }

        }

        /// <summary>
        /// construtor da classe
        /// </summary>
        public ObjectRetorno()
        {

        }
    }

}