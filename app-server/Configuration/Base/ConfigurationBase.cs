using System.Collections.Generic;

namespace CardAPI.Configuration.Base
{
    public class ConfigurationBase
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
