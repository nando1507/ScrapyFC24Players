using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Extracao_dados_Futebol.Constants
{
    [CollectionDefinition("Chrome Driver")]
    public class CollectionFixture : ICollectionFixture<ChromeFixture>
    {


    }
}
