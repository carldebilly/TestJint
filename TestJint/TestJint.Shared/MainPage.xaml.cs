using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Jint;
using Uno.Foundation;

namespace TestJint
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            void Log(object o)
            {
                output.Text = o?.ToString() ?? "<null>";
            }

            var engine = new Engine()
                    .SetValue("log", new Action<object>(Log))
                ;

            engine.Execute(@"
      function hello() { 
        log('Hello World ' + new Date());
      };
      
      hello();
    ");

#if __WASM__
            output2.Text =
                WebAssemblyRuntime.InvokeJS("(function(){return 'Hello World ' + new Date();})();");
#else
            output2.Text = "Not supported on this platform.";
#endif
        }
    }
}
