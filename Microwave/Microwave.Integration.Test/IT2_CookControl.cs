using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Integration.Test
{

        [TestFixture]
        public class IT2_CookControl
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
                cc = new CookController(timer, display, power);

                light = Substitute.For<ILight>();
                timer = Substitute.For<ITimer>();
                display = Substitute.For<IDisplay>();
                power = Substitute.For<IPowerTube>();
                output = Substitute.For<IOutput>();
                
               
            }

           
            [Test]
            public void CookControl_TimerReceived()
            {
                powerbtn.Press();
                timerbtn.Press();
                startCbtn.Press();

                timer.Received().Start(60);
               

            }

            [Test]
            public void CookControl_StopTimer()
            {
                powerbtn.Press();
                timerbtn.Press();
                startCbtn.Press();

                door.Open();

                timer.Received().Stop();
            }


            
        }
    }

