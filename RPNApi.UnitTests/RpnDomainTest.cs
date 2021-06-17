using NuGet.Frameworks;
using NUnit.Framework;
using RPNApi.Server.Business;
using RPNApi.Server.Models.Operand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPNApi.UnitTests
{
    public class RpnDomainTest
    {
        public RpnDomain _rpnDomain;

        [SetUp]
        public void Setup()
        {
            _rpnDomain = RpnDomain.Instance;
        }


        [Test]
        public void RpnDomain_ShouldHaveSupportedOperands()
        {
            var operandsByStringRepresentation = _rpnDomain.GetOrCreateOperandByStringRepresentation();
            List<string> operandsString = operandsByStringRepresentation.Keys.ToList();
            string[] supportedStrings = new string[] { "+", "-", "/", "*" };

            foreach(string supportedString in supportedStrings)
            {
                Assert.IsTrue(operandsString.Contains(supportedString));
            }
        }

        [Test]
        public void RpnDomain_CheckAdditionOperand()
        {
            var operandsByStringRepresentation = _rpnDomain.GetOrCreateOperandByStringRepresentation();
            IOperand operand = operandsByStringRepresentation["+"];

            Assert.AreEqual(5m + 2m, operand.GetResult(5, 2));
        }


        [Test]
        public void RpnDomain_CheckSoustractionOperand()
        {
            var operandsByStringRepresentation = _rpnDomain.GetOrCreateOperandByStringRepresentation();
            IOperand operand = operandsByStringRepresentation["-"];

            Assert.AreEqual(5m - 2m, operand.GetResult(5, 2));
        }



        [Test]
        public void RpnDomain_CheckDivisionOperand()
        {
            var operandsByStringRepresentation = _rpnDomain.GetOrCreateOperandByStringRepresentation();
            IOperand operand = operandsByStringRepresentation["/"];

            Assert.AreEqual(5m / 2m, operand.GetResult(5, 2));
        }



        [Test]
        public void RpnDomain_CheckMultiplicationOperand()
        {
            var operandsByStringRepresentation = _rpnDomain.GetOrCreateOperandByStringRepresentation();
            IOperand operand = operandsByStringRepresentation["*"];

            Assert.AreEqual(5m * 2m, operand.GetResult(5, 2));
        }
    }
}
