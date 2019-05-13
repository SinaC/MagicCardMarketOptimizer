using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using MagicCardMarket.Cache;
using MagicCardMarket.Log;

namespace MagicCardMarket.APIHelpers
{
    public abstract class HelperBase
    {
        protected IXDocumentCache Cache = new FileSystemXDocumentCache(ConfigurationManager.AppSettings["cachepath"]);

        //protected T DeserializeSingle<T>(XDocument document)
        //{
        //    XmlSerializer serializer = new XmlSerializer(typeof(T));
        //    XElement rootElement = document.Root.Elements().First(); // remove <response>
        //    return (T)serializer.Deserialize(rootElement.CreateReader());
        //}

        //protected T[] DeserializeMultiple<T>(XDocument document)
        //{
        //    XmlSerializer serializer = new XmlSerializer(typeof(T));
        //    XDocument doc = document;
        //    T[] values = new T[doc.Root.Nodes().Count()];
        //    int index = 0;
        //    foreach (XNode node in doc.Root.Nodes()) // loop on <response> subnodes
        //        values[index++] = (T)serializer.Deserialize(node.CreateReader());
        //    return values;
        //}

        protected XDocument Serialize<T>(T data)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XDocument payload = new XDocument();
            using (var writer = payload.CreateWriter())
                serializer.Serialize(writer, data);
            return payload;
        }

        protected async Task<T> DeserializeSingleAsync<T>(Task<XDocument> documentTask)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            var rootElement = (await documentTask).Root.Elements().First();
            return (T)serializer.Deserialize(rootElement.CreateReader());
        }

        protected async Task<T> DeserializeSingleAsync<T>(Task<XDocument> documentTask, string replaceRootName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XDocument document = await documentTask;
            XElement rootElement = document.Root;
            rootElement.Name = replaceRootName;
            return (T) serializer.Deserialize(rootElement.CreateReader());
        }

        protected async Task<T[]> DeserializeMultipleAsync<T>(Task<XDocument> documentTask)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XDocument doc = await documentTask;
            T[] values = new T[doc.Root.Nodes().Count()];
            int index = 0;
            foreach (XNode node in doc.Root.Nodes()) // loop on <response> subnodes
                values[index++] = (T)serializer.Deserialize(node.CreateReader());
            return values;
        }

        //protected XDocument GetWithCache(string category, int id, Func<XDocument> documentFunc)
        //{
        //    XDocument document;
        //    if (Cache.Contains(category, id))
        //        document = Cache.Get(category, id);
        //    else
        //    {
        //        document = documentFunc();
        //        Cache.Set(category, id, document);
        //    }
        //    return document;
        //}

        protected async Task<XDocument> GetWithCacheAsync(string category, int id, bool forceReload, Func<Task<XDocument>> documentTaskFunc)
        {
            XDocument document;
            if (!forceReload && Cache.Contains(category, id))
            {
                Log.Log.Default.WriteLine(LogLevels.Debug, $"CACHE HIT GetWithCacheAsync: category={category} id={id}");
                document = Cache.Get(category, id);
            }
            else
            {
                document = await documentTaskFunc();
                Cache.Set(category, id, document);
            }
            return document;
        }
    }
}
