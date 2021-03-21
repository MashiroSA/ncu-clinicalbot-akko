using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin.Interfaces;
using ncu_clinicalbot_akko.Extensions;

namespace ncu_clinicalbot_akko.Module
{
    public partial class MedicalQuestionBasic : IGroupMessage // 填写接口
    {
        private long botQQ;
        public MedicalQuestionBasic(long botQQ) // 构造方法，可以保持空
        {
            this.botQQ = botQQ;
        }

        public async Task<bool> GroupMessage(MiraiHttpSession session, IGroupMessageEventArgs e) 
        {
            if (e.Sender.Id == botQQ)
            {
                return false;
            }
            string strOriginal = string.Join(null, (IEnumerable<IMessageBase>)e.Chain); // 取消息
            string[] strArray = strOriginal.Split(new char[2] { '[', ']' }); // 分割Mirai码部分
            string str = strArray[2];
            
            if (str.Equals("随机常规题"))
            {
                MedicalQuestionBasicExtension m = new MedicalQuestionBasicExtension();
                IMessageBase plain = new PlainMessage("检索可能出现错误");
                string message = m.GetRandomMedicalQuestion();
                plain = new PlainMessage(message);
                
                IMessageBase atTarget = new AtMessage(e.Sender.Id);
                await session.SendGroupMessageAsync(e.Sender.Group.Id, atTarget, plain);
            }else if (str.Contains("查答案"))
            {
                // 空格分割处理
                int blank = str.Split(' ').Length - 1;
                string tIDs = str.Substring((str.IndexOf(" ")), (str.Length - str.IndexOf(" ")));
                tIDs = tIDs.Replace(" ", "");
                int tID = int.Parse(tIDs);
                
                MedicalQuestionBasicExtension m = new MedicalQuestionBasicExtension();
                IMessageBase plain = new PlainMessage("检索可能出现错误");
                string message = m.GetMedicalAnswer(tID);
                plain = new PlainMessage(message);
                
                IMessageBase atTarget = new AtMessage(e.Sender.Id);
                await session.SendGroupMessageAsync(e.Sender.Group.Id, atTarget, plain);
            }else if (str.Contains("查常规题"))
            {
                // 空格分割处理
                int blank = str.Split(' ').Length - 1;
                string tIDs = str.Substring((str.IndexOf(" ")), (str.Length - str.IndexOf(" ")));
                tIDs = tIDs.Replace(" ", "");
                int tID = int.Parse(tIDs);
                
                MedicalQuestionBasicExtension m = new MedicalQuestionBasicExtension();
                IMessageBase plain = new PlainMessage("检索可能出现错误");
                string message = m.GetMedicalQuestion(tID);
                plain = new PlainMessage(message);
                
                IMessageBase atTarget = new AtMessage(e.Sender.Id);
                await session.SendGroupMessageAsync(e.Sender.Group.Id, atTarget, plain); 
            }
            
            return false; // 消息阻隔
        }
    }
}