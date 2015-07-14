using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pecuniaus.Renewal.Models
{
    public class ScoringData
    {
        [Display(Name = "Score")]
        public string Score { get; set; }

        [Display(Name = "Score Redondeado")]
        public string ScoreRedondeado { get; set; }

        [Display(Name = "Letra Final")]
        public string LetraFinal { get; set; }

        [Display(Name = "Total Aproved Amount")]
        public string TotalAprovedAmount { get; set; }

        [Display(Name = "Total Credit Available")]
        public string TotalCreditAvailable { get; set; }

        [Display(Name = "Total Owed Amount")]
        public string TotalOwedAmount { get; set; }

        [Display(Name = "Total Monthly Payment")]
        public string TotalMonthlyPayment { get; set; }

        [Display(Name = "Coeficiente Variacion")]
        public string CoeficienteVariacion { get; set; }

        [Display(Name = "Time Since First Credit")]
        public string TimeSinceFirstCredit { get; set; }

        [Display(Name = "Date Of First Credit")]
        public string DateOfFirstCredit { get; set; }

        [Display(Name = "Cantidad Montos Distinto De Cero")]
        public string CantidadMontosDistintoDeCero { get; set; }

        [Display(Name = "Cantidad Montos En Cero")]
        public string CantidadMontosEnCero { get; set; }

        [Display(Name = "Start Date")]
        public string StartDate { get; set; }

        [Display(Name = "Coeficiente Crecimiento")]
        public string CoeficienteCrecimiento { get; set; }

        [Display(Name = "Pct Cuota")]
        public string PctCuota { get; set; }

        [Display(Name = "Pct Adeudado")]
        public string PctAdeudado { get; set; }

        [Display(Name = "Pct Alquiler")]
        public string PctAlquiler { get; set; }

        [Display(Name = "Time Since Start Date")]
        public string TimeSinceStartDate { get; set; }

        [Display(Name = "Industria Puntaje")]
        public string IndustriaPuntaje { get; set; }

        [Display(Name = "Alquiler Puntaje")]
        public string AlquilerPuntaje { get; set; }

        [Display(Name = "Local Puntaje")]
        public string LocalPuntaje { get; set; }

        [Display(Name = "Crecimiento Puntaje")]
        public string CrecimientoPuntaje { get; set; }

        [Display(Name = "Tiempo Puntaje")]
        public string TiempoPuntaje { get; set; }

        [Display(Name = "Adeudado Puntaje")]
        public string AdeudadoPuntaje { get; set; }

        [Display(Name = "Cuota Corporativa Puntaje")]
        public string CuotaCorporativaPuntaje { get; set; }

        [Display(Name = "CV Puntaje")]
        public string CVPuntaje { get; set; }

        [Display(Name = "Industria Puntaje Ponderado")]
        public string IndustriaPuntajePonderado { get; set; }

        [Display(Name = "Alquiler Puntaje Ponderado")]
        public string AlquilerPuntajePonderado { get; set; }

        [Display(Name = "Local Puntaje Ponderado")]
        public string LocalPuntajePonderado { get; set; }

        [Display(Name = "Crecimiento Puntaje Ponderado")]
        public string CrecimientoPuntajePonderado { get; set; }

        [Display(Name = "TiempoPuntajePonderado")]
        public string TiempoPuntajePonderado { get; set; }

        [Display(Name = "Adeudado Puntaje Ponderado")]
        public string AdeudadoPuntajePonderado { get; set; }

        [Display(Name = "Cuota Corporativa Puntaje Ponderado")]
        public string CuotaCorporativaPuntajePonderado { get; set; }

        [Display(Name = "CV Puntaje Ponderado")]
        public string CVPuntajePonderado { get; set; }

        [Display(Name = "Adverse Reason 1")]
        public string AdverseReason1 { get; set; }

        [Display(Name = "Adverse Reason 2")]
        public string AdverseReason2 { get; set; }

        [Display(Name = "Adverse Reason 3")]
        public string AdverseReason3 { get; set; }

        [Display(Name = "Adverse Reason 4")]
        public string AdverseReason4 { get; set; }

        [Display(Name = "Adverse Reason 5")]
        public string AdverseReason5 { get; set; }

        [Display(Name = "Adverse Reason 6")]
        public string AdverseReason6 { get; set; }

        [Display(Name = "Adverse Reason 7")]
        public string AdverseReason7 { get; set; }
    }
}