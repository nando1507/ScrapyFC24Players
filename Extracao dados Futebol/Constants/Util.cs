using System.Reflection;

namespace Extracao_dados_Futebol.Constants
{
    public static class Util
    {
        public static string Caminho = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\";
        //public Util()
        //{
        //    _Path = ;
        //}
        //public string Caminho()
        //{
        //    return System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\";
        //}

    }
}
