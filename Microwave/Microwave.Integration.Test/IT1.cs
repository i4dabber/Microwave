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

namespace Microwave.Integration.Test
{


    [TestFixture]
    public class CookControllerTest
    {

        //Fakers
        private ITimer tim;
        private IPowerTube tub;
        private IDisplay disp;

        private ICookController cook;


        private IOutput output;
        private IUserInterface ui;


        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();
            tim = new Timer();
            tub = new PowerTube(output);
            disp = new Display(output);
            ui = Substitute.For<IUserInterface>();
            cook = new CookController(tim, disp, tub, ui);



        }


        [TestCase]
        public void isCooking_StartingprintingtoOutput()
        {
            cook.StartCooking(50, 5);
            //ToLower formater strengen til lowcase characters
            output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("powertube works with 7,1 %")));

        }

        [TestCase]
        public void isCooking_WhileOn()
        {
            cook.StartCooking(90, 5);
            Assert.Throws<System.ApplicationException>(() => cook.StartCooking(90, 5));

        }

        [TestCase]
        public void isPowertube_Off()
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => tub.TurnOff());
        }



        [TestCase]
        public void isCookingbeenStopped_toOutput()
        {
            cook.Stop();
            
            output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("powertube turned off")));
            tub.TurnOff();
        }











    }
}

