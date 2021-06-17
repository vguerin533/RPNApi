using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPNApi.Server.Models.Operand
{
    public interface IOperand
    {
        string GetSymbol();
        decimal GetResult(decimal left, decimal right);
    }
}
