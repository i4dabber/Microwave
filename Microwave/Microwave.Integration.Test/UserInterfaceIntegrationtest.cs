﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public class OutputTest
        {
            private UserInterface tUI;
            private IOutput output;
            private IButton inputButton;
            private IDoor inputDoor;
            private IDisplay inputDisplay;
            private ILight inputLight;
            private ICookController inputCookController;
            private IPowerTube inputPower;
            private ITimer inputTimer;

            [SetUp]
            public void Setup()
            {
                output = Substitute.For<IOutput>();
                inputButton = Substitute.For<IButton>();
                inputDoor = Substitute.For<IDoor>();
                inputDisplay = Substitute.For<IDisplay>();
                inputLight = Substitute.For<ILight>();
                inputCookController = Substitute.For<ICookController>();
                inputPower = Substitute.For<IPowerTube>();
                inputTimer = Substitute.For<ITimer>();

                tUI = new UserInterface(inputButton, inputButton, inputButton, inputDoor, inputDisplay, inputLight, inputCookController);
            }

           
            [Test]
            public void OpenDoor_DoorIsOpen_LightisOn()
            {
                //Act
                tUI.OnDoorOpened(null, null);

                inputLight.Received(1);
                //output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("on")));

            }

            [Test]
            public void CloseDoor_DoorisClosed_LightisOff()
            {
                tUI.OnDoorClosed(null, null);

                inputLight.Received(2);
            }

            [Test]
            public void PowerPressed_Started()
            {
                tUI.OnPowerPressed(null, null);

                inputPower.Received(50);

                //output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("50 W")));
            }

            [Test]
            public void PowerPressedMultipleTimes_Started()
            {
                for (int i = 0; i < 14; i++)
                    tUI.OnPowerPressed(null, null);

                inputPower.Received(700);
            }

            [Test]
            public void PowerPressedTooManyTimes_Started()
            {
                for (int i = 0; i < 15; i++)
                    tUI.OnPowerPressed(null, null);

                inputPower.Received(50);
            }

            [Test]
            public void TimePressed_Started()
            {
                tUI.OnTimePressed(null, null);

                inputTimer.Received(60);
            }

            [Test]
            public void TimePressedMultipleTimes_Started()
            {
                for (int i = 0; i < 10; i++)
                    tUI.OnTimePressed(null, null);

                inputTimer.Received(600);
            }




        }
    }
}
