using FluentAssertions;
using Serilog.Sinks.TestCorrelator;

namespace FaimLibTests
{
    [TestClass]
    public class TestFaimRec
    {
        //ILogger _logger;
        [TestInitialize]
        public void Setup()
        {
            Log.Logger = new LoggerConfiguration().WriteTo.TestCorrelator().CreateLogger();
            //var lc = new LoggerConfiguration().WriteTo.Console();
            //_logger = lc.CreateLogger();
        }
        [TestMethod]
        public void TestLoging()
        {
            using (TestCorrelator.CreateContext())
            {
                Log.Information("My log message!");

                TestCorrelator.GetLogEventsFromCurrentContext()
                    .Should().ContainSingle()
                    .Which.MessageTemplate.Text
                    .Should().Be("My log message!");
            }
        }

        [TestMethod]
        public void TestFaimRecFromRawParseTags()
        {
            using (TestCorrelator.CreateContext())
            {


                TestCorrelator.GetLogEventsFromCurrentContext()
                    .Should().ContainSingle(x => x.MessageTemplate.Text.StartsWith("FAIM header: XFT811")); 
            }
        }

        [TestMethod]
        public void TestFaimRecFromRawBadHeader()
        {
            using (TestCorrelator.CreateContext())
            {
  
                TestCorrelator.GetLogEventsFromCurrentContext()
                    .Should().ContainSingle(x => x.MessageTemplate.Text.StartsWith("No header found."));
            }
        }
    }
}