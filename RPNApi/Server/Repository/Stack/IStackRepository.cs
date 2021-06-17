using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPNApi.Server.Repository.Stack
{
    public interface IStackRepository
    {
        IEnumerable GetStacks();
        ConcurrentStack<decimal> GetStackById(int stackId);
        int InsertStack(ConcurrentStack<decimal> stack);
        void DeleteStack(int stackId);
        void UpdateStack(int stackId, ConcurrentStack<decimal> stack);
        void Save();
    }
}
