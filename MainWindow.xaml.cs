using System.IO;
using System.IO.Compression;
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
            if (!File.Exists(".\\AppData.bin"))
            {
                // 释放数据文件
                FileUtil.ExtractResFile("L4D2DevTools.Files.AppData.zip", ".\\AppData.bin");

                if (!Directory.Exists(Globals.AppDataDir))
                {
                    // 解压数据文件
                    ZipFile.ExtractToDirectory(".\\AppData.bin", ".\\AppData");
                }
            }

            // 读取对应配置文件
            Globals.L4D2MainExec = IniHelper.ReadValue("Config", "L4D2MainExec");
            Globals.L4D2MainDir = IniHelper.ReadValue("Config", "L4D2MainDir");

            if (string.IsNullOrEmpty(Globals.L4D2MainDir) || !File.Exists(Globals.L4D2MainExec))
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

        /////////////////////////////////////////////////////////////////////////

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

        /////////////////////////////////////////////////////////////////////////

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
                ProcessUtil.OpenExecWithArgs(Globals.VPK, $"\"{folder.FileName}\"");
            }
        }

        private void Button_RunGCFScape_Click(object sender, RoutedEventArgs e)
        {
            ProcessUtil.OpenExecWithArgs(Globals.GCFScape);
        }

        private void Button_RunVTFEdit_Click(object sender, RoutedEventArgs e)
        {
            ProcessUtil.OpenExecWithArgs(Globals.VTFEdit);
        }

        private void Button_RunBSPSource_Click(object sender, RoutedEventArgs e)
        {
            ProcessUtil.OpenExecWithArgs(Globals.BSPSource);
        }

        private void Button_RunHammer_Click(object sender, RoutedEventArgs e)
        {
            ProcessUtil.OpenExecWithArgs(Globals.HammerExec);
        }

        /////////////////////////////////////////////////////////////////////////

        private void Button_VPKUnpack_Drop(object sender, DragEventArgs e)
        {
            DropHelper(e, Globals.VPK, ".vpk");
        }

        private void Button_RunGCFScape_Drop(object sender, DragEventArgs e)
        {
            DropHelper(e, Globals.GCFScape, ".vpk");
        }

        private void Button_RunVTFEdit_Drop(object sender, DragEventArgs e)
        {
            DropHelper(e, Globals.VTFEdit, ".vtf");
        }

        private void Button_RunBSPSource_Drop(object sender, DragEventArgs e)
        {
            DropHelper(e, Globals.BSPSource, ".bsp");
        }

        private void Button_RunHammer_Drop(object sender, DragEventArgs e)
        {
            DropHelper(e, Globals.HammerExec, ".vmf");
        }

        /////////////////////////////////////////////////////////////////////////

        private void Button_SetRegionEnUS_Click(object sender, RoutedEventArgs e)
        {
            ProcessUtil.OpenExecWithArgs("rundll32.exe", "shell32.dll,Control_RunDLL intl.cpl,,0");
            Win32.SetComboboxSelectIndex(501);
        }

        private void Button_SetRegionZhCN_Click(object sender, RoutedEventArgs e)
        {
            ProcessUtil.OpenExecWithArgs("rundll32.exe", "shell32.dll,Control_RunDLL intl.cpl,,0");
            Win32.SetComboboxSelectIndex(0);
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

        /////////////////////////////////////////////////////////////////////////

        private void CheckBox_IsTopMost_Click(object sender, RoutedEventArgs e)
        {
            this.Topmost = CheckBox_IsTopMost.IsChecked == true;
        }
    }
}
