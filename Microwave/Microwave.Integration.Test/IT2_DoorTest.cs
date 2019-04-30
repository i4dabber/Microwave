using System;
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
        public class DoorTest
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
            private Button tButton;

            [SetUp]
            public void Setup()
            {
                output = Substitute.For<IOutput>();
                inputButton = Substitute.For<IButton>();
                inputDoor = Substitute.For<IDoor>();
                inputDisplay = new Display(output);
                inputLight = Substitute.For<ILight>();
                inputCookController = Substitute.For<ICookController>();
                inputPower = Substitute.For<IPowerTube>();
                inputTimer = Substitute.For<ITimer>();
                tButton = new Button();

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
        }
    }
}
