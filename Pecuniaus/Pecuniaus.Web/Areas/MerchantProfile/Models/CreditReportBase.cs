using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pecuniaus.MerchantProfile.Models
{
    public class CreditReportBase
    {

        public Individual ind { get; set; }
        public Company company { get; set; }
        public List<Product> product { get; set; }
        public List<CreditAnalysis> creditAnalysis { get; set; }
        public List<Arrears> arrears { get; set; }
        public List<Delays> delays { get; set; }
        public List<Telephone> telphones { get; set; }
        public List<Addresses> address { get; set; }
        public List<PersonRelation> person { get; set; }
        public Totales totales { get; set; }
        public string photo { get; set; }

    }
    public class Individual
    {
        public Int64 creditReportId { get; set; }
        public string age { get; set; }
        public Int64 merchantId { get; set; }
        public Int64 contractId { get; set; }
        public double creditscore { get; set; }
        public string firstName { get; set; }
        public string fatherName { get; set; }
        public string motherName { get; set; }
        public string category { get; set; }
        public string lastName { get; set; }
        public string name { get; set; }
        public string middleName { get; set; }
        public string rawData { get; set; }
        public double probabilityOfDefault { get; set; }
        public int monthEvaualted { get; set; }
        public string timeofreport { get; set; }
        public string passport { get; set; }
        public string occupation { get; set; }
        public string nationality { get; set; }
        public string errors { get; set; }
        public string informationconsulted { get; set; }
        //InformacionConsultada

        public string gender { get; set; }
        public string conyugue { get; set; }
        public string image { get; set; }
        public string dob { get; set; }
        public string placeofbirth { get; set; }
        public string consultationId { get; set; }

    }
    public class Company
    {
        public string name { get; set; }
        public string commercialname { get; set; }
        public string rnc { get; set; }
        public string companyType { get; set; }
        public string commercialActivity { get; set; }
        public string foundedDate { get; set; }
    }
    public class Telephone
    {

        public string type { get; set; }
        public string subscriptor { get; set; }
        public string address { get; set; }
        public string telephone { get; set; }
    }
    public class Addresses
    {
        public string DateFetched { get; set; }
        public string Value { get; set; }

    }
    public class Product
    {
        public Int64 id { get; set; }
        public string code { get; set; }
        public List<Accounts> account { get; set; }

    }
    public class Accounts
    {

        public string status { get; set; }
        public string currency { get; set; }
        public string affiliates { get; set; }
        public string openedDate { get; set; }
        public string fdr { get; set; }
        public string reportedDate { get; set; }
        public string lastTransactionDate { get; set; }
        public string consolidateMonth { get; set; }
        public string totalDelay { get; set; }
        public string totalOwed { get; set; }
        public string totalapproved { get; set; }
        public string totaldue { get; set; }
        public string quota { get; set; }
        public string totalDelay30 { get; set; }
        public string totalDelay60 { get; set; }
        public string totalDelay90 { get; set; }
        public string totalDelay120 { get; set; }
        public string ultimateStatus { get; set; }
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
        public string name { get; set; }
        public string lastname { get; set; }
        public string newId { get; set; }
        public string oldId { get; set; }
        public string age { get; set; }


    }

    public class CreditAnalysis
    {
        // Credit Analysis
        public string currencyId { get; set; }
        public string mostRecentDate { get; set; }
        public string mostRecentYears { get; set; }
        public string mostRecentAmount { get; set; }
        public string oldestDate { get; set; }
        public string oldestDateYears { get; set; }
        public string oldestDateAmount { get; set; }
        public string retailApprovedDate { get; set; }
        public string retailApprovedYears { get; set; }
        public string retailApprovedAmount { get; set; }
        public string increasedDueDate { get; set; }
        public string increasedDueYears { get; set; }
        public string increasedDueAmount { get; set; }



    }
    public class Totales
    {
        public string TotalAccounts { get; set; }
        public string TotalClosed { get; set; }
        public string TotalOpen { get; set; }
        public string TotalNormal { get; set; }
        public string TotalOnDelay { get; set; }
        public string PaymentAgreement { get; set; }
        public string InLegal { get; set; }
        public string Punished { get; set; }
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