﻿using System.Threading.Tasks;
using System.Xml.Linq;

namespace MagicCardMarket.Request
{
    public interface IPutRequestHelper
    {
        XDocument Put(string request, XDocument data);

        Task<XDocument> PutAsync(string request, XDocument data);
    }
}
