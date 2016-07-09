using System.Linq;
using System.Windows;
using System.Xml.Linq;
using System.Xml.Serialization;
using MagicCardMarket.Contracts;

namespace MagicCardMarket.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Tokens.Init(@"d:\utils\token mcm.txt");

            //Account account = GetData<Account>("account");
            //Game game = GetData<Game>("games");
            //Game[] games = GetDatas<Game>("games");
            //Product island = GetData<Product>("products/island/1/1/false");
            //Product[] islands = GetDatas<Product>("products/island/1/1/false");
            //MetaProduct metaIsland = GetData<MetaProduct>("metaproduct/2923");
            //Expansion expansion = GetData<Expansion>("expansion/1");
            //Expansion[] expansions = GetDatas<Expansion>("expansion/1");
            //WantsList[] wantsLists = GetDatas<WantsList>("wantslist");

            //Product product266361 = GetData<Product>("product/266361");
            //Article[] articles266361 = GetDatas<Article>("articles/266361");
            //wantsListId to test 968105
            Want[] wants968105 = GetDatas<Want>("wantslist/968105");
        }

        //public XDocument Request(string resource)
        //{
        //    //https://www.mkmapi.eu/ws/documentation/API:Auth_csharp
        //    RequestHelper request = new RequestHelper();
        //    string responseRaw = request.MakeRequest(resource);
        //    XDocument responseXml = XDocument.Parse(responseRaw);
        //    return new XDocument(new XDeclaration("1.0", "utf-8", "yes"), responseXml.Root.Elements().First());
        //}

        public T GetData<T>(string resource)
        {
            RequestHelper request = new RequestHelper();
            string responseRaw = request.MakeRequest(resource);
            XDocument responseXml = XDocument.Parse(responseRaw);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XElement rootElement = responseXml.Root.Elements().First();
            return (T)serializer.Deserialize(rootElement.CreateReader());
        }

        public T[] GetDatas<T>(string resource)
        {
            RequestHelper request = new RequestHelper();
            string responseRaw = request.MakeRequest(resource);
            XDocument responseXml = XDocument.Parse(responseRaw);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            T[] values = new T[responseXml.Root.Nodes().Count()];
            int index = 0;
            foreach (XNode node in responseXml.Root.Nodes())
            {
                values[index] = (T)serializer.Deserialize(node.CreateReader());
                index++;
            }
            return values;
        }
    }
}
