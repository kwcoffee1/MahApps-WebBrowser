using System;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Navigation;

using MahApps.Metro.Controls;

namespace BadBrowser
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            webBrowser.Navigated += WebBrowser_Navigated;
            WindowState = System.Windows.WindowState.Maximized;
            webBrowser.Source = new Uri("https://www.barchart.com/stocks/signals/top-bottom/top");
        }

        private void WebBrowser_Navigated(object sender, NavigationEventArgs e)
        {
            HideScriptErrors(true);
        }

        public void HideScriptErrors(bool hide)
        {
            var fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fiComWebBrowser == null) return;
            var objComWebBrowser = fiComWebBrowser.GetValue(webBrowser);
            if (objComWebBrowser == null)
            {
                webBrowser.Loaded += (o, s) => HideScriptErrors(hide); //In case we are to early
                return;
            }
            objComWebBrowser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, objComWebBrowser, new object[] { hide });
        }
    }
}
