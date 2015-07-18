using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers.Testing
{
    public class Test
    {
        protected void AssertThrows(Action action)
        {
            try
            {
                action();
                Assert.Fail("The action did not throw an exception.");
            }
            catch { }
        }
    }
}
