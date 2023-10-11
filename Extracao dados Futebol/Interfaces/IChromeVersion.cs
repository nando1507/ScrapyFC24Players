using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extracao_dados_Futebol.Interfaces
{
    public interface IChromeVersion
    {
        string chromeversion();
        bool procuraChromeDriver(Uri origin, string path);
        string ExtraiUrl(string pagina);
        void ExtraiZip(string caminho, string path);
    }
}
