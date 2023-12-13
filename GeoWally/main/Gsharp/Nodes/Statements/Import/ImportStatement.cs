using System.IO;

namespace Gsharp
{
    public sealed class ImportStatement : IStatement
    {
        public string Url { get; }
        private Scope referencedScope;

        public ImportStatement(string url)
        {
            Url = url;
        }

        public void GetScope(Scope actual)
        {
            referencedScope = actual ;
            string code = File.ReadAllText(@".\Projects\" + Url + ".geo.txt");
            var lexer = new Lexer(code);
            var parser = new Parser(lexer.GetTokenList());
            var statementList = parser.ParseStatementList();

            foreach (var statement in statementList)
            {
                statement.GetScope(referencedScope);
                statement.CheckSemantics();
                statement.Execute();
            }
        } 
        public WalleType CheckSemantics(){return WalleType.Void;}
        
        public void Execute(){}
        public override string ToString() => "import " + Url;
    }
}