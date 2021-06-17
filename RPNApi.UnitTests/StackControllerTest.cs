using System.Collections.Concurrent;
using System.Collections.Generic;
using Easy.Logger.Interfaces;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using RPNApi.Server.Repository;

namespace RPNApi.UnitTests
{
    public class StackControllerTest
    {
        public InMemoryStackRepository _stackRepository;

        [SetUp]
        public void Setup()
        {
            _stackRepository = new InMemoryStackRepository();
        }

        [Test]
        public void GetStacks_ShouldReturnEmpty()
        {
            Assert.IsEmpty(_stackRepository.GetStacks());
        }

        [Test]
        public void InsertStack_ShouldReturnNotEmpty()
        {
            int stackId = _stackRepository.InsertStack(new ConcurrentStack<decimal>());

            Assert.IsNotEmpty(_stackRepository.GetStacks());
        }

        [Test]
        public void UpdateStack_ShouldReturnNotEmpty()
        {
            int newValue = 5;
            var stack = new ConcurrentStack<decimal>();
            int stackId = _stackRepository.InsertStack(stack);
            stack.Push(newValue);
            _stackRepository.UpdateStack(stackId, stack);
            stack = _stackRepository.GetStackById(stackId);
            stack.TryPop(out var result);

            Assert.AreEqual(newValue, result);
        }

        [Test]
        public void DeleteStack_ShouldReturnEmpty()
        {
            int stackId = _stackRepository.InsertStack(new ConcurrentStack<decimal>());
            _stackRepository.DeleteStack(stackId + 1);

            Assert.IsNotEmpty(_stackRepository.GetStacks());

            _stackRepository.DeleteStack(stackId);

            Assert.IsEmpty(_stackRepository.GetStacks());
        }
    }
}
