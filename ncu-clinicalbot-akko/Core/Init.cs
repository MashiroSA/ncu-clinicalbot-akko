using System;
using System.Data.SQLite;
using System.IO;
using ncu_clinicalbot_akko.Entity.Core;
using ncu_clinicalbot_akko.Entity.Module;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ncu_clinicalbot_akko.Core
{
    public class Init
    {
        public Init()
        {
            InitDirectory();
            InitCoreConfig();
            InitBotBehaviourConfig();
            InitDB();
        }

        private int InitDirectory() // 初始化文件夹
        {
            string currPath = AppDomain.CurrentDomain.BaseDirectory;
            string configPath = currPath + @"/config/";
            string dataPath = currPath + @"/data/";
            string dbPath = AppDomain.CurrentDomain.BaseDirectory + @"/db/";
            if (false == System.IO.Directory.Exists(configPath))
            {
                System.IO.Directory.CreateDirectory(configPath);
            }
            if (false == System.IO.Directory.Exists(dataPath))
            {
                System.IO.Directory.CreateDirectory(dataPath);
            }
            if (false == System.IO.Directory.Exists(dbPath))
            {
                System.IO.Directory.CreateDirectory(dbPath);
            }
            TraceLog.Log("", "初始化:数据文件夹:执行成功");
            return 0;
        }

        private int InitCoreConfig() // 初始化核心配置文件
        {
            string configPath = AppDomain.CurrentDomain.BaseDirectory + @"/config/";
            string configFilePath = configPath + @"CoreConfig.yaml";
            if (false == System.IO.File.Exists(configFilePath))
            {
                FileStream fs = new FileStream(configFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                sw.Close();
                var cc = new CoreConfig
                {
                    account = "2227391033",
                    ip = "127.0.0.1",
                    port = "9999",
                    authkey = "1145141919810",
                };
                var serializer = new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
                var yaml = serializer.Serialize(cc);
                FileStream afs = new FileStream(configFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter asw = new StreamWriter(afs);
                asw.Write(yaml);
                asw.Close();
                TraceLog.Log("", "初始化:InitCoreConfig:执行成功");
            }
            return 0;
        }

        private int InitBotBehaviourConfig()
        {
            string configPath = AppDomain.CurrentDomain.BaseDirectory + @"/config/";
            string configFilePath = configPath + @"BotBehaviourConfig.yaml";
            if (false == System.IO.File.Exists(configFilePath))
            {
                FileStream fs = new FileStream(configFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                sw.Close();
                var bbc = new BotBehaviourConfig
                {
                    request = new Request
                    {
                        friendRequest = "f",
                        groupRequest = "f",
                    },
                    menu = new Menu
                    {
                        help = "NCUAkkoBot Core Menu \n" +
                        "输入以下指令查看详情\n" +
                        ".list：查看可用指令\n" +
                        ".info：查看项目详情",

                        list = "可用指令如下：（加号代表空格）\n" +
                               "- 随机常规题:获取随机常规医学题\n" +
                               "- 查答案+id:查询常规题目库中指定id的答案\n" +
                               "- 查常规题+id:查询指定id的题目",
                        
                        info = "Project Alice " +
                        "- 开源的屑QQBOT\n" +
                        "- 使用项目:Mirai、MiraiCS、MiraiHttp\n" +
                        "- 开发者:MashiroSA",
                    }
                };
                var serializer = new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
                var yaml = serializer.Serialize(bbc);
                FileStream afs = new FileStream(configFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter asw = new StreamWriter(afs);
                asw.Write(yaml);
                asw.Close();
                TraceLog.Log("", "初始化:InitBotBehaviourConfig:执行成功");
            }
            return 0;
        }

        private int InitDB()
        {
            string dbPath = AppDomain.CurrentDomain.BaseDirectory + @"/db/";
            string dbQAFile = dbPath + @"QA.db";

            if (false == System.IO.File.Exists(dbQAFile)) // 初始化鉴别
            {
                SqliteSystem.NewDbFile(dbQAFile);
                
                SQLiteConnection sqliteConn = new SQLiteConnection("data source=" + dbQAFile);
                if (sqliteConn.State != System.Data.ConnectionState.Open)
                {
                    sqliteConn.Open();
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.Connection = sqliteConn;
                    cmd.CommandText = "CREATE TABLE " + "QA" +
                                      "(ID int, Question varchar, A varchar, B varchar, C varchar, D varchar, Answer varchar)";
                    cmd.ExecuteNonQuery();
                }

                sqliteConn.Close();
            }

            return 0;
        }
    }
}
