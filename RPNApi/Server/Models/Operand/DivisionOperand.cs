using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPNApi.Server.Models.Operand
{
    public class DivisionOperand : IOperand
    {
        public string GetSymbol()
        {
            return "/";
        }

        public decimal GetResult(decimal left, decimal right)
        {
            return left / right;
        }
    }
}
