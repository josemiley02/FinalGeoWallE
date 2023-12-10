using System;
using System.Collections;
using System.Collections.Generic;

namespace Gsharp
{
    public sealed class PredefinedFunction : ExecutableFunction
    {
        public override int Count { get; }
        public override WalleType ReturnType { get; }
        private Func<List<IExpression>, object> function;
        // expectedTypes
        public PredefinedFunction(string name, int count, WalleType returnType, Func<List<IExpression>, object> function) : base(name)
        {
            Count = count;
            ReturnType = returnType;
            this.function = function;
        }
        public override object Run(List<IExpression> arguments) => function(arguments);
    }
}
    