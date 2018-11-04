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
                var e = ModelsV2.CultureName.cn_de_DE;
                var s = NameAsTypeConverter.CultureNameToString(e);
                var enew = NameAsTypeConverter.StringToCultureName(s);
                Assert.Equal(e, enew);
            }
            {
                var e = ModelsV2.CultureName.FixEmpty;
                var s = NameAsTypeConverter.CultureNameToString(e);
                var enew = NameAsTypeConverter.StringToCultureName(s);
                Assert.Equal(e, enew);
            }
            {
                var e = ModelsV2.CultureName.FixNull;
                var s = NameAsTypeConverter.CultureNameToString(e);
                var enew = NameAsTypeConverter.StringToCultureName(s);
                Assert.Equal(e, enew);
            }
            {
                var s = "de-DE";
                var e = NameAsTypeConverter.StringToCultureName(s);
                var snew = NameAsTypeConverter.CultureNameToString(e);
                Assert.Equal(s, snew);
            }
            {
                var s = "cn_de_DE";
                var e = NameAsTypeConverter.StringToCultureName(s);
                var snew = NameAsTypeConverter.CultureNameToString(e);
                Assert.Equal("de-DE", snew);
            }

            {
                var s = "FixEmpty";
                var e = NameAsTypeConverter.StringToCultureName(s);
                var snew = NameAsTypeConverter.CultureNameToString(e);
                Assert.Equal("", snew);
            }
            {
                var s = "FixNull";
                var e = NameAsTypeConverter.StringToCultureName(s);
                var snew = NameAsTypeConverter.CultureNameToString(e);
                Assert.Null(snew);
            }

            foreach (ModelsV2.CultureName e in Enum.GetValues(typeof(ModelsV2.CultureName)))
            {
                var s = NameAsTypeConverter.CultureNameToString(e);
                var enew = NameAsTypeConverter.StringToCultureName(s);
                Assert.Equal(e, enew);
            }
            foreach (var s in Enum.GetNames(typeof(ModelsV2.CultureName)))
            {
                var e = NameAsTypeConverter.StringToCultureName(s);
                var snew = NameAsTypeConverter.CultureNameToString(e);
                switch (e)
                {
                    case ModelsV2.CultureName.FixEmpty:
                        Assert.Equal("FixEmpty", s);
                        Assert.Equal("", snew);
                        break;
                    case ModelsV2.CultureName.FixNull:
                        Assert.Equal("FixNull", s);
                        Assert.Null(snew);
                        break;
                    default:
                        Assert.Equal(s.Replace("cn_", "").Replace("_", "-"), snew);
                        break;
                }
            }
        }
    }
}
