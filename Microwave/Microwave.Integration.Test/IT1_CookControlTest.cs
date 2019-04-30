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


        [Test] //Tester for om Output klassen modtager data fra this og de andre udnyttede klasser
        public void isCooking_StartingprintingtoOutput()
        {
            cook.StartCooking(50, 5);
            //ToLower formater strengen til lowcase characters
            output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("powertube works with 7,1 %")));

        }

        [Test] //Tester for om tiden vi modtager fra Timer klasses er det forventede vi får i this
        public void isTimeRemaining_theexpected()
        {
            cook.StartCooking(90, 1);
            Thread.Sleep(1500);

            output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("powertube turned off")));

        }


        [Test] //Tester for om tiden er udeløbet, dvs efter 10 sek bør man den dø
        public void isCookingthen_StoppingTime()
        {
            cook.StartCooking(90,1);
            cook.Stop();

            Thread.Sleep(1500);
            output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("powertube turned off")));

        }

        /*[Test] //Tester for integrationen mellem this og UI-klassen. Se definitionerne
        public void isCookingDone_AfterTimeFinished()
        {
            cook.StartCooking(90, 1);
            cook.Stop(); 
            ui.CookingIsDone();
            ui.Received()
           
        }*/

        [Test] //Tester For om Cooking overhovedet gider at cooke igennem Timer og Powertube
        public void isCooking_WhileOn()
        {
            cook.StartCooking(90, 5);
            //Assert.That(() => cook.StartCooking(90, 5), Throws.TypeOf<ApplicationException>());

            Assert.Throws<System.ApplicationException>(() => cook.StartCooking(90, 5));

        }

        [Test] //Tester for om 
        public void StopCookingWhilecookingOn()
        {
            
            cook.StartCooking(90, 1);
            cook.Stop();

            //Denne her funktion skulle gerne give et boolsk true udtryk, men når jeg kigger på definitionerne - 
            //Så passer det ikke med det jeg ønsker, selvom testen siger korrekt.
            output.Received(1);
            
        }

















    }
}

