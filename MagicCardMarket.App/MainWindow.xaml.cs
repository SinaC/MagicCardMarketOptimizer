using System;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using System.Xml.Serialization;
using MagicCardMarket.Models;
using MagicCardMarket.Request;

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

            //Tokens.Init(@"d:\temp\token mcm.txt");
            //RequestHelper helper = new RequestHelper();
            //Product[] vampireNighthawks = helper.GetDatas<Product>("products/vampirenighthawk/1/1/false");
            //string responseRaw = helper.MakeRequestPaging("articles/284478");
            //XDocument responseXml = XDocument.Parse(responseRaw);
            //Article[] articles = GetDatas<Article>("articles/284478");

            MainViewModel vm = new MainViewModel();
            DataContext = vm;

            vm.Initialize();
        }

        //public T[] GetDatas<T>(string resource)
        //{
        //    string responseRaw = String.Empty;
        //    try
        //    {
        //        RequestHelper request = new RequestHelper();
        //        responseRaw = request.MakeRequestPaging(resource);
        //        XDocument responseXml = XDocument.Parse(responseRaw);
        //        XmlSerializer serializer = new XmlSerializer(typeof(T));
        //        T[] values = new T[responseXml.Root.Nodes().Count()];
        //        int index = 0;
        //        foreach (XNode node in responseXml.Root.Nodes())
        //        {
        //            values[index] = (T)serializer.Deserialize(node.CreateReader());
        //            index++;
        //        }
        //        return values;
        //    }
        //    catch (Exception)
        //    {
        //        System.Diagnostics.Debug.WriteLine(responseRaw);
        //        throw;
        //    }
        //}

        //public XElement Request(string resource)
        //{
        //    //https://www.mkmapi.eu/ws/documentation/API:Auth_csharp
        //    RequestHelper request = new RequestHelper();
        //    string responseRaw = request.MakeRequestPaging(resource);
        //    XDocument responseXml = XDocument.Parse(responseRaw);
        //    return responseXml.Root.Elements().First();
        //}
    }
}
