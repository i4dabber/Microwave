using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using MicrowaveOvenClasses.Controllers;
using System.Threading;

namespace Microwave.Integration.Test
{

    [TestFixture]
    public class IT5_Display
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

            output = Substitute.For<IOutput>();

            light = Substitute.For<ILight>();
            timer = Substitute.For<ITimer>();
            display = Substitute.For<IDisplay>();
            power = new PowerTube(output);

            cc = new CookController(timer, display, power);
            ui = new UserInterface(powerbtn, timerbtn, startCbtn, door, display, light, cc);
        }

        [Test]
        public void TurnOn_PowertubeWorking()
        {
            powerbtn.Press();
            timerbtn.Press();
            startCbtn.Press();
            output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("powertube works")));
        
        }

        [Test]
        public void Powertube_TurnOffFromDoor()
        {
            powerbtn.Press();
            timerbtn.Press();
            startCbtn.Press();

            door.Open();
            output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("powertube turned off")));
        }
    }
  }
