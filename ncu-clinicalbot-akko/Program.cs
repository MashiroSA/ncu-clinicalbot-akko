using Mirai_CSharp;
using Mirai_CSharp.Models;
using ncu_clinicalbot_akko.Core;
using ncu_clinicalbot_akko.Entity.Core;
using ncu_clinicalbot_akko.Module;
using System;
using System.IO;
using System.Threading.Tasks;
using ncu_clinicalbot_akko.Extensions;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ncu_clinicalbot_akko
{
    public static class Program
    {
        public static async Task Main()
        {
            Init init = new Init(); // 初始化参数文件
            
            /*
             * 下列是对coreconfig反序列化读取各项配置
             * todo:对相关的coreconfig进行重写 @author MashiroSA 
             */
            var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
            string s = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"/config/CoreConfig.yaml");
            var c = deserializer.Deserialize<CoreConfig>(s);
            
            string ip = c.ip; // ip
            int port = int.Parse(c.port); // 端口
            string authkey = c.authkey; // http authkey
            long qq = long.Parse(c.account); // 账号
            
            MiraiHttpSessionOptions options = new MiraiHttpSessionOptions(ip, port, authkey); //MiraiQQ Http
            await using MiraiHttpSession session = new MiraiHttpSession();
            
            /*
             * 插件装载
             */
            BotBehaviourControl botBehaviourControl = new BotBehaviourControl(qq); // 机器人行为控制，记得传入机器人QQ号
            GroupEventNotice groupEventNotice = new GroupEventNotice(); // 群事件消息提醒
            MedicalQuestionBasic medicalQuestionBasic = new MedicalQuestionBasic(qq); // 医学题目查询，记得传入机器人QQ号
            
            session.AddPlugin(botBehaviourControl);
            TraceLog.Log("", "插件装载:机器人行为处理插件装载成功");
            session.AddPlugin(groupEventNotice);
            TraceLog.Log("", "插件装载:群事件消息提醒插件装载成功");
            session.AddPlugin(medicalQuestionBasic);
            TraceLog.Log("", "插件装载:医学题库插件装载成功");
            /*
             * QQ
             */
            await session.ConnectAsync(options, qq); // QQ号
            while (true)
            {
                if (await Console.In.ReadLineAsync() == "exit")
                {
                    return;
                }
            }
        }
    }
}
