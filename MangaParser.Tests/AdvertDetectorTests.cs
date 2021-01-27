using System;
using System.IO;
using MangaPacker.Images;
using Xunit;
using Xunit.Abstractions;

namespace MangaParser.Tests
{
    public class AdvertDetectorTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        private readonly string _testDirectory = Path.Join(Directory.GetCurrentDirectory(), "../../../Test Data/");
        private readonly AdvertDetector _advertDetector;
        
        public AdvertDetectorTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _advertDetector = new AdvertDetector(Path.Join(_testDirectory, "adverts"));
        }

        [Theory]
        [InlineData("is advert.jpg", true)]
        [InlineData("not advert.jpg", false)]
        public void IsAdvertTest(string inputFile, bool expected)
        {
            var filePath = Path.Join(_testDirectory, inputFile);
            Assert.Equal(expected, _advertDetector.IsAdvert(filePath));
        }
    }
}