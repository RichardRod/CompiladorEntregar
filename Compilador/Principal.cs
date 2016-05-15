using System;
using System.IO;

namespace Compilador
{
  public class Principal
  {
    public static void Main(string[] args)
    {
      Lexico lexico = new Lexico();
      Principal principal = new Principal();

      //lexico.Entrada(principal.LeerArchivo());
      lexico.Entrada("1 2 3 99 9.99999999 888 8 8 vaselina var1 var2 +");


      Console.WriteLine("Simbolo\t\tTipo");
      while (lexico.Simbolo.CompareTo("$") != 0)
      {

        lexico.SiguienteSimbolo();

        Console.WriteLine(lexico.Simbolo + "\t\t" + lexico.TipoACadena(lexico.Tipo));
      }
    } //fin del metodo de entrada

    private string LeerArchivo()
    {
      String linea = "";
      string contenido = "";
      string nombreArchivo = "archivoFuente.txt";
      using (var lector = new StreamReader(nombreArchivo))
      {
        while ((linea = lector.ReadLine()) != null)
        {
          contenido += linea;
        } //fin de while
      } //fin de using

      return contenido;
    } //fin del metodo LeerArchivo
  } //fin de la clase Princupal
}//fin del espacio de nombres Compilador