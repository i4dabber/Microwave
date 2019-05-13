//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using MicrowaveOvenClasses.Boundary;
//using MicrowaveOvenClasses.Interfaces;
//using NSubstitute;
//using NUnit.Framework;
//using MicrowaveOvenClasses.Controllers;
//using System.Threading;

//namespace Microwave.Integration.Test
//{


//    [TestFixture]
//    public class IT1_UI
//    {

//        private IDoor door;
//        private IUserInterface ui;
//        private ITimer timer;
//        private IPowerTube power;
//        private IDisplay display;
//        private ILight light;
//        private IButton powerbtn;
//        private IButton timerbtn;
//        private IButton startCbtn;
//        private ICookController cc;
//        private IOutput output;



//        [SetUp]
//        public void Setup()
//        {
//            powerbtn = new Button();
//            timerbtn = new Button();
//            startCbtn = new Button();
//            door = new Door();
            
//            ui = new UserInterface(powerbtn, timerbtn, startCbtn, door, display, light, cc);



//        }


//        [Test] //Tester for om Output klassen modtager data fra this og de andre udnyttede klasser
//        public void isCooking_StartingprintingtoOutput()
//        {
//            //StartCooking accesser funktionerne TurnOn og Timer Start
//            cook.StartCooking(50, 5);
//            //ToLower formater strengen til lowcase characters
//            output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("powertube works with 7,1 %")));

//        }
//        [Test] //Tester for om Output klassen modtager data fra this og de andre udnyttede klasser
//        /*public void isCooking_StartingprintingtoOutput()
//        {
//            //StartCooking accesser funktionerne TurnOn og Timer Start
//            cook.StartCooking(50, 5);
//            //ToLower formater strengen til lowcase characters
//            output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("powertube works with 7,1 %")));

//        }*/

//        [TestCase] //Tester for om tiden vi modtager fra Timer klasses er det forventede vi får i this
//        public void isTimeRemaining_theexpected()
//        {
//            cook.StartCooking(90, 1);
//            ManualResetEvent pause = new ManualResetEvent(false);

//            pause.WaitOne(1500);

//            output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("powertube turned off")));

//        }


//        [Test] //Tester for om tiden er udeløbet, dvs efter 10 sek bør man den dø
//        public void isCookingthen_StoppingTime()
//        {
//            cook.StartCooking(90,1);

//            ManualResetEvent pause = new ManualResetEvent(false);

//            pause.WaitOne(1500);
            

//            cook.Stop();

//            output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("powertube turned off")));

//        }

//        [Test] //Tester For om Cooking overhovedet gider at cooke igennem Timer og Powertube
//        public void isCooking_WhileOn()
//        {
//            cook.StartCooking(90, 5);
//            //Assert.That(() => cook.StartCooking(90, 5), Throws.TypeOf<ApplicationException>());

//            Assert.Throws<System.ApplicationException>(() => cook.StartCooking(90, 5));

//        }
        
//        [Test] //Tester for om Stop() funktionen fungerer uden delays.  
//        public void StopCookingWhilecookingOn()
//        {
            
//            cook.StartCooking(90, 1);
//            cook.Stop();

//            output.Received().OutputLine(Arg.Is<string>(str => str.ToLower().Contains("powertube turned off")));

//        }


//    }
//}






