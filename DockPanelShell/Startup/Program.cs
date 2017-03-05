using System;
using System.Windows.Forms;
using UIShell.OSGi;

namespace Startup
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            //创建OSGi内核
            using (var runtime = new BundleRuntime())
            {
                //启动内核，加载插件
                runtime.Start();
                //显示主窗口
                var form = runtime.GetFirstOrDefaultService<Form>();
                if (form == null)
                {
                    return;
                }
                Application.Run(form);
            }
        }
    }
}