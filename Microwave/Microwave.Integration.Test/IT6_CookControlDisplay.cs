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
    public class IT6_CookControlDisplay
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
            timer = new MicrowaveOvenClasses.Boundary.Timer();
            display = new Display(output);
            power = new PowerTube(output);

            cc = new CookController(timer, display, power);
            ui = new UserInterface(powerbtn, timerbtn, startCbtn, door, display, light, cc);
        }

        [Test]
        public void showTime_AfterTicks()
        {
            powerbtn.Press();
            timerbtn.Press();
            startCbtn.Press();

            ManualResetEvent pause = new ManualResetEvent(false);
            
            timer.TimerTick += (sender, args) => pause.Set();

            pause.WaitOne(1100);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("00:59")));
        }

        

    }
}
