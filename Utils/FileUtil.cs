using System.IO;
using System.Reflection;

namespace L4D2DevTools.Utils
{
    public static class FileUtil
    {
        /// <summary>
        /// 从资源文件中抽取资源文件
        /// </summary>
        /// <param name="resFileName"></param>
        /// <param name="outputFile"></param>
        public static void ExtractResFile(string resFileName, string outputFile)
        {
            BufferedStream inStream = null;
            FileStream outStream = null;

            try
            {
                var asm = Assembly.GetExecutingAssembly();
                inStream = new BufferedStream(asm.GetManifestResourceStream(resFileName));
                outStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write);

                var buffer = new byte[1024];
                int length;

                while ((length = inStream.Read(buffer, 0, buffer.Length)) > 0)
                    outStream.Write(buffer, 0, length);

                outStream.Flush();
            }
            finally
            {
                outStream?.Close();
                inStream?.Close();
            }
        }
    }
}
