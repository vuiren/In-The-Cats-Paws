namespace Game_Project.Scripts.CommonLayer.Factories
{
    public class LoggerFactory
    {
        public static IMyLogger Create(object param)
        {
            var logger = new MyLogger(param);
            return logger;
        }
    }
}