using Atata;
using NUnit.Framework;

namespace AtataSamples.ValidationMessagesVerification
{
    [TestFixture]
    public class SignUpTests
    {
        [SetUp]
        public void SetUp()
        {
            AtataContext.Configure().
                UseChrome().
                    WithArguments("start-maximized").
                UseBaseUrl("https://atata-framework.github.io/atata-sample-app/#!/").
                UseCulture("en-us").
                UseNUnitTestName().
                AddNUnitTestContextLogging().
                    WithoutSectionFinish().
                LogNUnitError().
                Build();
        }

        [TearDown]
        public void TearDown()
        {
            AtataContext.Current.CleanUp();
        }

        [Test]
        public void SignUp_Validation_Required()
        {
            Go.To<SignUpPage>().
                SignUp.Click().
                ValidationMessages[x => x.FirstName].Should.Equal("is required").
                ValidationMessages[x => x.LastName].Should.Equal("is required").
                ValidationMessages[x => x.Email].Should.Equal("is required").
                ValidationMessages[x => x.Password].Should.Equal("is required").
                ValidationMessages[x => x.Agreement].Should.Equal("is required").
                ValidationMessages.Should.HaveCount(5);
        }

        [Test]
        public void SignUp_Validation_Required_UsingExtensions()
        {
            Go.To<SignUpPage>().
                SignUp.Click().
                ValidationMessages[x => x.FirstName].Should.BeRequired().
                ValidationMessages[x => x.LastName].Should.BeRequired().
                ValidationMessages[x => x.Email].Should.BeRequired().
                ValidationMessages[x => x.Password].Should.BeRequired().
                ValidationMessages[x => x.Agreement].Should.BeRequired().
                ValidationMessages.Should.HaveCount(5);
        }

        [Test]
        public void SignUp_Validation_MinLength()
        {
            Go.To<SignUpPage>().
                FirstName.Set("a").
                LastName.Set("a").
                Password.Set("a").
                SignUp.Click().
                ValidationMessages[x => x.FirstName].Should.Equal("minimum length is 2").
                ValidationMessages[x => x.LastName].Should.Equal("minimum length is 2").
                ValidationMessages[x => x.Password].Should.Equal("minimum length is 6");
        }

        [Test]
        public void SignUp_Validation_MinLength_UsingExtensions()
        {
            Go.To<SignUpPage>().
                FirstName.Set("a").
                LastName.Set("a").
                Password.Set("a").
                SignUp.Click().
                ValidationMessages[x => x.FirstName].Should.HaveMinLength(2).
                ValidationMessages[x => x.LastName].Should.HaveMinLength(2).
                ValidationMessages[x => x.Password].Should.HaveMinLength(6);
        }

        [Test]
        public void SignUp_Validation_IncorrectEmail()
        {
            Go.To<SignUpPage>().
                Email.Set("some@email").
                SignUp.Click().
                ValidationMessages[x => x.Email].Should.Equal("has incorrect format").
                Email.Append(".com").
                SignUp.Click().
                ValidationMessages[x => x.Email].Should.Not.Exist();
        }

        [Test]
        public void SignUp_Validation_IncorrectEmail_UsingExtensions()
        {
            Go.To<SignUpPage>().
                Email.Set("some@email").
                SignUp.Click().
                ValidationMessages[x => x.Email].Should.HaveIncorrectFormat().
                Email.Append(".com").
                SignUp.Click().
                ValidationMessages[x => x.Email].Should.Not.Exist();
        }
    }
}
