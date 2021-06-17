using Easy.Logger.Interfaces;
using Easy.Logger;

namespace RPNApi.UnitTests
{
    public class TestHelper
    {
        public static IEasyLogger GetLogger<T>()
        {
            ILogService logService = Log4NetService.Instance;
            IEasyLogger logger = logService.GetLogger<T>();
            logger.Debug("Test logger created");
            return logger;
        }
    }
}