namespace AtataSamples.PageVerification;

using _ = PlansWithOnVerifyPage;

[Url("plans")]
public sealed class PlansWithOnVerifyPage : Page<_>
{
    public H1<_> Header { get; private set; }

    protected override void OnVerify()
    {
        base.OnVerify();

        PageTitle.Should.Be("Plans - Atata Sample App");
        Header.Should.Be("Plans");
        Content.Should.Contain("Please choose your payment plan");
    }
}
