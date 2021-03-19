using System;
namespace ncu_clinicalbot_akko.Entity.Core
{
    public class CoreConfig // 这是对MiraiCS的核心参数账号、密码、端口、authkey的实体类，用以yaml的序列化和反序列化
    {
        public string account { get; set; }
        public string ip { get; set; }
        public string port { get; set; }
        public string authkey { get; set; }
    }
}
