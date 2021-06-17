using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easy.Logger.Interfaces;
using RPNApi.Server.Models.Operand;
using RPNApi.Server.Repository;
using RPNApi.Server.Repository.Stack;

namespace RPNApi.Server.Business
{
    public sealed class RpnDomain
    {
        private static readonly RpnDomain instance = new RpnDomain();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static RpnDomain()
        {
        }

        private RpnDomain()
        {
        }

        public static RpnDomain Instance => instance;


        private IStackRepository _stackRepository;
        private Dictionary<string, IOperand> _operandByStringRepresentation;

        public IStackRepository GetOrCreateInMemoryStackRepository()
        {
            return _stackRepository ??= new InMemoryStackRepository();
        }

        public Dictionary<string, IOperand> GetOrCreateOperandByStringRepresentation()
        {
            return _operandByStringRepresentation ??= GetOperandByStringRepresentation();
        }

        private Dictionary<string, IOperand> GetOperandByStringRepresentation()
        {
            Dictionary<string, IOperand> operands = new Dictionary<string, IOperand>();

            AddOperand(operands, new AdditionOperand());
            AddOperand(operands, new SoustractionOperand());
            AddOperand(operands, new DivisionOperand());
            AddOperand(operands, new MultiplicationOperand());

            return operands;
        }

        private void AddOperand(Dictionary<string, IOperand> operands, IOperand operand)
        {
            operands.Add(operand.GetSymbol(), operand);
        }
    }
}
