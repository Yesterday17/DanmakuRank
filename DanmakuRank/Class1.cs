using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace DanmakuRank
{
    public class MainClass : BilibiliDM_PluginFramework.DMPlugin
    {
        public static string configFileLocation = Environment.SpecialFolder.MyDocuments + "\\弹幕姬\\Config\\countress.xml";
        public static Data _data = new Data();

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
            //首先分析rawData中cmd是否等于DANMU_MSG然后进行操作
            string cmd_tmp = e.Danmaku.RawData.Substring(e.Danmaku.RawData.IndexOf("\"cmd\"") + 7);
            string cmd = cmd_tmp.Substring(0, cmd_tmp.IndexOf("\""));


            if (cmd == "DANMU_MSG")
            {
                if (_data.getPersonByName(e.Danmaku.CommentUser) != null)
                {
                    _data.getPersonByName(e.Danmaku.CommentUser).AddDanmaku(e.Danmaku);
                }
                else
                {
                    _data.AddPerson(e.Danmaku);
                }
                //System.Windows.Forms.MessageBox.Show(e.Danmaku.CommentText + "——by " + e.Danmaku.CommentUser);
            }
        }

        public override void Admin()
        {
            base.Admin();
            Console.WriteLine("Hello World");
        }

        public override void Stop()
        {
            //AddDM("统计姬已停止。");   
            base.Stop();
        }

        public override void Start()
        {
            //这个有些不大对劲= =是新开了一个window然后发弹幕的= =
            Bililive_dm.MainWindow x = new Bililive_dm.MainWindow();
            x.AddDMText("123", "456");

            Bililive_dm.MainOverlay y = x.overlay;

            y.Hide();
            //x.Left = 10000;
            //x.Top = 10000;
            base.Start();
        }
    }
}
