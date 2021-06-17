using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Easy.Logger.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPNApi.Server.Business;
using RPNApi.Server.Models.Operand;
using RPNApi.Server.Repository.Stack;

namespace RPNApi.Controllers
{
    [ApiController]
    [Route("rpn/op")]
    public class OperandController : ControllerBase
    {
        private readonly IEasyLogger<OperandController> _logger;
        private RpnDomain _rpnDomain;

        public OperandController(IEasyLogger<OperandController> logger)
        {
            _logger = logger;
            _rpnDomain = RpnDomain.Instance;
        }


        /// <summary>
        /// Get operands values
        /// </summary>
        /// <returns>A list of operands values</returns>
        /// <response code="200">Returns a list operands values</response>
        [HttpGet(Name = nameof(GetOperands))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<string> GetOperands()
        {
            _logger.Debug("Get operands");
            List<string> operandValues = new List<string>();

            try
            {
                var _operandByStringRepresentation = _rpnDomain.GetOrCreateOperandByStringRepresentation();
                operandValues = _operandByStringRepresentation.Keys.ToList();
            }
            catch (Exception e)
            {
                _logger.Error("Failed to get operands", e);
            }
            return operandValues;
        }

        /// <summary>
        /// Apply operand
        /// </summary>
        /// <response code="200">Returns a list operands values</response>
        [HttpPost("{op}/stack/{stackId}", Name = nameof(ApplyOperand))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public void ApplyOperand([FromRoute] string op, [FromRoute] int stackId)
        {
            _logger.Debug("Apply operand");
            

            try
            {
                IOperand operand = GetOperand(op);

                IStackRepository stackRepository = _rpnDomain.GetOrCreateInMemoryStackRepository();

                ConcurrentStack<decimal> stack = stackRepository.GetStackById(stackId);

                decimal[] values = new decimal[2];
                stack.TryPopRange(values);

                decimal result = operand.GetResult(values[1], values[0]);
                stack.Push(result);
            }
            catch (Exception e)
            {
                _logger.Error("Failed to apply operand", e);
            }
        }

        private IOperand GetOperand(string op)
        {
            var _operandByStringRepresentation = _rpnDomain.GetOrCreateOperandByStringRepresentation();

            if (_operandByStringRepresentation.ContainsKey(op) == false)
                throw new InvalidDataException();

            return _operandByStringRepresentation[op];
        }
    }
}
