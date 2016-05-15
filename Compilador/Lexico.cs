using System;

namespace Compilador
{
  public class Lexico
  {
    //atributos
    private string fuente;
    private string simbolo;
    private int tipo;
    private int indice;
    private bool continua;
    private char c;
    private int estado;

    //constructor sin parametros
    public Lexico()
    {
      indice = 0;
    } //fin del constructor sin parametros

    //constructor parametrizado
    public Lexico(string fuente)
    {
      indice = 0;
      simbolo = "";
      this.fuente = fuente;
    } //fin del constructor parametrizado

    public string Simbolo
    {
      get { return simbolo; }
    }

    public int Tipo
    {
      get { return tipo; }
    }

    public void Entrada(string fuente)
    {
      indice = 0;
      simbolo = "";
      this.fuente = fuente;
    } //fin del metodo Entrada

    public int SiguienteSimbolo()
    {
      estado = 0;
      continua = true;
      simbolo = "";

      while (continua)
      {
        c = SiguienteCaracter();

        switch (estado)
        {
          case 0:
            if (EsDigito(c))
              SiguienteEstado(1);
            else if(EsLetra(c))
              SiguienteEstado(4);
            else if(c == '"')
              SiguienteEstado(5);
            else if(c == '+' || c == '-')
              Aceptacion(7);



            else if(EsEspacio(c))
              Retroceso();
            else if (c == '$')
              Aceptacion(23);
            break;

          case 1:
            if (EsDigito(c))
              SiguienteEstado(1);
            else if (c == '.')
              SiguienteEstado(2);
            else
              Aceptacion(1);

            break;

          case 2:
            if (EsDigito(c))
              SiguienteEstado(3);
            else
              Aceptacion(3);
            break;

            case 3:
            if(EsDigito(c))
              SiguienteEstado(3);
            else
              Aceptacion(3);
            break;

            case 4:
            if(EsLetra(c) || EsDigito(c))
              SiguienteEstado(4);
            else
            Aceptacion(4);
            break;

            case 5:
            if(EsLetra(c) || EsDigito(c))
              SiguienteEstado(5);
            else if(EsEspacio(c))
              SiguienteEstado(5);
            else if (c == '"')
              Aceptacion(6);
            else
              Retroceso();


            break;


        } //fin de switch
      } //fin de while

      switch (estado)
      {
        case 1:
          tipo = TipoSimbolo.Entero;
          break;

          case 3:
          tipo = TipoSimbolo.Real;
          break;

          case 4:
          tipo = TipoSimbolo.Identificador;
          break;

          case 6:
          tipo = TipoSimbolo.Cadena;
          break;

          case 7:
          tipo = TipoSimbolo.OpSuma;
          break;


        case 23:
          tipo = TipoSimbolo.PESOS;
          break;
      } //fin de switch


      return tipo;
    } //fin del metodo SiguienteSimbolo

    private char SiguienteCaracter()
    {
      if (Terminado()) return '$';
      return fuente[indice++];
    } //fin del metodo SiguienteCaracter

    private void SiguienteEstado(int estado)
    {
      this.estado = estado;
      simbolo += c;
    }

    private void Aceptacion(int estado)
    {
      SiguienteEstado(estado);
      continua = false;
    }

    private bool Terminado()
    {
      return indice >= fuente.Length;
    }

    private bool EsLetra(char c)
    {
      return Char.IsLetter(c) || c == '_';
    }

    private bool EsDigito(char c)
    {
      return Char.IsDigit(c);
    }

    private bool EsEspacio(char c)
    {
      return Char.IsWhiteSpace(c);
    }

    private void Retroceso()
    {
      if (c != '$') indice--;
      continua = false;
    }


    public string TipoACadena(int tipo)
    {
      string cadena = "";

      switch (tipo)
      {
        case TipoSimbolo.Error:
          cadena = "Error";
          break;

        case TipoSimbolo.Identificador:
          cadena = "Identificador";
          break;

        case TipoSimbolo.Entero:
          cadena = "Entero";
          break;

        case TipoSimbolo.Real:
          cadena = "Real";
          break;

        case TipoSimbolo.Cadena:
          cadena = "Cadena";
          break;

        case TipoSimbolo.Tipo:
          cadena = "Tipo";
          break;

        case TipoSimbolo.OpSuma:
          cadena = "Operador Suma";
          break;

        case TipoSimbolo.OpMultiplicacion:
          cadena = "Operador Multiplicacion";
          break;

        case TipoSimbolo.OP_RELACIONAL:
          cadena = "Operador Relacional";
          break;

        case TipoSimbolo.OP_OR:
          cadena = "Operador OR";
          break;

        case TipoSimbolo.OP_AND:
          cadena = "Operador AND";
          break;

        case TipoSimbolo.OP_NOT:
          cadena = "Operador NOT";
          break;

        case TipoSimbolo.OP_IGUALDAD:
          cadena = "Operador Igualdad";
          break;

        case TipoSimbolo.PUNTO_COMA:
          cadena = "Punto y Coma";
          break;

        case TipoSimbolo.COMA:
          cadena = "Coma";
          break;

        case TipoSimbolo.PARENTESIS_INICIO:
          cadena = "Parentesis Inicio";
          break;

        case TipoSimbolo.PARENTESIS_FIN:
          cadena = "Parentesis Fin";
          break;

        case TipoSimbolo.LLAVE_INICIO:
          cadena = "Llave Inicio";
          break;

        case TipoSimbolo.LLAVE_FIN:
          cadena = "Llave Fin";
          break;

        case TipoSimbolo.IGUAL:
          cadena = "Operador Asignacion";
          break;

        case TipoSimbolo.IF:
          cadena = "Palabra Reservada if";
          break;

        case TipoSimbolo.WHILE:
          cadena = "Palabra Reservada while";
          break;

        case TipoSimbolo.RETURN:
          cadena = "Palabra Reservada return";
          break;

        case TipoSimbolo.ELSE:
          cadena = "Palabra Reservada else";
          break;

        case TipoSimbolo.PESOS:
          cadena = "Fin de la entrada";
          break;
      } //fin de switch

      return cadena;
    } //fin del metodo TipoACadena
  } //fin de la clase Lexico
} //fin del espacio de nombres Compilador