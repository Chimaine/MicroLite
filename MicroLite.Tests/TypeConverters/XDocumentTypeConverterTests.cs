﻿namespace MicroLite.Tests.TypeConverters
{
    using System;
    using System.Xml.Linq;
    using MicroLite.TypeConverters;
    using Xunit;

    public class XDocumentTypeConverterTests
    {
        public class WhenCallingCanConvert_WithTypeOfXDocument
        {
            [Fact]
            public void TrueShouldBeReturned()
            {
                var typeConverter = new XDocumentTypeConverter();
                Assert.True(typeConverter.CanConvert(typeof(XDocument)));
            }
        }

        public class WhenCallingConvertFromDbValue_AndTheValueIsNotNull
        {
            private object result;
            private ITypeConverter typeConverter = new XDocumentTypeConverter();
            private string value = "<customer><name>fred</name></customer>";

            public WhenCallingConvertFromDbValue_AndTheValueIsNotNull()
            {
                this.result = typeConverter.ConvertFromDbValue(value, typeof(XDocument));
            }

            [Fact]
            public void TheResultShouldBeAnXDocument()
            {
                Assert.IsType<XDocument>(this.result);
            }

            [Fact]
            public void TheResultShouldContainTheSpecifiedValue()
            {
                Assert.Equal(
                    XDocument.Parse(this.value).ToString(SaveOptions.DisableFormatting),
                    ((XDocument)this.result).ToString(SaveOptions.DisableFormatting));
            }
        }

        public class WhenCallingConvertFromDbValue_AndTheValueIsNull
        {
            private object result;
            private ITypeConverter typeConverter = new XDocumentTypeConverter();

            public WhenCallingConvertFromDbValue_AndTheValueIsNull()
            {
                this.result = typeConverter.ConvertFromDbValue(DBNull.Value, typeof(XDocument));
            }

            [Fact]
            public void TheResultShouldBeNull()
            {
                Assert.Null(this.result);
            }
        }

        public class WhenCallingConvertToDbValue_AndTheValueIsNotNull
        {
            private object result;
            private ITypeConverter typeConverter = new XDocumentTypeConverter();
            private XDocument value = XDocument.Parse("<customer><name>fred</name></customer>");

            public WhenCallingConvertToDbValue_AndTheValueIsNotNull()
            {
                this.result = typeConverter.ConvertToDbValue(value, typeof(XDocument));
            }

            [Fact]
            public void TheResultShouldBeAString()
            {
                Assert.IsType<string>(this.result);
            }

            [Fact]
            public void TheResultShouldContainTheSpecifiedValue()
            {
                Assert.Equal(this.value.ToString(SaveOptions.DisableFormatting), this.result);
            }
        }

        public class WhenCallingConvertToDbValue_AndTheValueIsNull
        {
            private object result;
            private ITypeConverter typeConverter = new XDocumentTypeConverter();

            public WhenCallingConvertToDbValue_AndTheValueIsNull()
            {
                this.result = typeConverter.ConvertToDbValue(null, typeof(XDocument));
            }

            [Fact]
            public void TheResultShouldBeNull()
            {
                Assert.Null(this.result);
            }
        }
    }
}