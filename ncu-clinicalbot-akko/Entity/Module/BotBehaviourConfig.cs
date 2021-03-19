using System;
namespace ncu_clinicalbot_akko.Entity.Module
{
    public class BotBehaviourConfig // 这是对机器人行为控制插件的实体类，用以yaml序列化和反序列化
    {
        public Request request { get; set; }
        public Menu menu { get; set; }

    }

    public class Request
    {
        public string friendRequest { get; set; }
        public string groupRequest { get; set; }
    }

    public class Menu
    {
        public string help { get; set; }
        public string list { get; set; }
        public string info { get; set; }
    }
}
