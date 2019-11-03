using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests
{
    public static class Assert
    {
        public static void AreNotEqual(object expected, object actual)
        {
            // If either are null, we return.  Either one is null and the other isn't,
            // so what we are saying is true, or they are both null, in which case we count them as being different.
            if (expected == null) { return; }
            if (actual == null) { return; }

            if (expected == actual) { throw new Exception("Assert failed.  " + expected + " equals " + actual + "."); }
        }

        public static void AreEqual(string expected, string actual)
        {
            if (expected != actual) { throw new Exception("Assert failed.  Expected " + expected + " but got " + actual + "."); }
        }

        public static void AreEqual(int expected, int actual)
        {
            if (expected != actual) { throw new Exception("Assert failed.  Expected " + expected + " but got " + actual + "."); }
        }
    }
}
