namespace Gsharp
{
    public sealed class RestoreStatement : IStatement
    {        
        public void GetScope(Scope Actual){}
        public WalleType CheckSemantics() => WalleType.Void;
        public void Execute() => CompilatorTools.RestoreColor();
        public override string ToString() => "restore" ;
    }

    public sealed class ColorStatement : IStatement
    {
        public string Color { get; }
        public ColorStatement(string color)
        {
            Color = color;
        }

        public void GetScope(Scope Actual){}
        public WalleType CheckSemantics() => WalleType.Void ;

        public void Execute() => CompilatorTools.AddColor(Color);
        public override string ToString() => "color " + Color.ToString();
    }
}