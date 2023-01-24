using System.IO;
using System.Windows;
using System.Windows.Navigation;
using System.Diagnostics;
using System.ComponentModel;
using Microsoft.Win32;

using L4D2DevTools.Utils;
using L4D2DevTools.Helper;

namespace L4D2DevTools
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Main_Loaded(object sender, RoutedEventArgs e)
        {
            // 读取对应配置文件
            Globals.L4D2MainExec = IniHelper.ReadValue("Config", "L4D2MainExec");
            Globals.L4D2MainDir = IniHelper.ReadValue("Config", "L4D2MainDir");

            if (string.IsNullOrEmpty(Globals.L4D2MainDir))
            {
                var initWindow = new InitWindow
                {
                    Owner = this
                };
                initWindow.ShowDialog();
            }
        }

        private void Window_Main_Closing(object sender, CancelEventArgs e)
        {
            IniHelper.WriteValue("Config", "L4D2MainExec", Globals.L4D2MainExec);
            IniHelper.WriteValue("Config", "L4D2MainDir", Globals.L4D2MainDir);
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(e.Uri.OriginalString);
            e.Handled = true;
        }

        private void DropHelper(DragEventArgs e, string extePath, string fileExte)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var fileNames = e.Data.GetData(DataFormats.FileDrop) as string[];
                if (Path.GetExtension(fileNames[0]).ToLower() == fileExte)
                {
                    ProcessUtil.OpenExecWithArgs(extePath, $"\"{fileNames[0]}\"");
                }
                else
                {
                    MsgBoxUtil.Warning($"当前拖放的目标文件非 {fileExte} 格式，操作取消");
                }
            }
        }

        private void Button_RunGCFScape_Click(object sender, RoutedEventArgs e)
        {
            ProcessUtil.OpenExecWithArgs(".\\AppData\\GCFScape\\GCFScape.exe");
        }

        private void Button_RunVTFEdit_Click(object sender, RoutedEventArgs e)
        {
            ProcessUtil.OpenExecWithArgs(".\\AppData\\VTFEdit\\VTFEdit.exe");
        }

        private void Button_RunBSPSource_Click(object sender, RoutedEventArgs e)
        {
            ProcessUtil.OpenExecWithArgs(".\\AppData\\BSPSource\\bspsrc.bat");
        }

        private void Button_VPKUnpack_Click(object sender, RoutedEventArgs e)
        {
            var folder = new OpenFileDialog
            {
                Title = "选择要解包的VPK文件",
                RestoreDirectory = true,
                DefaultExt = ".vpk",
                Filter = "VPK文件 (*.vpk)|*.vpk",
                ValidateNames = true,
                AddExtension = true,
                CheckFileExists = false,
                Multiselect = false
            };

            if (folder.ShowDialog() == true)
            {
                ProcessUtil.OpenExecWithArgs(".\\AppData\\VPK\\vpk.exe", $"\"{folder.FileName}\"");
            }
        }

        private void Button_RunGCFScape_Drop(object sender, DragEventArgs e)
        {
            DropHelper(e, ".\\AppData\\GCFScape\\GCFScape.exe", ".vpk");
        }

        private void Button_RunVTFEdit_Drop(object sender, DragEventArgs e)
        {
            DropHelper(e, ".\\AppData\\VTFEdit\\VTFEdit.exe", ".vtf");
        }

        private void Button_RunBSPSource_Drop(object sender, DragEventArgs e)
        {
            DropHelper(e, ".\\AppData\\BSPSource\\bspsrc.bat", ".bsp");
        }

        private void Button_VPKUnpack_Drop(object sender, DragEventArgs e)
        {
            DropHelper(e, ".\\AppData\\VPK\\vpk.exe", ".vpk");
        }

        private void Button_SetRegionEnUS_Click(object sender, RoutedEventArgs e)
        {
            ProcessUtil.OpenExecWithArgs("rundll32.exe", "shell32.dll,Control_RunDLL intl.cpl,,0");
        }

        private void Button_L4D2Dir_Click(object sender, RoutedEventArgs e)
        {
            ProcessUtil.OpenLink($"{Globals.L4D2MainDir}");
        }

        private void Button_L4D2Dir_Addons_Click(object sender, RoutedEventArgs e)
        {
            ProcessUtil.OpenLink(Globals.L4D2AddonsDir);
        }

        private void Button_L4D2Dir_Maps_Click(object sender, RoutedEventArgs e)
        {
            ProcessUtil.OpenLink(Globals.L4D2MapsDir);
        }

        private void CheckBox_IsTopMost_Click(object sender, RoutedEventArgs e)
        {
            this.Topmost = CheckBox_IsTopMost.IsChecked == true;
        }
    }
}
