using Atata;
using NUnit.Framework;

namespace AtataSamples.JsonConfiguration.MultiEnvironment
{
    [SetUpFixture]
    public class SetUpFixture
    {
        [OneTimeSetUp]
        public void GlobalSetUp()
        {
            AtataContext.GlobalConfiguration.
                ApplyJsonConfig<AppConfig>().
#if DEV
                ApplyJsonConfig<AppConfig>(environmentAlias: "DEV");
#elif QA
                ApplyJsonConfig<AppConfig>(environmentAlias: "QA");
#elif STAGING
                ApplyJsonConfig<AppConfig>(environmentAlias: "STAGING");
#endif
        }
    }
}
