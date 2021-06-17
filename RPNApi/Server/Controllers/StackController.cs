using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easy.Logger.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPNApi.Controllers;
using RPNApi.Server.Business;
using RPNApi.Server.Repository.Stack;

namespace RPNApi.Server.Controllers
{
    [ApiController]
    [Route("rpn/stack")]
    public class StackController
    {
        private readonly IEasyLogger<OperandController> _logger;
        private RpnDomain _rpnDomain;

        public StackController(IEasyLogger<OperandController> logger)
        {
            _logger = logger;
            _rpnDomain = RpnDomain.Instance;
        }

        [HttpPost(Name = nameof(CreateStack))]
        public int CreateStack()
        {
            _logger.Debug("Create stack");
            int stackId = -1;

            try
            {
                IStackRepository stackRepository = _rpnDomain.GetOrCreateInMemoryStackRepository();
                stackId = stackRepository.InsertStack(new ConcurrentStack<decimal>());
            }
            catch (Exception e)
            {
                _logger.Error("Failed to created stack", e);
            }
            return stackId;
        }

        [HttpGet(Name = nameof(GetStacks))]
        public IEnumerable GetStacks()
        {
            _logger.Debug("Get stacks");
            IEnumerable stacks = new List<Stack<decimal>>();

            try
            {
                IStackRepository stackRepository = _rpnDomain.GetOrCreateInMemoryStackRepository();
                stacks = stackRepository.GetStacks();
            }
            catch (Exception e)
            {
                _logger.Error("Failed to get stacks", e);
            }
            return stacks;
        }

        [HttpGet("{stackId}", Name = nameof(DeleteStack))]
        public void DeleteStack([FromRoute] int stackId)
        {
            _logger.Debug("Delete stack");

            try
            {
                IStackRepository stackRepository = _rpnDomain.GetOrCreateInMemoryStackRepository();
                stackRepository.DeleteStack(stackId);
            }
            catch (Exception e)
            {
                _logger.Error("Failed to delete stack", e);
            }
        }

        [HttpPost("{stackId}", Name = nameof(GetStacks))]
        public void UpdateStack([FromRoute] int stackId, [FromQuery] int newValue)
        {
            _logger.Debug("Update stack");

            try
            {
                IStackRepository stackRepository = _rpnDomain.GetOrCreateInMemoryStackRepository();
                ConcurrentStack<decimal> stack = stackRepository.GetStackById(stackId);
                stack.Push(newValue);
                stackRepository.UpdateStack(stackId, stack);
            }
            catch (Exception e)
            {
                _logger.Error("Failed to update stack", e);
            }
        }

        [HttpGet("{stackId}", Name = nameof(GetStacks))]
        public ConcurrentStack<decimal> GetStacks([FromRoute] int stackId)
        {
            _logger.Debug("Get stack");
            ConcurrentStack<decimal> stack = new ConcurrentStack<decimal>();

            try
            {
                IStackRepository stackRepository = _rpnDomain.GetOrCreateInMemoryStackRepository();
                stack = stackRepository.GetStackById(stackId);
            }
            catch (Exception e)
            {
                _logger.Error("Failed to get stack", e);
            }

            return stack;
        }
    }
}
