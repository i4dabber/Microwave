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
            public void isCookingStartingprintingtoOutput ()
            {
            cook.StartCooking(90, 5);

            output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("powertube works with 90 %")));

        }





        }
    }

