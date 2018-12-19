

namespace EKReportsemp.Common.Models
{

    using System;
    using System.ComponentModel.DataAnnotations;
    public class CRM_SEMP_SALDOS
    {
        [Key]
        public int idCRM_SEMP_SALDOS { get; set; }
        public string caja { get; set; }
        public string numcaja { get; set; }
        public decimal saldo { get; set; }
        public DateTime fecha { get; set; }
        public string localidad { get; set; }
    }
}
