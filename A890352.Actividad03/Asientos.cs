using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Asientos
    {
        public int NroAsiento { get; set; }
        public string Nombre { get; set; }
        public int CodigoCuenta { get; set; }
        public Double Debe { get; set; }
        public Double Haber { get; set; }

        public string Fecha { get; set; }

        // Con esto nos aseguramos de que ya vaya formateado como dice la actividad.
        public override string ToString()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}", NroAsiento, Fecha, CodigoCuenta, Debe, Haber);
        }
    }
}
