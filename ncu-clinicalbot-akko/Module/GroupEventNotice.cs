using System;
using System.Threading.Tasks;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin.Interfaces;

namespace ncu_clinicalbot_akko.Module
{
    public partial class GroupEventNotice : IGroupMemberJoined, IGroupMemberKicked, IGroupMemberPositiveLeave, IGroupMemberMuted, IGroupMemberPermissionChanged, IGroupMemberUnmuted
    {
        public GroupEventNotice() // 这是一个处理群聊中各种事件的模块
        {

        }

        public async Task<bool> GroupMemberJoined(MiraiHttpSession session, IGroupMemberJoinedEventArgs e) // 群聊有人加入
        {
            IMessageBase plainWelcome = new PlainMessage($"欢迎{e.Member.Name}[{e.Member.Id}]，加入群聊哦！");
            IMessageBase plainAttention = new PlainMessage($"{e.Member.Name}，@{e.Member.Id}，请务必仔细阅读群公告");
            await session.SendGroupMessageAsync(e.Member.Group.Id, plainWelcome, plainAttention); 
            return false; 
        }

        public async Task<bool> GroupMemberKicked(MiraiHttpSession session, IGroupMemberKickedEventArgs e) // 群聊有人被踢出
        {
            IMessageBase plainAttention = new PlainMessage($"不得了呀！{e.Member.Name}，@{e.Member.Id}，被踢了！，大家快跑！");
            await session.SendGroupMessageAsync(e.Member.Group.Id, plainAttention);
            return false;
        }

        public async Task<bool> GroupMemberMuted(MiraiHttpSession session, IGroupMemberMutedEventArgs e) // 群聊有人被禁言
        {
            IMessageBase plainAttention = new PlainMessage($"大家小心！{e.Member.Name}，@{e.Member.Id}，被{e.Operator.Name}禁言了{e.Duration.Hours}小时{e.Duration.Minutes}分钟！");
            await session.SendGroupMessageAsync(e.Member.Group.Id, plainAttention);
            return false;
        }

        public async Task<bool> GroupMemberPermissionChanged(MiraiHttpSession session, IGroupMemberPermissionChangedEventArgs e) // 群聊中有人的权限发生改变
        {
            IMessageBase plainAttention = new PlainMessage($"哦？{e.Member.Name}，@{e.Member.Id}，从{e.Origin}成为了{e.Current}");
            await session.SendGroupMessageAsync(e.Member.Group.Id, plainAttention);
            return false;
        }

        public async Task<bool> GroupMemberPositiveLeave(MiraiHttpSession session, IGroupMemberPositiveLeaveEventArgs e) // 群聊有人主动离开，需要设计多种提示语
        {
            Random ran = new Random(); // 引入随机数，使用系统时间作为随机数种子
            int n = ran.Next(1, 5); // 随机数字限定在1-5
            IMessageBase plainAttention = new PlainMessage($"哦我的天！{e.Member.Name}，@{e.Member.Id}，溜走了！");
            switch (n)
            {
                case 1:
                    plainAttention = new PlainMessage($"{e.Member.Name}，@{e.Member.Id}，因为本群没人陪他or她离开了本群");
                    break;
                case 2:
                    plainAttention = new PlainMessage($"{e.Member.Name}，@{e.Member.Id}，因为芜湖起飞离开了本群");
                    break;
                case 3:
                    plainAttention = new PlainMessage($"{e.Member.Name}，@{e.Member.Id}，因为梦不到仿生羊而离开了本群");
                    break;
                case 4:
                    plainAttention = new PlainMessage($"{e.Member.Name}，@{e.Member.Id}，因为是外星人这个地球是一秒都呆不下去了而离开本群");
                    break;
                case 5:
                    plainAttention = new PlainMessage($"{e.Member.Name}，@{e.Member.Id}，因为忘记打卡而离开了本群");
                    break;
            }
            await session.SendGroupMessageAsync(e.Member.Group.Id, plainAttention);
            return false;
        }

        public async Task<bool> GroupMemberUnmuted(MiraiHttpSession session, IGroupMemberUnmutedEventArgs e) // 群聊中有人被解除禁言
        {
            IMessageBase plainAttention = new PlainMessage($"恭喜！{e.Member.Name}，@{e.Member.Id}，从小黑屋出来了");
            await session.SendGroupMessageAsync(e.Member.Group.Id, plainAttention);
            return false;
        }
    }
}
