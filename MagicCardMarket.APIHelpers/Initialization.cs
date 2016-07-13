using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagicCardMarket.Request;
using System.Configuration;

namespace MagicCardMarket.APIHelpers
{
    public class Initialization
    {
        public void InitializeTokens()
        {
            Tokens.Init(ConfigurationManager.AppSettings["tokensfile"]);
        }
    }
}
