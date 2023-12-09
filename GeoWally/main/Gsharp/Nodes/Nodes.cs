using System.Collections.Generic;

namespace Gsharp 
{
    public interface INode
    {
        void GetScope(Scope Actual);
        void CheckSemantics();
    }

    public sealed class SyntaxTree 
    {
        public List<IStatement> Program{get ; private set ;}
        public Scope Enviroment {get ; private set ;}

        public SyntaxTree(List<IStatement> program)
        {
            Program = program;
            Enviroment = new Scope();
        }
        public void CheckSemantics()
        {
            foreach (var statement in Program)
            {
                statement.CheckSemantics();           
            }
        }

        public void Execute()
        {
            foreach (var statement in Program)
            {
                statement.Execute();
            }
        }
        
        public int Run()
        {
            foreach(var statement in Program)
            {
                statement.GetScope(Enviroment);
            }
            CheckSemantics();
            Execute();
            return 0 ;
        }
    }
}