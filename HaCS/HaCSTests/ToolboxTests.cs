using NUnit.Framework;
using HaCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCS.Tests
{
    [TestFixture()]
    public class ToolboxTests
    {
        [Test()]
        public void getTypeIntTest()
        { 
            Assert.AreEqual(Toolbox.getType(HaCSParser.INT), new HaCS.Types.tINT());
        }
        [Test()]
        public void getTypeIntTypeTest()
        {
            Assert.AreEqual(Toolbox.getType(HaCSParser.INT_Type), new HaCS.Types.tINT());
        }
        [Test()]
        public void getTypeFloatTest()
        {
            Assert.AreEqual(Toolbox.getType(HaCSParser.FLOAT), new HaCS.Types.tFLOAT());
        }
        [Test()]
        public void getTypeFloatTypeTest()
        {
            Assert.AreEqual(Toolbox.getType(HaCSParser.FLOAT_Type), new HaCS.Types.tFLOAT());
        }
        [Test()]
        public void getTypeCharTest()
        {
            Assert.AreEqual(Toolbox.getType(HaCSParser.CHAR), new HaCS.Types.tCHAR());
        }
        [Test()]
        public void getTypeCharTypeTest()
        {
            Assert.AreEqual(Toolbox.getType(HaCSParser.CHAR_Type), new HaCS.Types.tCHAR());
        }
        [Test()]
        public void getTypeBoolTest()
        {
            Assert.AreEqual(Toolbox.getType(HaCSParser.BOOL), new HaCS.Types.tBOOL());
        }
        [Test()]
        public void getTypeBoolTypeTest()
        {
            Assert.AreEqual(Toolbox.getType(HaCSParser.BOOL_Type), new HaCS.Types.tBOOL());
        }
        [Test()]
        public void getTypeInvalidTest()
        {
            Assert.AreEqual(Toolbox.getType(9000), new HaCS.Types.tINVALID());
        }
    }
}