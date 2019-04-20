using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKReportsemp.WinForms.Models
{
    public class Prestamos
    {
        public int IdPrestamos { get; set; }
        
        public DateTime Fecha { get; set; }

        public string Leyenda { get; set; }

        public int Mes { get; set; }

        public int Ano { get; set; }

        public int Dia { get; set; }

        public string Caja { get; set; }

        public string Empresa { get; set; }

        public decimal prestamo { get; set; }

       


    }
}
