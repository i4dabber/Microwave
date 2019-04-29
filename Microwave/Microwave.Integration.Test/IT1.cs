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
            tim = new MicrowaveOvenClasses.Boundary.Timer();
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
        public void isTimeRemaining_theexpected()
        {
            cook.StartCooking(90, 1);
            Thread.Sleep(1500);

            output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("powertube turned off")));

        }

        [TestCase]
        public void isCooking_WhileOn()
        {
            cook.StartCooking(90, 5);
            Assert.Throws<System.ApplicationException>(() => cook.StartCooking(90, 5));

        }


        











    }
}

