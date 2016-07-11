using System.Windows;
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
            //Product[] islands = helper.GetDatas<Product>("products/island/1/1/false");

            MainViewModel vm = new MainViewModel();
            DataContext = vm;

            vm.Initialize();
        }

        //public XDocument Request(string resource)
        //{
        //    //https://www.mkmapi.eu/ws/documentation/API:Auth_csharp
        //    RequestHelper request = new RequestHelper();
        //    string responseRaw = request.MakeRequest(resource);
        //    XDocument responseXml = XDocument.Parse(responseRaw);
        //    return new XDocument(new XDeclaration("1.0", "utf-8", "yes"), responseXml.Root.Elements().First());
        //}
    }
}
