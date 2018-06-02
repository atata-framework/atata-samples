using Atata;

namespace AtataSamples.ValidationMessagesVerification
{
    public static class ValidationMessageExtensions
    {
        public static TOwner BeRequired<TOwner>(this IFieldVerificationProvider<string, ValidationMessage<TOwner>, TOwner> should)
            where TOwner : PageObject<TOwner>
        {
            return should.Equal("is required");
        }

        public static TOwner HaveIncorrectFormat<TOwner>(this IFieldVerificationProvider<string, ValidationMessage<TOwner>, TOwner> should)
            where TOwner : PageObject<TOwner>
        {
            return should.Equal("has incorrect format");
        }

        public static TOwner HaveMinLength<TOwner>(this IFieldVerificationProvider<string, ValidationMessage<TOwner>, TOwner> should, int length)
            where TOwner : PageObject<TOwner>
        {
            return should.Equal($"minimum length is {length}");
        }

        public static TOwner HaveMaxLength<TOwner>(this IFieldVerificationProvider<string, ValidationMessage<TOwner>, TOwner> should, int length)
            where TOwner : PageObject<TOwner>
        {
            return should.Equal($"maximum length is {length}");
        }
    }
}
