using NUnit.Framework;

namespace GUtilsUnity.Extensions.Tests
{
    [TestFixture]
    public class TextPluralizeExtensions
    {
        [TestCase(
            "{0} $[plural_english(0,Plural)] Potato$[plural_english(1,Singular,Plural)]",
            new object[] { 1, 2f },
            ExpectedResult = "{0}  PotatoPlural"
        )]
        [TestCase(
            "{0} $[plural_english(0,Plural)] Potato$[plural_english(1,Singular,Plural)]",
            new object[] { 2, 1f },
            ExpectedResult = "{0} Plural PotatoSingular"
        )]
        [TestCase(
            "{0} $[plural_english(0,Plural)] Potato$[plural_english(1,Singular,Plural)]",
            new object[] { 2, 2f },
            ExpectedResult = "{0} Plural PotatoPlural"
        )]
        public static string Pluralize_OnString_WorksAsExpected(string toPluralize, object[] parameters)
        {
            return StringPluralizationExtensions.Pluralize(toPluralize, parameters);
        }

        [TestCase(
            "{0} Potato$[plural_english(0,es)]",
            new object[] { 1 },
            ExpectedResult = "1 Potato"
        )]
        [TestCase(
            "{0} Potato$[plural_english(0,es)]",
            new object[] { 0.9f },
            ExpectedResult = "0.9 Potatoes"
        )]
        [TestCase(
            "{0} $[plural_english(0,Potato,Potatoes)]",
            new object[] { 2d },
            ExpectedResult = "2 Potatoes"
        )]
        [TestCase(
            "{0} $[plural_english(0,Potato,Potatoes)]$[plural_english(1, Plural)]",
            new object[] { 2d, 4 },
            ExpectedResult = "2 Potatoes Plural"
        )]
        public static string PluralizeAndFormat_OnString_WorksAsExpected(string toPluralize, object[] parameters)
        {
            string pluralized = StringPluralizationExtensions.Pluralize(toPluralize, parameters);
            string formatted = string.Format(pluralized, parameters);

            return formatted;
        }
    }
}
