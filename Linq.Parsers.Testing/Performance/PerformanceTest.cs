using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers.Testing.Performance
{
    [TestClass]
    public class PerformanceTest : Test
    {
        #region MethodInvocation

        [TestMethod]
        public void MethodInvocation()
        {
            //var interfaceTimes = new List<double>();
            //var abstractTimes = new List<double>();
            //var virtualTimes = new List<double>();

            //for (int i = 0; i < 1000000; i++)
            //{
            //    var something = new InterfaceImplementation();
            //    var abstractSomething = new ConcreteAbstractClass();
            //    var virtualSomething = new ConcreteVirtualClass();

            //    interfaceTimes.Add(ExecuteInterface(something));
            //    abstractTimes.Add(ExecuteAbstractClass(abstractSomething));
            //    virtualTimes.Add(ExecuteVirtualClass(virtualSomething));
            //}

            //var interfaceAverage = interfaceTimes.Average();
            //var abstractAverage = abstractTimes.Average();
            //var virtualAverage = virtualTimes.Average();

            //var minAverage = new[] { interfaceAverage, abstractAverage, virtualAverage }.Min();
            //Assert.IsTrue(true);
        }

        #endregion //MethodInvocation

        #region Utilities

        private double ExecuteInterface(IInterface something)
        {
            var stopwatch = Stopwatch.StartNew();
            something.Execute();
            stopwatch.Stop();

            var ticks = stopwatch.ElapsedTicks;
            var nanoseconds = GetNanoseconds(ticks);

            return nanoseconds;
        }

        private double ExecuteAbstractClass(AbstractClass something)
        {
            var stopwatch = Stopwatch.StartNew();
            something.Execute();
            stopwatch.Stop();

            var ticks = stopwatch.ElapsedTicks;
            var nanoseconds = GetNanoseconds(ticks);

            return nanoseconds;
        }

        private double ExecuteVirtualClass(VirtualClass something)
        {
            var stopwatch = Stopwatch.StartNew();
            something.Execute();
            stopwatch.Stop();

            var ticks = stopwatch.ElapsedTicks;
            var nanoseconds = GetNanoseconds(ticks);

            return nanoseconds;
        }

        private static double GetNanoseconds(long ticks)
        {
            return 1000000000.0 * ((double)ticks / Stopwatch.Frequency);
        }

        #endregion //Utilities

        #region Test Classes

        private interface IInterface
        {
            void Execute();
        }

        private class InterfaceImplementation : IInterface
        {
            public void Execute()
            {
            }
        }

        private abstract class AbstractClass
        {
            public abstract void Execute();
        }

        private class ConcreteAbstractClass : AbstractClass
        {
            public override void Execute()
            {
            }
        }

        private class VirtualClass
        {
            public virtual void Execute()
            {
            }
        }

        private class ConcreteVirtualClass : VirtualClass
        {
            public override void Execute()
            {
            }
        }

        #endregion //Test Classes
    }
}
