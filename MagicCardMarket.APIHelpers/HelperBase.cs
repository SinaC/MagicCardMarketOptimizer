using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using MagicCardMarket.Cache;

namespace MagicCardMarket.APIHelpers
{
    public abstract class HelperBase
    {
        protected ICache Cache = new FileSystemCache(ConfigurationManager.AppSettings["cachepath"]);

        protected async Task<T> GetSingleAsync<T>(Task<XDocument> document)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XElement rootElement = (await document).Root.Elements().First(); // remove <response>
            return (T)serializer.Deserialize(rootElement.CreateReader());
        }

        protected async Task<T[]> GetMultipleAsync<T>(Task<XDocument> document)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XDocument doc = await document;
            T[] values = new T[doc.Root.Nodes().Count()];
            int index = 0;
            foreach (XNode node in doc.Root.Nodes()) // loop on <response> subnodes
            {
                values[index] = (T)serializer.Deserialize(node.CreateReader());
                index++;
            }
            return values;
        }
    }
}
