using System;
using System.Collections.Generic;

namespace Gsharp
{
    public interface IFigure
    {
        //Identificador de las Figuras para pintarlas
        string Name { get; set; }
        //Obtener el tipo de figura para vincularlo a la Parte Visual
        FigureType GetFigureType();
        //Trasladar una figura en el plano
        IFigure Traslate(int movX, int movY);
        //Interseccion de una figura con otra figura
        IEnumerable<IExpression> Intersect(IFigure figures);
        IEnumerable<Point> GetPoints(IFigure figures); 
    }
    public interface ILine : IFigure
    {
        //Obtener la Pendiente de una Linea
        float GetPendiente();
    }
}