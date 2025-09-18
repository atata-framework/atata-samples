namespace AtataSamples.Xunit;

public class AtataSetUpFixture
{
    public AtataSetUpFixture()
    {
        AtataContext.GlobalConfiguration
            .UseChrome()
                .WithArguments("start-maximized", "disable-search-engine-choice-screen")
            .UseBaseUrl("https://demo.atata.io/")
            .UseCulture("en-US");

        AtataContext.GlobalConfiguration.AutoSetUpDriverToUse();
    }
}
