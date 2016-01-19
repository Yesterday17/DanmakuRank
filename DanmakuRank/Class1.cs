using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

//用来测试的直播间：http://live.bilibili.com/52507
//以及：http://live.bilibili.com/23719
//感谢这位正好开着直播并且发弹幕的人还不少的UP啦╰(*°▽°*)╯

namespace DanmakuRank
{
    public class MainClass : BilibiliDM_PluginFramework.DMPlugin
    {
        public static string configFileLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\弹幕姬\\Config\\Countress" + ".xml";
        //下个版本加入直播房间切换的统计 这个版本就自己用自己的别给别人用吧= =
        public static Data _data = new Data();
        //不知道为什么加进来的 总感觉会有用但是用在哪里给忘了 估计下个版本更新时删除或用上（见上面 我想起来了= =）
        public static bool startLive;

        public MainClass()
        {
            this.ReceivedDanmaku += Countress_ReceivedDanmaku;
            this.Disconnected += Countress_Disconnected;
            this.PluginAuth = "昨日的十七号";
            this.PluginName = "弹幕统计姬";
            this.PluginDesc = "做完之前懒得写介绍辣O(∩_∩)O~";
            this.PluginCont = "yesterday17@yesterday17.cn";
            this.PluginVer = "v0.0.1";
            startLive = false;
        }

        private void Countress_ReceivedDanmaku(object sender, BilibiliDM_PluginFramework.ReceivedDanmakuArgs e)
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
                    //_data.AddPerson(e.Danmaku);
                    _data.AddPerson(new Data.Person(e.Danmaku.RawData));
                    _data.getPersonByName(e.Danmaku.CommentUser).AddDanmaku(e.Danmaku);
                }
                //System.Windows.Forms.MessageBox.Show(e.Danmaku.CommentText + "——by " + e.Danmaku.CommentUser);
            }
        }

        private void Countress_Disconnected(object sender, BilibiliDM_PluginFramework.DisconnectEvtArgs e)
        {
            SaveData();
        }

        public override void Admin()
        {
            base.Admin();
        }

        public override void Stop()
        {
            SaveData();
            base.Stop();
        }

        public override void Start()
        {
            //这个有些不大对劲= =是新开了一个window然后发弹幕的= =
            //Bililive_dm.MainWindow x = new Bililive_dm.MainWindow();
            //x.AddDMText("123", "456");

            //Bililive_dm.MainOverlay y = x.overlay;

            //y.Hide();
            //x.Left = 10000;
            //x.Top = 10000;

            LoadData();
            base.Start();
        }

        public override void DeInit()
        {
            SaveData();
            base.DeInit();
        }

        public void SaveData()
        {
            _data.Save(configFileLocation);
            AddDM("统计姬已停止，数据保存在" + configFileLocation);
        }
        
        public void LoadData()
        {
            _data.Load(configFileLocation)
;            AddDM("统计姬已开始运行！断开连接，停用插件或关闭弹幕姬时都会自动保存数据！");
        }
    }
}
