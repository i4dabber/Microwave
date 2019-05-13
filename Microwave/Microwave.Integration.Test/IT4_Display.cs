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
    public class IT4_Display
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
            display = new Display(output);
            power = Substitute.For<IPowerTube>();
            
            cc = Substitute.For<ICookController>();
            ui = new UserInterface(powerbtn, timerbtn, startCbtn, door, display, light, cc);
        }

        [Test]
        public void PressPower_ShowPower()
        {

            powerbtn.Press();
            output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("50 w")));

        }
        [Test]
        public void PressPower_ShowTime()
        {

            powerbtn.Press();
            timerbtn.Press();
            output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("01:00")));

        }
        [Test]
        public void PressPower_isClear()
        {
            
            powerbtn.Press();
            timerbtn.Press();
            startCbtn.Press();

            door.Open();
            output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("display cleared")));


        }

    }
}



