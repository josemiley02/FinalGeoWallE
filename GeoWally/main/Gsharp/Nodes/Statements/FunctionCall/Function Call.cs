using System.Collections.Generic;

namespace Gsharp
{

public class FunctionCallExpression : IExpression , IStatement 
{
    public string Name{ get ; private set ;}

    public WalleType ReturnType => functionImplementation.ReturnType ;
    private ExecutableFunction functionImplementation ;

    private List<IExpression> args;
        
    public FunctionCallExpression(string name, List<IExpression> args)
    {
        Name = name;
        this.args = args;
    }
    public void GetScope(Scope actual)
    {
        foreach (var item in args)
        {
            item.GetScope(actual);
        }
    }
    public WalleType CheckSemantics()
    {
        functionImplementation = CompilatorTools.SearchFunction(Name,args.Count);
        foreach (var expression in args)
        {
            expression.CheckSemantics();
            // chequear que el tipo del argumento coincida con el esperado por la funcion
        }
        return ReturnType;
    }
    public object Evaluate()
    {   
        var result = functionImplementation.Run(args);
        functionImplementation.ResetScope();
        return result ;           
    }
    public void Execute() => Evaluate();
    
    public bool ConvertToBool() => (double)Evaluate() != 0 ;
    public override string ToString()
    {
        string output = $"{Name}(" ;
        for (int i = 0 ; i < args.Count ; i++)
        {
            output += args[i].ToString();
            if(i != args.Count - 1){output += ",";}
        }
        return $"{output})";
    }
}
}