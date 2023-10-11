using Extracao_dados_Futebol.Constants;
using Extracao_dados_Futebol.Interfaces;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace Extracao_dados_Futebol.Services
{
    public class ChromeVersion : IChromeVersion
    {
        public string chromeversion()
        {
            string target = @$"C:\Program Files\Google\Chrome\Application\";
            DirectoryInfo dir = new DirectoryInfo(target);
            FileInfo[] info = dir.GetFiles("chrome.exe",
                                            SearchOption.TopDirectoryOnly);
            return FileVersionInfo.GetVersionInfo(info[0].FullName).ProductVersion;
        }

        public bool procuraChromeDriver(Uri origin, string path)
        {
            using (WebClient cliente = new WebClient())
            {
                var pagina = cliente.DownloadString(origin);
                Uri urlSite = new Uri(ExtraiUrl(pagina));
                return cliente.DownloadFileTaskAsync(urlSite, path + "chromedriver_win32.zip").Wait(3000);
            }
        }

        public string ExtraiUrl(string pagina)
        {
            string versao = chromeversion().Substring(0, 11);
            string numeroVersao = versao.Substring(0, versao.IndexOf('.'));

            if (int.Parse(numeroVersao) > 114)
            {
                return VersaoAcima115(versao).AbsoluteUri.ToString();
                
            }
            else
            {
                return VersaoAbaixo114(versao, pagina).AbsoluteUri.ToString();
            }

        
        }

        public void ExtraiZip(string caminho, string path)
        {
            ZipFile.ExtractToDirectory(caminho, path, true);
            
            DirectoryInfo info = new DirectoryInfo(path);
            DirectoryInfo pastas = info.GetDirectories()[0];
            FileInfo file = pastas.GetFiles("*.exe",SearchOption.TopDirectoryOnly)[0];
            File.Copy(file.FullName, path + "\\" + file.Name,true);

            Thread.Sleep(5000);
            File.Delete(caminho);

            foreach (var item in pastas.GetFiles())
            {
                File.Delete(item.FullName);
            }
            Directory.Delete(pastas.FullName, true);
        }

        private Uri VersaoAcima115(string versao)
        {
            string url = $@"https://googlechromelabs.github.io/chrome-for-testing/";

            using (WebClient cliente = new WebClient())
            {
                var pagina = cliente.DownloadString(url);
                int posicao1 = pagina.IndexOf(versao);
                string versaoLocalizada = pagina.Substring(posicao1, 100);
                string versaoDisponivel = versaoLocalizada.Substring(0, versaoLocalizada.IndexOf("</code>"));
                return new Uri($@"https://edgedl.me.gvt1.com/edgedl/chrome/chrome-for-testing/{versaoDisponivel}/win64/chromedriver-win64.zip");
                 //cliente.DownloadFileTaskAsync(urlSite, Util.Caminho + "chromedriver_win32.zip").Wait(3000);
            }
        }

        private Uri VersaoAbaixo114(string versao, string pagina)
        {

            using (WebClient cliente = new WebClient())
            {
                string url = $@"https://chromedriver.storage.googleapis.com/index.html?path={versao}";
                int tamanho = url.Length;
                int indexPagina = pagina.IndexOf(url);
                string aux = pagina.Substring(indexPagina);
                string retorno = aux.Substring(0, aux.IndexOf(@"/", tamanho));//indexPagina, tamanho + 2);

                string versaoSite = retorno.Substring(retorno.LastIndexOf("=") + 1); 
                return new Uri($@"https://chromedriver.storage.googleapis.com/{versaoSite}/chromedriver_win32.zip");
                //return cliente.DownloadFileTaskAsync(urlSite, Util.Caminho + "chromedriver_win32.zip").Wait(3000);
            }
        }
    }
}
