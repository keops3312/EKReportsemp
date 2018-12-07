

namespace EKReportsemp.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class CRM_SEMP
    {
        [Key]
        public int idCRM_SEMP_TOTAL { get; set; }
        public string LOCALIDAD_NOM { get; set; }
        public DateTime FECHA_NOM { get; set; }
        public string HORA_NOM { get; set; }
        public decimal PRESTAMO_C { get; set; }
        public decimal PRESTAMO_P { get; set; }
        public decimal REFRENDO_C { get; set; }
        public decimal REFRENDO_P { get; set; }
        public decimal REMISION_C { get; set; }
        public decimal REMISION_P { get; set; }
        public decimal APARTADO_C { get; set; }
        public decimal APARTADO_P { get; set; }
        public decimal DEPOSITO_C { get; set; }
        public decimal DEPOSITO_P { get; set; }
        public decimal RETIRO_C { get; set; }
        public decimal RETIRO_P { get; set; }
        public decimal REIMPRESION_C { get; set; }
        public decimal REIMPRESION_P { get; set; }
        public decimal ABONO_C { get; set; }
        public decimal ABONO_P { get; set; }
        public decimal MOVS { get; set; }
        public decimal GASTOS_C { get; set; }
        public decimal GASTOS_P { get; set; }
        public decimal CANCEL_C { get; set; }
        public decimal CANCEL_CP { get; set; }
        public decimal CANCEL_N { get; set; }
        public decimal CANCEL_NP { get; set; }
        public decimal CANCEL_R { get; set; }
        public decimal CANCEL_RP { get; set; }
        public decimal C_AU { get; set; }
        public decimal P_AU { get; set; }
        public decimal C_JOY { get; set; }
        public decimal P_JOY { get; set; }
        public decimal C_ELE { get; set; }
        public decimal P_ELE { get; set; }
        public decimal C_OTR { get; set; }
        public decimal P_OTR { get; set; }
        public decimal C_NR { get; set; }
        public decimal C_ND { get; set; }
    }
}
