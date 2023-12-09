using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Gsharp;
using System.Threading;
using System.IO;

namespace Geo_Wall_E
{
    public partial class Work : Form
    {
        public Work()
        {
            InitializeComponent();
        }
        private List<IFigure> Figures = new List<IFigure>();

        #region Run Code
        private void Button1_Click(object sender, EventArgs e)
        {
            ErrorShow.Text = "";
            if (textBox1.Text == "") return;
            //Reinicia el lienzo
            lienzo.Refresh();
            //Guarda la informacion del TextBox donde se escribe el codigo
            string code = textBox1.Text;
            //Hacemos una nueva lista 
            Figures = new List<IFigure>();

            //Inicia el proceso de Tokenizacion, Parseo y Chequeo Semantico del codigo
            try
            {
                SyntaxTree syntax = CallLogic.CallLogic.WorkWithCode(code);
                foreach(var item in syntax.Program)
                {
                    if(item is DrawStatement)
                    {
                        var test = (DrawStatement)item;
                        test.DrawThis += DrawFigure;
                    }
                    item.Execute();
                    Thread.Sleep(100);
                }
            }
            catch(Exception E)
            {
                ErrorShow.Text = E.Message;
            }
        }

        private void DebugButton_Click(object sender, EventArgs e)
        {
            int lines = 0;
            string codeToDebug = "";
            string temp = textBox1.Text;
            if (numberDegub.Text != "")
            {
                lines = int.Parse(numberDegub.Text);
            }
            if (lines > 0 && lines < textBox1.Lines.Length)
            { 
                for (int i = 0; i <= lines - 1; i++)
                {
                    codeToDebug += textBox1.Lines[i] + "\n";
                }
                textBox1.Text = codeToDebug;
            }
            Button1_Click(sender, e);
            textBox1.Text = temp;
        }
        #endregion

        #region Save and Load
        //Boton para Salvar Proyectos
        private void Button2_Click(object sender, EventArgs e)
        {
            if(saveFileArchive.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileArchive.FileName;
                string code = textBox1.Text;
                StreamWriter streamWriter = File.CreateText(path);

                streamWriter.Write(code);
                streamWriter.Flush();
                streamWriter.Close();
            }
        }
        //Boton para Cargar Proyectos
        private void Button3_Click(object sender, EventArgs e)
        {
            if(openFile.ShowDialog() == DialogResult.OK)
            {
                string path = openFile.FileName;
                string import = File.ReadAllText(path);
                textBox1.Text = textBox1.Text + "\n" + import;
            }
        }

        #endregion

        #region Draw Section
        public void DrawFigure(IFigure figure)
        {
            /*Este método es el encargado de dibujar las figuras, llamando al método correspondiente.
            Utiliza herramientas de Windows Form, el lector podrá darse cuenta de que hace cada método
            solamente leyendo el codigo*/
            Graphics graphics = lienzo.CreateGraphics();

            /*Antes de agregar una figura a la lista preguntar si no existe ninguna con su mismo nombre
            Esta línea ayuda a la hora de trasladar las figuras en el lienzo*/
            if (!(Figures.Any(x => x.Name == figure.Name))) { Figures.Add(figure); }
            FigureType thisType = figure.GetFigureType();
            //Chequear el tipo de figura para llamar al metodo correspondientes
            if (thisType == FigureType.Point)
            {
                Gsharp.Point p1 = (Gsharp.Point)figure;
                MyDrawPoint(p1, graphics);
                return;
            }
            if (thisType == FigureType.Circle)
            {
                Circle c1 = (Circle)figure;
                MyDrawCircle(c1, graphics);
                return;
            }
            if (thisType == FigureType.Segment)
            {
                Segment l1 = (Segment)figure;
                MyDrawSegment(l1, graphics);
                return;
            }
            if (thisType == FigureType.Line)
            {
                Line l1 = (Line)figure;
                MyDrawLine(l1, graphics);
                return;
            }
            if (thisType == FigureType.Ray)
            {
                Ray l1 = (Ray)figure;
                MyDrawRay(l1, graphics);
                return;
            }
            if (thisType == FigureType.Arc)
            {
                Arc a1 = (Arc)figure;
                MyDrawArc(a1, graphics);
                return;
            }
        }
        //Draw point
        private void MyDrawPoint(Gsharp.Point point, Graphics graphics)
        {
            graphics.FillEllipse(Brushes.Red, point.X - 3, point.Y - 3, 6, 6);
            graphics.DrawString(point.Name, new Font("Arial", 10), Brushes.Black, point.X, point.Y);
        }
        //Draw Circle
        private void MyDrawCircle(Circle circle, Graphics graphics)
        {
            graphics.DrawEllipse(new Pen(System.Drawing.Color.Black), circle.Center.X - circle.Ratio, circle.Center.Y - circle.Ratio, (float)circle.Ratio * 2, (float)circle.Ratio * 2);
            graphics.FillEllipse(Brushes.Black, circle.Center.X - 3, circle.Center.Y - 3, 6, 6);
        }
        //Draw Segment
        private void MyDrawSegment(Segment line, Graphics graphics)
        {
            graphics.DrawLine(new Pen(System.Drawing.Color.Black), line.p1.X, line.p1.Y, line.p2.X, line.p2.Y);
            graphics.FillEllipse(Brushes.Red, line.p1.X - 3, line.p1.Y - 3, 6, 6);
            graphics.FillEllipse(Brushes.Red, line.p2.X - 3, line.p2.Y - 3, 6, 6);
        }
        //Draw Line
        private void MyDrawLine(Line line, Graphics graphics)
        {
            //Para dibujar una linea (recta en geometria) se usa la ecuación de la recta, para determinar
            //los interceptos con el ancho y alto del lienzo, conociendo los dos puntos por los que la recta pasa
            float m = line.GetPendiente();
            float n = line.p1.Y - m * line.p1.X;
            (Gsharp.Point, Gsharp.Point) interceps = GetIntersepts(m, n);
            graphics.DrawLine(new Pen(System.Drawing.Color.Black), interceps.Item1.X, interceps.Item1.Y, interceps.Item2.X, interceps.Item2.Y);
            graphics.FillEllipse(Brushes.Red, line.p1.X - 3, line.p1.Y - 3, 6, 6);
            graphics.FillEllipse(Brushes.Red, line.p2.X - 3, line.p2.Y - 3, 6, 6);
        }
        //Draw Ray
        private void MyDrawRay(Ray line, Graphics graphics)
        {
            //Cuando conoce el punto inicial del rayo, halla los interceptos con los extremos del lienzo
            //Mediante el uso del Vector Director y de la ecuacion de la recta se obtiene el sentido
            //del rayo y bueno, luego se pinta
            graphics.DrawLine(new Pen(System.Drawing.Color.Black), line.p1.X, line.p1.Y, line.p2.X, line.p2.Y);
            float m = line.GetPendiente();
            float n = line.p1.Y - m * line.p1.X;
            (Gsharp.Point, Gsharp.Point) interceps = GetIntersepts(m, n);
            //Vector Director de la recta que pasa por los puntos del rayo
            (float, float) vector1 = (line.p2.X - line.p1.X, line.p2.Y - line.p2.X);
            //Vector Director del primer punto del rayo con respecto al intercepto mas a la izquierda
            (float, float) vector2 = (interceps.Item1.X - line.p1.X, interceps.Item1.Y - line.p1.Y);
            //Vector Director del primer punto del rayo con respecto al intercepto mas a la derecha
            (float, float) vector3 = (interceps.Item2.X - line.p1.X, interceps.Item2.Y - line.p1.Y);
            //Verficar si el sentido del rayo es el mismo que el sentido de la izquierda
            if (vector1.Item1 > 0 && vector2.Item1 > 0 || vector1.Item1 < 0 && vector2.Item1 < 0)
            {
                graphics.DrawLine(new Pen(System.Drawing.Color.Black), line.p1.X, line.p1.Y, interceps.Item1.X, interceps.Item1.Y);
            }
            //Verificar si el sentido del rayo es el mismo que el sentido de la derecha
            else if (vector1.Item1 > 0 && vector3.Item1 > 0 || vector1.Item1 < 0 && vector3.Item1 < 0)
            {
                graphics.DrawLine(new Pen(System.Drawing.Color.Black), line.p1.X, line.p1.Y, interceps.Item2.X, interceps.Item2.Y);
            }
            graphics.FillEllipse(Brushes.Red, line.p1.X - 3, line.p1.Y - 3, 6, 6);
            graphics.FillEllipse(Brushes.Red, line.p2.X - 3, line.p2.Y - 3, 6, 6);
        }
        private void MyDrawArc(Arc arc, Graphics graphics)
        {
            graphics.FillEllipse(Brushes.Red, arc.origin.X - 3, arc.origin.Y - 3, 6, 6);
            graphics.FillEllipse(Brushes.Red, arc.first.X - 3, arc.first.Y - 3, 6, 6);
            graphics.FillEllipse(Brushes.Red, arc.second.X - 3, arc.second.Y - 3, 6, 6);

            float startAngle = GetAngle(arc.origin, arc.second);
            float endAngle = GetAngle(arc.origin, arc.first);

            float possitiveStart = Math.Sign(startAngle) * startAngle;
            float possitiveEnd = Math.Sign(endAngle) * endAngle;
            float sweepAngle = 0;

            if (Math.Sign(startAngle) == Math.Sign(endAngle))
            {
                if (startAngle < 0)
                    sweepAngle = possitiveStart > possitiveEnd ? possitiveStart - possitiveEnd : 360 - possitiveEnd + possitiveStart;
                else
                    sweepAngle = possitiveStart > possitiveEnd ? 360 - possitiveStart + possitiveEnd : possitiveEnd - possitiveStart;
            }
            else
            {
                sweepAngle = Math.Sign(endAngle) > 0 ? possitiveEnd + possitiveStart : 360 - possitiveEnd - possitiveStart;
            }

            graphics.DrawArc(new Pen(System.Drawing.Color.Black), arc.origin.X - arc.measure, arc.origin.Y - arc.measure, arc.measure * 2, arc.measure * 2, startAngle, sweepAngle);
        }
        #endregion

        #region Geometric Concepts
        private float GetPendiente(Gsharp.Point p1, Gsharp.Point p2)
        {
            return ((p2.Y - p1.Y) / (p2.X - p1.X));
        }
        private (Gsharp.Point, Gsharp.Point) GetIntersepts(float m, float n)
        {
            float intercepUP = -(n / m);
            if(intercepUP < 0)
            {
                return(new Gsharp.Point(0, n, ""), new Gsharp.Point((lienzo.Height - 1 - n) / m, lienzo.Height - 1, ""));
            }
            if(n < 0)
            {
                return (new Gsharp.Point(intercepUP, 0, ""), new Gsharp.Point(lienzo.Width - 1, m * lienzo.Width - 1 + n, ""));
            }
            return (new Gsharp.Point(intercepUP, 0, ""), new Gsharp.Point(0, n, ""));
        }
        private float GetAngle(Gsharp.Point p1, Gsharp.Point p2)
        {
            float m = GetPendiente(p1, p2);
            float angle = (float)(Math.Atan(m) * 180 / Math.PI);
            if (m >= 0)
            {
                angle = p1.Y > p2.Y ? -180 + angle : angle;
            }
            else
            {
                angle = p1.Y > p2.Y ? angle : 180 + angle;
            }
            return angle;
        }
        #endregion

        #region Traslate Figures
        private void Traslation(int X, int Y)
        {
            List<IFigure> traslates = new List<IFigure>();
            lienzo.Refresh();
            foreach (var item in Figures)
            {
                IFigure traslate = item.Traslate(X,Y);
                DrawFigure(traslate);
                traslates.Add(traslate);
            }
            Figures = traslates;
        }
        private void Up_Click(object sender, EventArgs e)
        {
            Traslation(0, -10);
        }

        private void Left_Click(object sender, EventArgs e)
        {
            Traslation(-10, 0);
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            Traslation(0, 10);
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            Traslation(10, 0);
        }

        #endregion

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //if(e.KeyValue == (int)Keys.Enter || e.KeyValue == (int)Keys.Back)
            {
                lineCounter.Text = "";
                for(int i = 1; i <= textBox1.Lines.Length; i++)
                {
                    lineCounter.Text += i + "\r\n";
                }
            }
        }
    }
}