using Microsoft.Extensions.Configuration;

namespace MatSource.Library.Helper
{
    public class MyConfigration
    {
        private IConfiguration Config;
        public MyConfigration()
        {


        }
        public MyConfigration(IConfiguration _configuration)
        {
            Config = _configuration;

        }
        public IConfiguration myConfigration()
        {
            return Config;
        }
    }
    public interface IMyConfigration
    {

        IConfiguration myConfigration();

    }

}
