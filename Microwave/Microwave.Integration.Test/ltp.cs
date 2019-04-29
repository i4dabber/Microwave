using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class OutputTest
    {
        private Light lit;
        private Timer tim;
        private PowerTube tub;
        private IOutput output;

        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();


            lit = new Light(output);
            tim = new Timer();
            tub = new PowerTube(output);
        }

        [Test]
        public void SendToOutputTrue()
        {
            lit.TurnOn();
            
            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("on")));
        }

        



    }
}