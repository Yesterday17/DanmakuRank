using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml;

namespace DanmakuRank
{
    public class MainClass : BilibiliDM_PluginFramework.DMPlugin
    {
        public static string configFileLocation = Environment.SpecialFolder.MyDocuments + "\弹幕姬\Config\countress.xml";

        public MainClass()
        {
            this.ReceivedDanmaku += Class1_ReceivedDanmaku;
            this.PluginAuth = "昨日的十七号";
            this.PluginName = "弹幕统计姬";
            this.PluginCont = "yesterday17@yesterday17.cn";
            this.PluginVer = "v0.0.1";
        }

        private void Class1_ReceivedDanmaku(object sender, BilibiliDM_PluginFramework.ReceivedDanmakuArgs e)
        {
            //throw new NotImplementedException();
        }

        public override void Admin()
        {
            base.Admin();
            Console.WriteLine("Hello World");
        }

        public override void Stop()
        {
            AddDM("统计姬已停止。");
            base.Stop();
        }

        public override void Start()
        {
            AddDM("统计姬已开始为您效劳。");
            base.Start();
        }
    }
}
