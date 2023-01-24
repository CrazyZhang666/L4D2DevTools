using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.ComponentModel;

namespace L4D2DevTools
{
    /// <summary>
    /// InitWindow.xaml 的交互逻辑
    /// </summary>
    public partial class InitWindow : Window
    {
        public InitWindow()
        {
            InitializeComponent();
        }

        private void Window_Init_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Init_Closing(object sender, CancelEventArgs e)
        {

        }

        private void Button_SelectL4D2MainExec_Click(object sender, RoutedEventArgs e)
        {
            var folder = new OpenFileDialog
            {
                Title = "选择求生之路2主程序 left4dead2.exe",
                RestoreDirectory = true,
                DefaultExt = ".exe",
                Filter = "可执行文件 (*.exe)|*.exe",
                FileName = "left4dead2.exe",
                ValidateNames = true,
                AddExtension = true,
                CheckFileExists = false,
                Multiselect = false
            };

            if (!string.IsNullOrEmpty(Globals.L4D2MainDir))
                folder.InitialDirectory = Globals.L4D2MainDir;

            if (folder.ShowDialog() == true)
            {
                Globals.L4D2MainExec = folder.FileName;
                Globals.L4D2MainDir = Path.GetDirectoryName(folder.FileName);

                this.Close();
            }
        }
    }
}
