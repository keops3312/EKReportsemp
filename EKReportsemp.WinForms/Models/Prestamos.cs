

namespace EKReportsemp.WinForms.Models
{
    using System;
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
