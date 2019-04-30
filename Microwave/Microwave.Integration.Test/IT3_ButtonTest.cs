using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;

namespace Microwave.Integration.Test
{
    
    using MicrowaveOvenClasses.Boundary;
    using MicrowaveOvenClasses.Interfaces;
    using NSubstitute;
    using NUnit.Framework;

    namespace Microwave.Test.Unit
    {
        [TestFixture]
        public class ButtonTest
        {

            private UserInterface tUI;
            private IOutput output;
            private IButton inputButton;
            private IDoor inputDoor;
            private IDisplay inputDisplay;
            private ILight inputLight;
            private ICookController tCookController;
            private IPowerTube inputPower;
            private ITimer inputTimer;
            private Button tButton;


            [SetUp]
            public void Setup()
            {
                output = Substitute.For<IOutput>();
                inputButton = Substitute.For<IButton>();
                inputDoor = Substitute.For<IDoor>();
                inputDisplay = new Display(output);
                inputLight = Substitute.For<ILight>();
                inputPower = new PowerTube(output);
                inputTimer = new Timer();
                tButton = new Button();

                tCookController = new CookController(inputTimer, inputDisplay, inputPower, tUI);
                tUI = new UserInterface(inputButton, inputButton, inputButton, inputDoor, inputDisplay, inputLight, tCookController);
            }

            [Test]
            public void PowerPressed_Started()
            {
                tUI.OnPowerPressed(null, null);

                output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("50 w")));
            }

            [Test]
            public void PowerPressedMultipleTimes_Started()
            {
                for (int i = 0; i < 14; i++)
                    tUI.OnPowerPressed(null, null);

                output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("700 w")));
            }

            [Test]
            public void PowerPressedTooManyTimes_Started()
            {
                for (int i = 0; i < 15; i++)
                    tUI.OnPowerPressed(null, null);

                output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("50 w")));
            }

            [Test]
            public void TimePressed_Started()
            {
                tUI.OnPowerPressed(null, null);
                tUI.OnTimePressed(null, null);

                output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("01:00")));
            }

            [Test]
            public void TimePressedMultipleTimes_Started()
            {
                tUI.OnPowerPressed(null, null);
                for (int i = 0; i < 10; i++)
                    tUI.OnTimePressed(null, null);

                output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("10:00")));
            }

            [Test]
            public void StartCancel_Power()
            {
                tUI.OnPowerPressed(null, null);
                tUI.OnTimePressed(null, null);
                tUI.OnStartCancelPressed(null, null);

                output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("powertube works with")));
            }

            [Test]
            public void StartCancel_Timer()
            {
                tUI.OnPowerPressed(null, null);
                tUI.OnTimePressed(null, null);
                tUI.OnStartCancelPressed(null, null);

                ManualResetEvent pause = new ManualResetEvent(false);

                pause.WaitOne(2100);

                output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("00:58")));

            }
        }
    }
}