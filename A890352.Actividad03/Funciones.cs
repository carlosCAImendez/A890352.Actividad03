using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp3
{

    // Funciones comunes a toda la aplicacion
    class Funciones
    {
        public static List<PlanCuentas> CargarPlanDeCuentas()
        {
           List<PlanCuentas> PlanDeCuentas = new();
        string direccion = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),  "Actividad 03 - Plan de cuentas.txt");
        
            FileInfo elPlan = new FileInfo(direccion);
            if (!elPlan.Exists)
            {
                Funciones.MostrarError("No se encuentra el plan de cuentas");
                Console.ReadLine();
                Environment.Exit(0);
            }

            try 
            {
                StreamReader abridor = elPlan.OpenText();
                if (elPlan.Length == 0)
                {
                    Funciones.MostrarError("El archivo de plan de cuentas esta vacio!.");
                    Console.ReadLine();
                    Environment.Exit(0);
                    return null;
                }
                while (!abridor.EndOfStream)
                {

                    string Linea = abridor.ReadLine();
                
                        string[] Vector = Linea.Split('|');

                        if (Vector[0] == "Codigo" && Vector[1] == "Nombre" && Vector[2] == "Tipo")
                        {
                            // Con esto nos salteamos la primera linea.
                        }

                        else
                        {
                            PlanCuentas P = new PlanCuentas();
                            P.Codigo = Int32.Parse(Vector[0]);
                            P.Nombre = Vector[1];
                            P.Tipo = Vector[2];
                            PlanDeCuentas.Add(P);

                        
                    }
                }

                abridor.Close();
                return PlanDeCuentas;
            }
            catch (IOException e)
            {
                Console.WriteLine("No se pudo cargar el plan de cuentas. Vuelva a abrir el programa:  " + e.Source);
                Console.ReadLine();
                Environment.Exit(0);
                return null;
            }

    
        }

        // Funcion para contar las lineas de Diario.txt

        public static int ContarAsientos()
        {
            string direccion = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Diario.txt");
            FileInfo elPlan = new FileInfo(direccion);
            int cantidad = 0;
            try
            {
                StreamReader abridor = elPlan.OpenText();
                while (!abridor.EndOfStream)
                {
                    string Linea = abridor.ReadLine();
                 
                        cantidad = cantidad + 1;
                  
                   
                }
                abridor.Close();
                return cantidad;
            }   
            catch
            {
                Funciones.MostrarError("No hay asientos para mostrar.");
                Console.ReadLine();
                return 0;
            }
           
        }
        public static List<Asientos> CargarAsientos()
        {
            List<Asientos> Asiento = new();
            string direccion = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Diario.txt");
            FileInfo elPlan = new FileInfo(direccion);


            try
            {
                StreamReader abridor = elPlan.OpenText();
                if (elPlan.Length == 0)
                {
                    Funciones.MostrarError("No hay asientos para mostrar.");
                    Console.ReadLine();
                    return null;
                }

                while (!abridor.EndOfStream)
                {
                    string Linea = abridor.ReadLine();
                   

                    
                    string[] Vector = Linea.Split('|');

                   
                        Asientos P = new Asientos();
                    P.NroAsiento = Int32.Parse(Vector[0]);
                    P.Fecha = Vector[1];
                    P.CodigoCuenta = Int32.Parse(Vector[2]);
                    P.Debe = Double.Parse(Vector[3]);
                    P.Haber = Double.Parse(Vector[4]);
                    Asiento.Add(P);
                    


                }

                abridor.Close();
                return Asiento;
            }
            catch 
            {
                Console.WriteLine("No se pudo abrir el archivo Diario.txt");
                Console.ReadLine();
                return null;
            }


        } 
        public static bool ComprobarTxt()
        {
            string direccion = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Diario.txt");
            FileInfo losAsientos = new FileInfo(direccion);
            if (!losAsientos.Exists)
            {
                // Generamos el archivo.
                try
                {
                    File.Create(direccion).Dispose();
                    return true;
                }
                catch
                {
                    return false;
                }

            }
            else
            {
                return false;
            }

        }

        public static bool ActualizarDiario(string linea)
        {
            string direccion = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Diario.txt");
            try
            {
                StreamWriter actualizador = new StreamWriter(direccion, true);
                if (linea.Length == 0)
                {
                    return false;
                }
                else
                { 
                actualizador.WriteLine(linea);
                actualizador.Close();
                return true;
                }
            }
            catch
            {
                Funciones.MostrarError("No se pudieron guardar los asientos.");
                return false;
            }
        }

        public static string centrarTextoParaAsientos(string linea, int largo)
        {
            if (linea.Length >= largo)
            {
                return linea;
            }

            int izquierda = (largo - linea.Length) / 2;
            int derecha = largo - linea.Length - izquierda;

            return new string(' ', izquierda) + linea + new string(' ', derecha);
        }

        public static void MostrarMenu()
        {
            Console.WriteLine("¿Que desea hacer?");
            Console.WriteLine("1 - Ingresar asientos"); // Codeado.
            Console.WriteLine("2 - Ver asientos existentes");
            Console.WriteLine("3 - Salir");
        }

        public static string MostrarError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mensaje);
            Console.ResetColor();
            return null;
        }

    }
}
