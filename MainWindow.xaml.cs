using System.IO;
using System.Windows;

using L4D2DevTools.Utils;

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

        private void Label_VPKUnpack_Drop(object sender, DragEventArgs e)
        {
            DropHelper(e, ".\\AppData\\VPK\\vpk.exe", ".vpk");
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
    }
}
