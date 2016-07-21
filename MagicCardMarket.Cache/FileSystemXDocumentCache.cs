using System.IO;
using System.Xml.Linq;

namespace MagicCardMarket.Cache
{
    public class FileSystemXDocumentCache : IXDocumentCache
    {
        public string RootPath { get; }

        public FileSystemXDocumentCache(string rootPath)
        {
            RootPath = rootPath;
        }

        #region IXDocumentCache

        public bool Contains(string category, int id)
        {
            string filename = Path.Combine(RootPath, category, id.ToString()) + ".xml";
            return File.Exists(filename);
        }

        public void Set(string category, int id, XDocument xml)
        {
            string path = Path.Combine(RootPath, category);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string filename = Path.Combine(path, id.ToString()) + ".xml";
            File.WriteAllText(filename, xml.ToString());
        }

        public XDocument Get(string category, int id)
        {
            string filename = Path.Combine(RootPath, category, id.ToString()) + ".xml";
            if (File.Exists(filename))
                return XDocument.Parse(File.ReadAllText(filename));
            return null;
        }

        public void Remove(string category, int id)
        {
            string filename = Path.Combine(RootPath, category, id.ToString()) + ".xml";
            if (File.Exists(filename))
                File.Delete(filename);
        }

        public void Clear(string category)
        {
            string path = Path.Combine(RootPath, category);
            if (Directory.Exists(path))
                Directory.Delete(path, true);
        }

        #endregion
    }
}
