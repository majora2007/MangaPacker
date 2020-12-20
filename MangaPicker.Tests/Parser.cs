using NUnit.Framework;
using Xunit;

namespace MangaPicker.Tests
{
    public class Parser
    {
        [Theory]
        [InlineData("Killing Bites Vol. 0001 Ch. 0001 - Galactica Scanlations (gb)", "1")]
        public void ParseVolume(string filename, string expectedOutput)
        {
            
        }
    }
}