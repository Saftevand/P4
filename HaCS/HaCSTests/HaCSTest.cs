using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HaCSTests
{
    [TestClass]
    public class HaCSTest
    {
        public TestContext TestContext { get; set; }
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
        "|DataDirectory|\\TestPath.csv", "TestPath#csv",
        DataAccessMethod.Sequential), DeploymentItem("TestPath.csv"),
        TestMethod]
        public void CompileTest()
        {
            string[] path = { TestContext.DataRow["Paths"].ToString() };
            int expected = Convert.ToInt32(TestContext.DataRow["Assertion"].ToString());
            Console.WriteLine(path[0]);
            int actual = HaCS.Program.Main(path);
            Assert.AreEqual<int>(expected, actual,path[0]);
        }

    }
}
