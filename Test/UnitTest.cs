using Mastermind;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class UnitTests
    {
        [Fact]
        public void MonkeyTest()
        {
            Assert.Equal(1, 1);    
        }
        
        // Argument Types

        [Fact]
        public void Initializes()
        {
            Assert.Equal(ArgumentTypes.ColourRange.Length, 6);
        }

        [Theory]
        [InlineData(0, "Red")]
        [InlineData(1, "Blue")]
        [InlineData(2, "Green")]
        [InlineData(3, "Orange")]
        [InlineData(4, "Purple")]
        [InlineData(5, "Yellow")]
        public void HasSixColoursAtIndex(int i, string colour)
        {
            Assert.Equal(ArgumentTypes.ColourRange[i], colour);
        }
    }

    public class SelectColours : ArgumentTypes
    {
        private readonly ITestOutputHelper output;

        public void MyTestClass(ITestOutputHelper output)
        {
            this.output = output;
        }
        
        [Theory]
        [InlineData(1, 2, 3, 4)]
        [InlineData(3, 4, 5, 6)]
        [InlineData(1, 1, 1, 2)]
        [InlineData(3, 3, 3, 3)]
        public void PickColours(int a, int b, int c, int d)
        {
            Assert.Equal(ColourPick,  new int[] {a, b, c, d});
        }
    }
    
}