using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanmakuRank
{
    public class Data
    {
        public class Person
        {
            public Person()
            {
                this.name = null;
                this.id = 0;
                this.id_s = id.ToString();
                this.isAdmin = false;
                this.isVip = false;
                this.level = -1;
                this.rank = null;

                this.danmaku = new List<BilibiliDM_PluginFramework.DanmakuModel>();
            }

            public Person(string u_name, int u_id, bool u_isAdmin, bool u_isVip, int u_level, string u_rank)
            {
                this.name = u_name;
                this.id = u_id;
                this.id_s = u_id.ToString();
                this.isAdmin = u_isAdmin;
                this.isVip = u_isVip;
                this.level = u_level;
                this.rank = u_rank;
                this.danmaku = new List<BilibiliDM_PluginFramework.DanmakuModel>();
            }

            public Person(int u_id)
            {
                //暂时不实现，从bilibili服务器上获取信息
            }

            public Person(string rawData)
            {
                this.name = "";
                this.id = 0;
                this.id_s = "";
                this.isAdmin = false;
                this.isVip = false;
                this.level = -1;
                this.rank = "";

                this.danmaku = new List<BilibiliDM_PluginFramework.DanmakuModel>();
            }

            public Person(BilibiliDM_PluginFramework.DanmakuModel d)
            {
                this.name = d.CommentUser;
                this.id = Convert.ToInt32(getJson("\",[", ",\"", d.RawData));
                this.id_s = id.ToString();
                this.isAdmin = d.isAdmin;
                this.isVip = d.isVIP;
                this.level = -1;
                this.rank = "";

                this.danmaku = new List<BilibiliDM_PluginFramework.DanmakuModel>();
            }

            public void update(string u_name, bool u_isAdmin, bool u_isVip, string u_rank)
            {
                this.name = u_name;
                this.isAdmin = u_isAdmin;
                this.isVip = u_isVip;
                this.rank = u_rank;
            }

            public string Name
            {
                get { return this.name; }
                set { this.name = value; }
            }

            public int Id
            {
                get { return this.id; }
                set { this.id = value; }
            }

            public string ID
            {
                get { return this.id_s; }
                set { this.id_s = value; }
            }

            public bool Admin
            {
                get { return this.isAdmin; }
                set { this.isAdmin = value; }
            }

            public bool Vip
            {
                get { return this.isVip; }
                set { this.isVip = value; }
            }

            public int Level
            {
                get { return this.level; }
                set { this.level = value; }
            }

            public string Rank
            {
                get{ return this.rank; }
                set { this.rank = value; }
            }

            private string name;
            private int id;
            private string id_s;
            private bool isAdmin;
            private bool isVip;
            private int level;
            private string rank;
            private List<BilibiliDM_PluginFramework.DanmakuModel> danmaku;


            public void AddDanmaku(BilibiliDM_PluginFramework.DanmakuModel dm)
            {
                danmaku.Add(dm);
            }

            public int getDanmakuNum()
            {
                return danmaku.Count(); 
            }

            /// <summary>
            /// 获取制定Json中的值信息
            /// </summary>
            /// <param name="key">开头的关键字</param>
            /// <param name="key_end">结束的关键字</param>
            /// <param name="json">Json原文</param>
            /// <returns></returns>
            private string getJson(string key, string key_end, string json)
            {
                string tmp = json.Substring(json.IndexOf(key) + key.Length);
                string ans = tmp.Substring(0, tmp.IndexOf(key_end)- key_end.Length + 2);
                return ans;
            }


        }

        private List<Person> dt = new List<Person>();

        public void AddPerson(Person x)
        {
            //Person x = new Person(name, id, isAdmin, isVip, level, rank);
            dt.Add(x);
        }

        public void AddPerson(BilibiliDM_PluginFramework.DanmakuModel x)
        {
            dt.Add(new Person(x));
            this.getPersonByName(x.CommentUser).AddDanmaku(x);
        }

        public Person getPersonByName(string u_name)
        {
            //Person x = new Person(uid);
            //dt.Find((Person y)=>y.Name==u_name);
            //return y;
            return dt.Find(
                delegate(Person y)
                {
                    return y.Name == u_name;
                }
                );
        }

        public Person getPersonById(int u_id)
        {
            return dt.Find((Person y) => y.Id == u_id);
        }

        public Person getPersonById(string u_id)
        {
            return dt.Find((Person y) => y.ID == u_id);
        }

        public void editByPerson(Person old, Person _new)
        {
            Person f = dt.Find((Person y)=>y==old);

        }

        public object getType()
        {
            return dt.GetType();
        }

    }
}
