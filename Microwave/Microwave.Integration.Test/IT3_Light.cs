using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
  
    using MicrowaveOvenClasses.Interfaces;
    using NSubstitute;
    using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class IT3_Light
    {
        private IDoor door;
        private IUserInterface ui;
        private ITimer timer;
        private IPowerTube power;
        private IDisplay display;
        private ILight light;
        private IButton powerbtn;
        private IButton timerbtn;
        private IButton startCbtn;
        private ICookController cc;
        private IOutput output;



        [SetUp]
        public void Setup()
        {
            startCbtn = new Button();
            timerbtn = new Button();
            powerbtn = new Button();
            door = new Door();

            ui = new UserInterface(powerbtn, timerbtn, startCbtn, door, display, light, cc);
            cc = Substitute.For<ICookController>();

            light = new Light(output);
            timer = Substitute.For<ITimer>();
            display = Substitute.For<IDisplay>();
            power = Substitute.For<IPowerTube>();
            output = Substitute.For<IOutput>();
        }


        [Test]
        public void pressOpenDoor_IsLightOn()
        {
            
            door.Open();   
            output.Received(1).OutputLine(Arg.Is<string>(str => str.ToLower().Contains("light is turned on")));

        }

        [Test]
        public void pressCloseDoor_IsLightOFF()
        {

            door.Open();
            door.Close();
            output.Received(1).OutputLine(Arg.Is<string>(str => str.ToLower().Contains("light is turned off")));

        }
        [Test]
        public void pressStartCancel_IsLightOn()
        {

            powerbtn.Press();
            timerbtn.Press();
            startCbtn.Press();

            output.Received(1).OutputLine(Arg.Is<string>(str => str.ToLower().Contains("light is turned on")));


        }

        [Test]
        public void pressStartCancel_IsLightOFF()
        {
            ManualResetEvent pause = new ManualResetEvent(false);
            powerbtn.Press();
            timerbtn.Press();
            startCbtn.Press();

            pause.WaitOne(1000);
            startCbtn.Press();

            output.Received(1).OutputLine(Arg.Is<string>(str => str.ToLower().Contains("light is turned off")));


        }



    }
}