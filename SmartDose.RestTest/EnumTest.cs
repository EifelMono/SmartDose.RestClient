using System;
using System.Globalization;
using System.Linq;
using SmartDose.RestDomain.Converter;
using Xunit;
using ModelsV2 = SmartDose.RestDomain.Models.V2;

namespace SmartDose.RestTest
{
    public class EnumTest
    {
        [Fact]
        public void GenerateCultureEnum()
        {
            var cinfo = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);
            var list = cinfo.Select(c => string.IsNullOrEmpty(c.Name) ? "" : "cin_" + c.Name.Replace("-", "_")).Where(s => !string.IsNullOrEmpty(s)).ToList();
            var enums = string.Join(",", list);
        }


        [Fact]
        public void TestCultureEnum()
        {
            {
                var e = ModelsV2.CultureInfoName.cin_de_DE;
                var s = NameAsTypeConverter.CultureInfoNameToString(e);
                var enew = NameAsTypeConverter.StringToCultureInfoName(s);
                Assert.Equal(e, enew);
            }
            {
                var e = ModelsV2.CultureInfoName.FixEmpty;
                var s = NameAsTypeConverter.CultureInfoNameToString(e);
                var enew = NameAsTypeConverter.StringToCultureInfoName(s);
                Assert.Equal(e, enew);
            }
            {
                var e = ModelsV2.CultureInfoName.FixNull;
                var s = NameAsTypeConverter.CultureInfoNameToString(e);
                var enew = NameAsTypeConverter.StringToCultureInfoName(s);
                Assert.Equal(e, enew);
            }
            {
                var s = "de-DE";
                var e = NameAsTypeConverter.StringToCultureInfoName(s);
                var snew = NameAsTypeConverter.CultureInfoNameToString(e);
                Assert.Equal(s, snew);
            }
            {
                var s = "cin_de_DE";
                var e = NameAsTypeConverter.StringToCultureInfoName(s);
                var snew = NameAsTypeConverter.CultureInfoNameToString(e);
                Assert.Equal("de-DE", snew);
            }

            {
                var s = "FixEmpty";
                var e = NameAsTypeConverter.StringToCultureInfoName(s);
                var snew = NameAsTypeConverter.CultureInfoNameToString(e);
                Assert.Equal("", snew);
            }
            {
                var s = "FixNull";
                var e = NameAsTypeConverter.StringToCultureInfoName(s);
                var snew = NameAsTypeConverter.CultureInfoNameToString(e);
                Assert.Null(snew);
            }

            foreach (ModelsV2.CultureInfoName e in Enum.GetValues(typeof(ModelsV2.CultureInfoName)))
            {
                var s = NameAsTypeConverter.CultureInfoNameToString(e);
                var enew = NameAsTypeConverter.StringToCultureInfoName(s);
                Assert.Equal(e, enew);
            }
            foreach (var s in Enum.GetNames(typeof(ModelsV2.CultureInfoName)))
            {
                var e = NameAsTypeConverter.StringToCultureInfoName(s);
                var snew = NameAsTypeConverter.CultureInfoNameToString(e);
                switch (e)
                {
                    case ModelsV2.CultureInfoName.FixEmpty:
                        Assert.Equal("FixEmpty", s);
                        Assert.Equal("", snew);
                        break;
                    case ModelsV2.CultureInfoName.FixNull:
                        Assert.Equal("FixNull", s);
                        Assert.Null(snew);
                        break;
                    default:
                        Assert.Equal(s.Replace("cin_", "").Replace("_", "-"), snew);
                        break;
                }
            }
        }
    }
}
