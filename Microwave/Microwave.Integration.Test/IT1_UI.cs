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
    public class IT1_UI
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
        



        [SetUp]
        public void Setup()
        {
            startCbtn = new Button();
            timerbtn = new Button();
            powerbtn = new Button();
            door = new Door();

           
            light = Substitute.For<ILight>();
            timer = Substitute.For<ITimer>();
            display = Substitute.For<IDisplay>();
            power = Substitute.For<IPowerTube>();

            cc = Substitute.For<ICookController>();
            ui = new UserInterface(powerbtn, timerbtn, startCbtn, door, display, light, cc);
        }

        [Test]
        public void openDoor_LightTurnedOn()
        {
            door.Open();
            light.Received().TurnOn();
        }

        [Test]
        public void closeDoor_LightTurnedOff()
        {
            door.Open();
            door.Close();
            light.Received().TurnOff();
        }

        [Test]
        public void powerButton_DisplayPower()
        {
            powerbtn.Press();
            display.Received().ShowPower(50);
        }

        [Test]
        public void timeButton_DisplayTime()
        {
            powerbtn.Press();
            timerbtn.Press();
            display.Received().ShowTime(1,0);
        }

        [Test]
        public void startButton_StartCooking()
        {
            powerbtn.Press();
            timerbtn.Press();
            startCbtn.Press();
            cc.Received().StartCooking(50, 60);
        }
    }
}






