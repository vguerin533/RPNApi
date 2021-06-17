using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easy.Logger.Interfaces;
using RPNApi.Server.Repository.Stack;

namespace RPNApi.Server.Repository
{
    public class InMemoryStackRepository : IStackRepository
    {
        private readonly ConcurrentDictionary<int, ConcurrentStack<decimal>> _stacksByStackId;
        private readonly object _stackRepositoryLock = new object();
        private int _lastKey;
            
        public InMemoryStackRepository()
        {
            _stacksByStackId = new ConcurrentDictionary<int, ConcurrentStack<decimal>>();
            _lastKey = 0;
        }

        public IEnumerable GetStacks()
        {
            return _stacksByStackId.Values;
        }

        public ConcurrentStack<decimal> GetStackById(int stackId)
        {
            return _stacksByStackId[stackId];
        }

        public int InsertStack(ConcurrentStack<decimal> stack)
        {
            int stackId;
            lock (_stackRepositoryLock)
            {
                stackId = _lastKey;
                _lastKey += 1;
            }

            _stacksByStackId.TryAdd(stackId, stack);

            return stackId;
        }

        public void DeleteStack(int stackId)
        {
            _stacksByStackId.TryRemove(stackId, out ConcurrentStack<decimal> value);
        }

        public void UpdateStack(int stackId, ConcurrentStack<decimal> stack)
        {
            _stacksByStackId[stackId] = stack;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
