using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp3
{

    // Cargamos el plan de cuentas en una lista. Check.
    // Comprobamos si existe el archivo Diario y sino lo creamos. Check.
    class Program
    {

        static void Main(string[] args)
        {
            // Inicializamos variables
      
            List<Asientos> Asiento = new();
            string direccion = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Diario.txt");

            #region Variables para el menu 1
            List<PlanCuentas> PlanDeCuentas = new();
            PlanDeCuentas = Funciones.CargarPlanDeCuentas();
            int cantidadAsientos, nroCuenta;
            DateTime fecha;
            Double montoDebe = 0;
            Double montoHaber = 0;
            Double debeIncremental = 0;
            Double haberIncremental = 0;
            Double incrementalCuentasHaber = 0;
            Double incrementalCuentasDebe = 0;

            //Bools de parses
            bool parse1 = false;
            bool parse2 = false;
            bool parse3 = false;
            bool parse4 = false;
            bool parse5 = false;
            bool parse6 = false;
            bool parse7 = false;
            bool parse8 = false;
            //Fin de bools de parses

            bool paso1 = false;
            bool paso2 = false;
            bool paso3 = false;
            #endregion

      

            // Inicio del programa
            // Variable para el menu
            bool continuamos = true;
            while (continuamos)
            {
                Funciones.MostrarMenu();
                int opcion;
                if (Int32.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out opcion))
                {
                    switch (opcion)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine("¿Cuantos asientos quiere ingresar?");
                            do
                            {
                                if (Int32.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out cantidadAsientos))
                                {
                                    if (cantidadAsientos == 0)
                                    {
                                        Funciones.MostrarError("Lo ingresado no es un numero valido. Ingrese nuevamente.");
                                    }
                                    else
                                    {
                                    
                                        parse1 = true;
                                    }

                                }
                                else
                                {
                                    Funciones.MostrarError("Lo ingresado no es un numero valido. Ingrese nuevamente.");
                                }
                            } while (parse1 == false);

                            Funciones.ComprobarTxt();


                            do
                            {
                                // Iniciamos recorrido.
                                for (int i = 0; i < cantidadAsientos; i++)
                                {

                                    Console.WriteLine("-----------Asiento {0}-----------", (i + 1));
                                    Console.WriteLine("Ingrese la fecha del asiento en el siguiente formato: DD/MM/AAAA");
                                    do
                                    {
                                        if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out fecha))
                                        {
                                            if (fecha > DateTime.Today)
                                            {
                                                Funciones.MostrarError("La fecha del asiento no puede ser futura. Ingrese nuevamente.");

                                            }
                                            else
                                            {
                                                parse2 = true;
                                            }
                                        }
                                        else
                                        {
                                            Funciones.MostrarError("Lo ingresado no es una fecha valida. Ingrese nuevamente.");
                                        }

                                    } while (parse2 == false);

                                    parse2 = false;
                                    Console.WriteLine("Ingrese la cantidad de cuentas del DEBE:");
                                    // Ciclo DEBE.
                                    int cantidadCuentasDebe;
                                    do
                                    {
                                        if (Int32.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out cantidadCuentasDebe))
                                        {
                                            if (cantidadCuentasDebe == 0)
                                            {
                                                Funciones.MostrarError("Lo ingresado no es un numero valido. Ingrese nuevamente.");
                                            }
                                            else
                                            {
                                                parse3 = true;
                                            }
                                        }
                                        else
                                        {
                                            Funciones.MostrarError("Lo ingresado no es un numero valido. Ingrese nuevamente.");

                                        }

                                    } while (parse3 == false);
                                    parse3 = false;

                                    for (int d = 0; d < cantidadCuentasDebe; d++)
                                    {
                                        paso1 = false;
                                        do
                                        {
                                            Console.WriteLine("Ingrese Nro de cuenta");
                                            do
                                            {
                                                if (Int32.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out nroCuenta))
                                                {
                                                    parse4 = true;
                                                }
                                                else
                                                {
                                                    Funciones.MostrarError("Lo ingresado no es un numero valido. Ingrese nuevamente.");
                                                }

                                            } while (parse4 == false);
                                            parse4 = false;
                                            PlanCuentas buscar = PlanDeCuentas.Find(x => (x.Codigo == nroCuenta));

                                            if (PlanDeCuentas.Contains(buscar))
                                            {
                                                paso1 = true;



                                            }
                                            else
                                            {
                                                Funciones.MostrarError("No existe la cuenta en el Plan de Cuentas. Intente nuevamente.");
                                            }
                                        }
                                        while (paso1 == false);
                                        paso1 = false;
                                        // Pedimos Montos
                                        Console.WriteLine("Ingrese monto");
                                        do
                                        {
                                            if (Double.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.AllowDecimalPoint, null, out montoDebe))
                                            {
                                                parse5 = true;
                                            }
                                            else
                                            {
                                                Funciones.MostrarError("Lo ingresado no es un numero valido. Ingrese nuevamente.");

                                            }
                                        } while (parse5 == false);
                                        parse5 = false;
                                        debeIncremental += montoDebe;
                                        incrementalCuentasDebe = incrementalCuentasDebe + cantidadCuentasDebe;
                                        Asiento.Add(new Asientos() { NroAsiento = (i + 1), Fecha = fecha.ToString("dd/MM/yyyy"), CodigoCuenta = nroCuenta, Debe = montoDebe, Haber = 0 });

                                    }
                                    // Fin ciclo DEBE

                                    // Inicio Ciclo Haber
                                    Console.WriteLine("Ingrese la cantidad de cuentas del HABER:");
                                    // Ciclo HABER.
                                    int cantidadCuentasHaber;
                                    do
                                    {
                                        if (Int32.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out cantidadCuentasHaber))
                                        {
                                            if (cantidadCuentasHaber == 0)
                                            {
                                                Funciones.MostrarError("Lo ingresado no es un numero valido. Ingrese nuevamente.");
                                            }
                                            else
                                            {
                                                parse6 = true;
                                            }
                                        }
                                        else
                                        {
                                            Funciones.MostrarError("Lo ingresado no es un numero valido. Ingrese nuevamente.");

                                        }

                                    } while (parse6 == false);
                                    parse6 = false;
                                    for (int d = 0; d < cantidadCuentasHaber; d++)
                                    {
                                        paso2 = false;
                                        do
                                        {
                                            Console.WriteLine("Ingrese Nro de cuenta");
                                            do
                                            {
                                                if (Int32.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.None, null, out nroCuenta))
                                                {
                                                    parse7 = true;
                                                }
                                                else
                                                {
                                                    Funciones.MostrarError("Lo ingresado no es un numero valido. Ingrese nuevamente.");
                                                }

                                            } while (parse7 == false);
                                            parse7 = false;
                                            PlanCuentas buscar = PlanDeCuentas.Find(x => (x.Codigo == nroCuenta));

                                            if (PlanDeCuentas.Contains(buscar))
                                            {
                                                paso2 = true;



                                            }
                                            else
                                            {
                                                Funciones.MostrarError("No existe la cuenta en el Plan de Cuentas. Intente nuevamente.");
                                            }
                                        }
                                        while (paso2 == false);
                                        paso2 = false;
                                        // Pedimos Montos
                                        Console.WriteLine("Ingrese monto");
                                        do
                                        {
                                            if (Double.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.AllowDecimalPoint, null, out montoHaber))
                                            {
                                                parse8 = true;
                                            }
                                            else
                                            {
                                                Funciones.MostrarError("Lo ingresado no es un numero valido. Ingrese nuevamente.");

                                            }
                                        } while (parse8 == false);
                                        parse8 = false;
                                        haberIncremental += montoHaber;
                                        incrementalCuentasHaber += cantidadCuentasHaber;
                                        Asiento.Add(new Asientos() { NroAsiento = (i + 1), Fecha = fecha.ToString("dd/MM/yyyy"), CodigoCuenta = nroCuenta, Debe = 0, Haber = montoHaber });
                                    }

                                    Console.WriteLine(debeIncremental);
                                    Console.WriteLine(haberIncremental);
                                    // Comprobamos que DEBE = HABER 
                                    if (debeIncremental == haberIncremental)
                                    {
                                        paso3 = true;
                                    }
                                    else
                                    {
                                        
                                        Funciones.MostrarError("El debe no es igual al haber. Por favor, reingrese los asientos correctamente.");
                                        montoDebe = 0;
                                        montoHaber = 0;
                                        debeIncremental = 0;
                                        haberIncremental = 0;
                                        incrementalCuentasDebe = 0;
                                        incrementalCuentasHaber = 0;
                                        Asiento.Clear();

                                        paso3 = false;
                                        paso1 = false;
                                        paso2 = false;
                                        parse3 = false;
                                        parse4 = false;
                                        parse5 = false;
                                        parse6 = false;
                                        parse7 = false;
                                        parse8 = false;

                                        break;
                                    }



                                }
                            } while (paso3 == false);
                            paso3 = false;

                            //Actualizamos el archivo.
                            foreach (var item in Asiento)
                            {
                                Funciones.ActualizarDiario(item.ToString());
                            }
                            // Mostramos los asientos de una manera bonita.

                            Console.WriteLine("Asientos ingresados: " + Environment.NewLine);
                            Console.WriteLine(String.Format("|{0,-5}|{1}|{2,-14}|{3,-5}|{4,-5}|", "Nro  ", Funciones.centrarTextoParaAsientos("Cuentas", 68), "Tipo", "Debe", "Haber"));

                            Console.WriteLine(Environment.NewLine);

                            for (int i = 0; i < cantidadAsientos; i++)
                            {

                                string fechadeasiento = Asiento.FirstOrDefault(x => x.NroAsiento == (i + 1)).Fecha;
                                string tipoCuenta = "";
                                Console.WriteLine("{0} ---------------------------------{1}-------------------------------", fechadeasiento, (i + 1));
                                Console.WriteLine(Environment.NewLine);

                                foreach (var item in Asiento.Where(x => (x.NroAsiento == (i + 1))))
                                {

                                    PlanCuentas cuentanombre = PlanDeCuentas.Find(x => (x.Codigo == item.CodigoCuenta));
                                    if (item.Debe == 0)
                                    {
                                        if (cuentanombre.Tipo == "Activo")
                                        {
                                            tipoCuenta = "Activo-";
                                        }
                                        else
                                        {
                                            tipoCuenta = cuentanombre.Tipo;
                                        }
                                        Console.WriteLine(String.Format("|{0,-5}|{1,-68}|{2,-14}|{3,-5}|{4,-5}|", item.CodigoCuenta, "      a  " + cuentanombre.Nombre, tipoCuenta, item.Debe, item.Haber));
                                        tipoCuenta = "";
                                    }
                                    else
                                    {
                                        if (cuentanombre.Tipo == "Pasivo")
                                        {
                                            tipoCuenta = "Pasivo-";
                                        }
                                        else if (cuentanombre.Tipo == "PatrimonioNeto")
                                        {
                                            tipoCuenta = "PatrimonioNeto-";
                                        }
                                        else
                                        {
                                            tipoCuenta = cuentanombre.Tipo;
                                        }
                                        Console.WriteLine(String.Format("|{0,-5}|{1,-68}|{2,-14}|{3,-5}|{4,-5}|", item.CodigoCuenta, cuentanombre.Nombre, tipoCuenta, item.Debe, item.Haber));
                                        tipoCuenta = "";

                                    }


                                }
                            }
                            Console.ReadLine();
                            Console.Clear();
                            break;

                      
                        case 2:
                            // Vamos a mostrar todo lo que hay en Diario.txt
                            Console.Clear();
                            List<Asientos> mostrarAsientos = new();
                            mostrarAsientos = Funciones.CargarAsientos();

                     
                 

                            for (int i = 0; i < (Funciones.ContarAsientos()) ; i++)
                            {
                                string tipoCuenta = "";
                                if (mostrarAsientos.FirstOrDefault(x => x.NroAsiento == (i + 1))== null)
                                {
                                    break;
                                }

                                string fechadeasiento = mostrarAsientos.FirstOrDefault(x => x.NroAsiento == (i + 1)).Fecha;
                            
                               

                               
                                Console.WriteLine("{0} ---------------------------------{1}-------------------------------", fechadeasiento, (i + 1));
                                Console.WriteLine(Environment.NewLine);
                                foreach (var item in mostrarAsientos.Where(x => (x.NroAsiento == (i + 1))))
                                {

                                    PlanCuentas cuentanombre = PlanDeCuentas.Find(x => (x.Codigo == item.CodigoCuenta));
                                    if (item.Debe == 0)
                                    {
                                        if (cuentanombre.Tipo == "Activo")
                                        {
                                            tipoCuenta = "Activo-";
                                        }
                                        else
                                        {
                                            tipoCuenta = cuentanombre.Tipo;
                                        }
                                        Console.WriteLine(String.Format("|{0,-5}|{1,-68}|{2,-14}|{3,-5}|{4,-5}|", item.CodigoCuenta, "      a  " + cuentanombre.Nombre, tipoCuenta, item.Debe, item.Haber));
                                        tipoCuenta = "";
                                    }
                                    else
                                    {
                                       
                                        if (cuentanombre.Tipo == "Pasivo")
                                        {
                                            tipoCuenta = "Pasivo-";
                                        }
                                        else if (cuentanombre.Tipo == "PatrimonioNeto")
                                        {
                                            tipoCuenta = "PatrimonioNeto-";
                                        }
                                        else
                                        {
                                            tipoCuenta = cuentanombre.Tipo;
                                        }
                                        Console.WriteLine(String.Format("|{0,-5}|{1,-68}|{2,-14}|{3,-5}|{4,-5}|", item.CodigoCuenta, cuentanombre.Nombre, tipoCuenta, item.Debe, item.Haber));
                                        tipoCuenta = "";
                                    }

                                }
                                Console.WriteLine(Environment.NewLine);
                            }

                               
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 3:
                            Environment.Exit(0);
                            continuamos = false;

                            break;
                        default:

                            Funciones.MostrarError("Lo ingresado no es una opcion valida.");


                            break;

                    }

                }
                else
                {
                    Console.WriteLine("Lo ingresado no es una opcion valida");

                }
            }



      
            Console.ReadLine();

        }
    }
}
