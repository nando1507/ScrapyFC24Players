using System.Text.RegularExpressions;

namespace Extracao_dados_Futebol.Constants
{
    public static class SplitQualificado
    {
        public static string[] SplitQualificados (
            this string Texto, 
            char Delimitador, 
            char Qualificador, 
            bool QualificadorDoResultado, 
            int QuantidadeColunas = 0) 
        {
            string pattern = string.Format(@"{0}(?=(?:[^{1}]*{1}[^{1}]*{1})*(?![^{1}]*{1}))",
                Regex.Escape(Delimitador.ToString()),
                Regex.Escape(Qualificador.ToString())
                );

            string[] split = Regex.Split(Texto, pattern);

            if(split.Length < QuantidadeColunas) 
            {
                split = Texto
                    .Split(Delimitador)
                    .Select(s => s
                        .Trim(Qualificador)
                        .Replace(Qualificador,'\0')
                       )
                    .ToArray();
            }

            if (QualificadorDoResultado)
            {
                return split
                    .Select(s => s
                        .Trim()
                        .Trim(Qualificador)
                       )
                    .ToArray();
            }
            else
            {
                return split;
            }
        }
    }
}
