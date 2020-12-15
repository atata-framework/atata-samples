﻿using Atata;
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
                    WithLocalDriverPath().
                UseBaseUrl("https://demo.atata.io/").
                UseCulture("en-US").
                UseAllNUnitFeatures().
                Build();
        }

        [TearDown]
        public void TearDown()
        {
            AtataContext.Current?.CleanUp();
        }

        [Test]
        public void Validation_Required()
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
        public void Validation_Required_UsingExtensions()
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
        public void Validation_MinLength()
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
        public void Validation_MinLength_UsingExtensions()
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
        public void Validation_IncorrectEmail()
        {
            Go.To<SignUpPage>().
                Email.Set("some@email").
                SignUp.Click().
                ValidationMessages[x => x.Email].Should.Equal("has incorrect format").
                Email.Type(".com").
                SignUp.Click().
                ValidationMessages[x => x.Email].Should.Not.Exist();
        }

        [Test]
        public void Validation_IncorrectEmail_UsingExtensions()
        {
            Go.To<SignUpPage>().
                Email.Set("some@email").
                SignUp.Click().
                ValidationMessages[x => x.Email].Should.HaveIncorrectFormat().
                Email.Type(".com").
                SignUp.Click().
                ValidationMessages[x => x.Email].Should.Not.Exist();
        }
    }
}
