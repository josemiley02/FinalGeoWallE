# GeoWally

Estructura de la parte logica
-CompilatorTools
-Figures
-Lexer 
-Expressions
-Parser
-Scope
-Types

Compilator Tools :
  Es una clase que tiene funciones utiles que se usan durante el programa
  (Cambiar la funcion ShowReports para que en vez de escribir los errores en consola lo haga en la interfaz visual)

Figures :
  Tiene la interface IFigure y las clases Punto, Linea y Circulo(tienen la misma implementacion que habias hecho)
  (En Params van las expresiones que tenias que eran de la forma circle( p1 , 50) ) *no las puse pa que no diera error, mnn hago que coincidan con las nuevas expresiones y las subo

Lexer :
  Tiene la clase Lexer y la clase Token, con todos los metodos para tokenizar el string que se recibe
  (Todo en talla)

Expressions
Aqui estan todos los tipos de expresiones de G# :
  -Unarias : 
    .NotExpression
    .NegativeExpression
  
  -Binarias 
    .Aritmeticas : Suma , Resta , Multiplicacion , Division
    .Comparadores : >= , > , <= , < , == , != 
    .Operadores Logicos : and , or

  -Condicional(if-else)
  
  -Figuras
    .Circle
    .Lines(line , segment , ray)
    .Point

  -DrawExpression
  -RestoreExpression

Las expresiones tienen metodos como GetScope, CheckSemantics, Evaluate , ToString()
Algunas implemetan la interfaz IBooleable y su metodo ConvertToBool() , otras implementan la interfaz IComparable<IExpression>(para compararse obviamente)

Parser
  Tiene la clase Parser con todos los metodos y cosas necesarias para parsear las diferentes expresiones *hay unas cuantas funciones con NotImplementExcepcon() es para que sirvan de esqueleto para las cosas que aun no he hecho
  y que sea solo rellenarlas
    
Scope
  La misma clase Scope que tenias(esta dura xD)

Types
  Ahi meti todos los enums que tienen que ver con los tipos del lenguaje, los tipo de tokens, de figuras y los tipos staticos del lenguaje

*Agregale lo que haga falta pa que pinche la Visual , ya no debo tocar mas nada en las expresiones
