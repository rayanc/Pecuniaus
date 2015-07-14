using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pecuniaus.Web.Models
{
    class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {                       //14-01-2015 02:53:30
            base.DateTimeFormat = "dd-MM-yyyy hh:mm:ss";
        }
    }
    #region CreditReportBase
    public class CreditReportBase
    {

        public Security Security { get; set; }
        public Individual Individual { get; set; }
        public Company Company { get; set; }
        public List<Product> Products { get; set; }
        public List<Product> CurrencyWiseProducts { get; set; }

        public CreditAnalysis creditAnalysis { get; set; }
        //public List<CreditAnalysis> creditAnalysis { get; set; }
        //public List<Arrears> arrears { get; set; }
        //     public List<Delays> delays { get; set; }
        public List<Telephone> Telphones { get; set; }
        public List<Addresses> Addresses { get; set; }
        public List<PersonRelation> Persons { get; set; }
        public string Photo { get; set; }
        public List<Currency> Currencies
        {
            get
            {
                return new List<Currency> {      
                    new Currency {Id="RD", Symbol ="R.D. $" }, 
                    new Currency {Id="US", Symbol ="U.S. $" } 
                };
            }
        }

        public ErrorHandling ErrorHandling { get; set; }
    }

    public class Currency
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
    }

    public class Security
    {
        [JsonProperty("InformacionConsultada")]
        public string InformationConsulted { get; set; }

        [JsonProperty("ConsultaId")]
        public string ConsultationId { get; set; }

        [JsonProperty("FechaHora")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? DateTime { get; set; }

        public string CedulaNueva
        {
            get
            {
                if (InformationConsulted.IndexOf(":") > 1)
                    return InformationConsulted.Substring(InformationConsulted.IndexOf(":") + 1);
                return InformationConsulted;
            }
        }

    }

    public class Individual
    {
        public Int64 creditReportId { get; set; }

        [JsonProperty("Edad")]
        public string Age { get; set; }
        public Int64 merchantId { get; set; }
        public Int64 contractId { get; set; }
        public double creditscore { get; set; }
        public string firstName { get; set; }
        [JsonProperty("Padre")]
        public string fatherName { get; set; }
        [JsonProperty("Madre")]
        public string motherName { get; set; }
        public string category { get; set; }
        [JsonProperty("Apellidos")]
        public string LastName { get; set; }
        [JsonProperty("Nombres")]
        public string Name { get; set; }
        public string middleName { get; set; }
        public string rawData { get; set; }
        public double probabilityOfDefault { get; set; }
        public int monthEvaualted { get; set; }
        //[JsonProperty("FechaHora")]
        //public string timeofreport { get; set; }
        public string passport { get; set; }
        [JsonProperty("Ocupacion")]
        public string occupation { get; set; }

        [JsonProperty("Nacionalidad")]
        public string nationality { get; set; }
        public string errors { get; set; }
        //    [JsonProperty("InformacionConsultada")]
        //     public string informationconsulted { get; set; }
        //InformacionConsultada
        [JsonProperty("Sexo")]
        public string gender { get; set; }
        [JsonProperty("Conyugue")]
        public string conyugue { get; set; }
        public string image { get; set; }
        [JsonProperty("FechaNacimiento")]
        public string BirthDate { get; set; }
        [JsonProperty("LugarNacimiento")]
        public string placeofbirth { get; set; }

    }
    public class Company
    {
        [JsonProperty("Nombre")]
        public string Name { get; set; }

        [JsonProperty("NombreComercial")]
        public string BusinessName { get; set; }

        [JsonProperty("RNC")]
        public string RNC { get; set; }

        [JsonProperty("Siglas")]
        public string Acronym { get; set; }

        [JsonProperty("TipoEmpresa")]
        public string CompanyType { get; set; }

        [JsonProperty("ActividadComercial")]
        public string CommertialActivity { get; set; }

        //[JsonProperty("FechaConstitucion")]
        //public DateTime? ConstitucionDate { get; set; }

        [JsonProperty("Edad")]
        public string Age { get; set; }
    }

    public class Telephone
    {
        public string type { get; set; }
        [JsonProperty("@Suscriptor")]
        public string subscriptor { get; set; }

        [JsonProperty("@Direccion")]
        public string address { get; set; }
        [JsonProperty("#text")]
        public string telephone { get; set; }
    }
    public class Addresses
    {
        [JsonProperty("@Fecha")]
        public string DateFetched { get; set; }
        [JsonProperty("#text")]
        public string Value { get; set; }

    }
    public class Product
    {
        public Int64 id { get; set; }
        public string code { get; set; }

        #region Used for Currencywise products
        public string Currency { get; set; }
        public string CurrencyId { get; set; }

        public decimal? TotalApproved { get { return Accounts.Sum(z => z.TotalApproved); } }
        public decimal? TotalOwed { get { return Accounts.Sum(z => z.TotalOwed); } }
        public decimal? TotalQuota { get { return Accounts.Sum(z => z.Quota); } }
        public decimal? TotalDelay { get { return Accounts.Sum(z => z.totalDelay); } }
        #endregion
        // public List<Accounts> account { get; set; }
        public List<Account> Accounts { get; set; }


        public Product ShallowCopy()
        {
            return (Product)this.MemberwiseClone();
        }

        //public Product DeepCopy()
        //{
        //    Product other = (Product)this.MemberwiseClone();
        //    other.IdInfo = new IdInfo(this.IdInfo.IdNumber);
        //    return other;
        //}

    }

    public class Account
    {
        public string status { get; set; }
        public string currency { get; set; }
        public string CurrencyId { get; set; }

        [JsonProperty("Afiliado")]
        public string affiliates { get; set; }
        [JsonProperty("FechaApertura")]
        public string openedDate { get; set; }
        [JsonProperty("FDR")]
        public decimal Fdr { get; set; }
        [JsonProperty("FechaReportado")]
        public string reportedDate { get; set; }
        [JsonProperty("FechaUltimaTransaccion")]
        public string lastTransactionDate { get; set; }
        [JsonProperty("MesConsolidado")]
        public string consolidateMonth { get; set; }
        //[JsonProperty("TotalAdeudado")]
        public decimal? TotalOwed
        {
            get
            {
                decimal v;
                decimal.TryParse(TotalOwedStr, out v);
                return v;
            }
        }

        [JsonProperty("TotalAdeudado")]
        public string TotalOwedStr { get; set; }

        [JsonProperty("CreditoAprobado")]
        public decimal? TotalApproved { get; set; }
        public decimal? TotalDue { get; set; }
        [JsonProperty("Cuota")]
        public decimal? Quota { get; set; }

        [JsonProperty("TotalAtraso")]
        public int? totalDelay { get; set; }
        [JsonProperty("VecesAtraso30")]
        public int? totalDelay30 { get; set; }
        [JsonProperty("VecesAtraso60")]
        public int? totalDelay60 { get; set; }
        [JsonProperty("VecesAtraso90")]
        public int? totalDelay90 { get; set; }
        [JsonProperty("VecesAtraso120")]
        public int? totalDelay120 { get; set; }
        public string ultimateStatus { get; set; }

        [JsonProperty("EstatusUltimo")]
        public string LatestStaus { get; set; }
        public List<PaymentHistory> paymentHistory { get; set; }
    }
    public class PaymentHistory
    {
        public string F { get; set; }
        public string PDV { get; set; }
        public string text { get; set; }
    }

    public class PersonRelation
    {
        [JsonProperty("Nombres")]
        public string name { get; set; }
        [JsonProperty("Apellidos")]
        public string lastname { get; set; }
        [JsonProperty("CedulaNueva")]
        public string newId { get; set; }
        [JsonProperty("CedulaVieja")]
        public string oldId { get; set; }
        [JsonProperty("Edad")]
        public string age { get; set; }
    }

    public class TotalAmountAccounts
    {

        public List<Currency> Currencies { get; set; }
        public Totales Totals { get; set; }

        public class Currency
        {
            [JsonProperty("@Id")]
            public string Id { get; set; }
            [JsonProperty("Cuentas")]
            public int? Accounts { get; set; }
            [JsonProperty("Cerradas")]
            public int? Closed { get; set; }
            [JsonProperty("Abiertas")]
            public int? Open { get; set; }
            [JsonProperty("Normal")]
            public int? Normal { get; set; }
            public int? OnDelay { get; set; }

            [JsonProperty("AcuerdoPago")]
            public int? PaymentAgreement { get; set; }
            [JsonProperty("EnLegal")]
            public int? InLegal { get; set; }
            [JsonProperty("Castigado")]
            public int? Punished { get; set; }
            [JsonProperty("Vencido")]
            public int? Expired { get; set; }
        }

    }
    public class CreditAnalysis
    {
        public TotalAmountAccounts TotalAmountAccount { get; set; }

        //// Credit Analysis
        //public string currencyId { get; set; }
        //public string mostRecentDate { get; set; }
        //public string mostRecentYears { get; set; }
        //public string mostRecentAmount { get; set; }
        //public string oldestDate { get; set; }
        //public string oldestDateYears { get; set; }
        //public string oldestDateAmount { get; set; }
        //public string retailApprovedDate { get; set; }
        //public string retailApprovedYears { get; set; }
        //public string retailApprovedAmount { get; set; }
        //public string increasedDueDate { get; set; }
        //public string increasedDueYears { get; set; }
        //public string increasedDueAmount { get; set; }
    }
    public class Totales
    {
        //       [JsonProperty("Cuentas")]
        //       public int? Accounts { get; set; }
        //       [JsonProperty("Cerradas")]
        //       public string Closed { get; set; }
        //       [JsonProperty("Abiertas")]
        //       public string Open { get; set; }
        //       [JsonProperty("Normal")]
        //       public string Normal { get; set; }
        ////       [JsonProperty("Abiertas")]
        //       public string OnDelay { get; set; }
        //       public string PaymentAgreement { get; set; }
        //       public string InLegal { get; set; }
        //       public string Punished { get; set; }

        [JsonProperty("Cuentas")]
        public int? Accounts { get; set; }
        [JsonProperty("Cerradas")]
        public int? Closed { get; set; }
        [JsonProperty("Abiertas")]
        public int? Open { get; set; }
        [JsonProperty("Normal")]
        public int? Normal { get; set; }
        public int? OnDelay { get; set; }
        [JsonProperty("AcuerdoPago")]
        public int? PaymentAgreement { get; set; }
        [JsonProperty("EnLegal")]
        public int? InLegal { get; set; }
        [JsonProperty("Castigado")]
        public int? Punished { get; set; }
        [JsonProperty("Vencido")]
        public int? Expired { get; set; }
    }

    public class Delays
    {
        public string worstDelaysDate { get; set; }
        public string worstDelaysYears { get; set; }
        public string worstDelaysDays { get; set; }
        public string creditApproved { get; set; }
        public string delayAmount { get; set; }
        public string product { get; set; }


    }
    public class Arrears
    {
        public string arrearsMostRecentDate { get; set; }
        public string arrearsMostRecentYears { get; set; }
        public string arrearsMostRecentDelayDays { get; set; }
        public string arrearsMostRecentAmount { get; set; }
        public string arrearsOldestDate { get; set; }
        public string arrearsOldestYears { get; set; }
        public string arrearsOldestDelayDays { get; set; }
        public string arrearsOldestYearsAmount { get; set; }
        public string arrearsDate { get; set; }
        public string arrearsYears { get; set; }
        public string arrearsDelayDays { get; set; }
        public double arrearsAmount { get; set; }
    }

}
    #endregion
