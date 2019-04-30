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
            private Light lit;
            private Timer tim;
            private UserInterface tUI;
            private Door door;
            private PowerTube tub;
            private ITimer time;
            private IOutput output;
            private IButton inputButton;
            private IDoor inputDoor;
            private IDisplay inputDisplay;
            private ILight inputLight;
            private ICookController inputCookController;
            private ITimer inputTimer;
            private IPowerTube inputPower;

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

                //lit = new Light(output);

                //door = new Door();

                tUI = new UserInterface(inputButton, inputButton, inputButton, inputDoor, inputDisplay, inputLight,
                    inputCookController);

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
            public void PowerPressedInputValue_Started()
            {
                tUI.OnPowerPressed(80, null);

                inputPower.Received(80);



                //output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("shows")));
            }

            [Test]
            public void PowerDouble_pressed()
            {
                tUI.OnPowerPressed(null, null);
                tUI.OnPowerPressed(null, null);

                inputPower.Received(100);

            }

            [Test]
            public void TimePressed_Started()
            {
                tUI.OnTimePressed(null, null);

                inputTimer.Received(60);
            }

            [Test]
            public void TimeDouble_Pressed_Started()
            {
                tUI.OnTimePressed(null, null);
                tUI.OnTimePressed(null, null);
                inputTimer.Received(120);
            }

            [Test]
            public void StartCancelButton_Power()
            {
                tUI.OnStartCancelPressed(null, null);

                inputPower.Received(50);
                inputLight.Received(1);


            }

            public void StartCancelButton_Light()
            {
                tUI.OnStartCancelPressed(null, null);


                inputLight.Received(1);


            }
        }
    }
}
