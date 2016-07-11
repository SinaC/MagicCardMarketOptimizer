using System.IO;

namespace MagicCardMarket.Cache
{
    public class FileSystemCache : ICache
    {
        public string RootPath { get; }

        public FileSystemCache(string rootPath)
        {
            RootPath = rootPath;
        }

        public bool Contains(string category, int id)
        {
            string filename = Path.Combine(RootPath, category, id.ToString()) + ".xml";
            return File.Exists(filename);
        }

        public void Set(string category, int id, string xml)
        {
            string path = Path.Combine(RootPath, category);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string filename = Path.Combine(path, id.ToString())+".xml";
            File.WriteAllText(filename, xml);
        }

        public string Get(string category, int id)
        {
            string filename = Path.Combine(RootPath, category, id.ToString()) + ".xml";
            if (File.Exists(filename))
                return File.ReadAllText(filename);
            return null;
        }
    }
}
