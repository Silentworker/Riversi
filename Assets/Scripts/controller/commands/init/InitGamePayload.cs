namespace Assets.Scripts.controller.commands.init
{
    public class InitGamePayload
    {
        public bool Resume { get; set; }
        public byte Players { get; set; }
        public byte Side { get; set; }
    }
}