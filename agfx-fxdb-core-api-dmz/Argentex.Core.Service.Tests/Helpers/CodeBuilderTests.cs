using Argentex.Core.Service.Helpers;
using Xunit;

namespace Argentex.Core.Service.Tests.Helpers
{
    public class CodeBuilderTests
    {
        [Fact]
        public void FormatTradeCode_With_Client_Company_ID_Less_Than_Ten_Thousand_And_With_Trade_Count_Less_Than_Ten_Thousand()
        {
            //Arrange
            //Act
            var tradeCode1 = CodeBuilder.FormatTradeCode(1, 1);
            var tradeCode01 = CodeBuilder.FormatTradeCode(10, 10);
            var tradeCode001 = CodeBuilder.FormatTradeCode(100, 100);
            var tradeCode0001 = CodeBuilder.FormatTradeCode(1000, 1000);

            //Assert
            Assert.Equal("AG0001-0001", tradeCode1);
            Assert.Equal("AG0010-0010", tradeCode01);
            Assert.Equal("AG0100-0100", tradeCode001);
            Assert.Equal("AG1000-1000", tradeCode0001);
        }

        [Fact]
        public void FormatTradeCode_With_Client_Company_ID_More_Than_Ten_Thousand_And_With_Trade_Count_Less_Than_Ten_Thousand()
        {
            //Arrange
            //Act
            var tradeCode1 = CodeBuilder.FormatTradeCode(1, 20000);
            var tradeCode01 = CodeBuilder.FormatTradeCode(10, 20000);
            var tradeCode001 = CodeBuilder.FormatTradeCode(100, 20000);
            var tradeCode0001 = CodeBuilder.FormatTradeCode(1000, 20000);

            //Assert
            Assert.Equal("AG20000-0001", tradeCode1);
            Assert.Equal("AG20000-0010", tradeCode01);
            Assert.Equal("AG20000-0100", tradeCode001);
            Assert.Equal("AG20000-1000", tradeCode0001);
        }

        [Fact]
        public void FormatTradeCode_With_Client_Company_ID_Less_Than_Ten_Thousand_And_With_Trade_Count_More_Than_Ten_Thousand()
        {
            //Arrange
            //Act
            var tradeCode1 = CodeBuilder.FormatTradeCode(20000, 1);
            var tradeCode01 = CodeBuilder.FormatTradeCode(20000, 10);
            var tradeCode001 = CodeBuilder.FormatTradeCode(20000, 100);
            var tradeCode0001 = CodeBuilder.FormatTradeCode(20000, 1000);

            //Assert
            Assert.Equal("AG0001-20000", tradeCode1);
            Assert.Equal("AG0010-20000", tradeCode01);
            Assert.Equal("AG0100-20000", tradeCode001);
            Assert.Equal("AG1000-20000", tradeCode0001);
        }

        [Fact]
        public void FormatTradeCode_With_Client_Company_ID_More_Than_Ten_Thousand_And_With_Trade_Count_More_Than_Ten_Thousand()
        {
            //Arrange
            //Act
            var tradeCode = CodeBuilder.FormatTradeCode(20000, 20000);

            //Assert
            Assert.Equal("AG20000-20000", tradeCode);
        }
    }
}
