using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utils
{
    public static class EnderecoUtils
    {
        public static string GetSiglaEstado(string Estado)
        {
            var EstadoFormatado = RemoveAccents(Estado).ToUpper();

            switch (EstadoFormatado)
            {
                case "ACRE": return "AC";
                case "ALAGOAS": return "AL";
                case "AMAPA": return "AP";
                case "AMAZONAS": return "AM";
                case "BAHIA": return "BA";
                case "CEARA": return "CE"; 
                case "DISTRITO FEDERAL": return "DF";
                case "ESPIRITO SANTO": return "ES"; 
                case "GOIAS": return "GO"; 
                case "MARANHAO": return "MA"; 
                case "MATO GROSSO": return "MT"; 
                case "MATO GROSSO DO SUL": return "MS"; 
                case "MINAS GERAIS": return "MG"; 
                case "PARA": return "PA"; 
                case "PARAIBA": return "PB"; 
                case "PARANA": return "PR"; 
                case "PERNANBUCO": return "PE"; 
                case "PIAUI": return "PI"; 
                case "RIO DE JANEIRO": return "RJ"; 
                case "RIO GRANDE DO NORTE": return "RN"; 
                case "RIO GRANDE DO SUL": return "RS"; 
                case "RONDONIA": return "RO"; 
                case "RORAIMA": return "RR"; 
                case "SANTA CATARINA": return "SC"; 
                case "SAO PAULO": return "SP"; 
                case "SERGIPE": return "SE"; 
                case "TOCANTINS": return "TO";
                default: return null;
            }

        }
        private static string RemoveAccents(this string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }
    }
}
