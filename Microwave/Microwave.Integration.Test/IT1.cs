using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwave.Integration.Test
{
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
            private Door door;
            private PowerTube tub;
            private ITimer time;
            private IOutput output;

            [SetUp]
            public void Setup()
            {
                output = Substitute.For<IOutput>();


                lit = new Light(output);

                door = new Door();
                
                
            }

           
            [Test]
            public void OpenDoorLightReceiver()
            {

                lit.TurnOff();

                
                door.Open();

                
                output.Received().OutputLine(Arg.Is<string>(str => str.Contains("on")));
                
  
            }





        }
    }
}
