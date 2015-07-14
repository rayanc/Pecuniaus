using Pecuniaus.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pecuniaus.Models.Contract
{
    public class ContractIOU
    {
        public string LegalCompanyName { get; set; }
        public string RNC { get; set; }
        public string CompanyAddress { get; set; }
        public string Province { get; set; }
        public DateTime FundingDate { get; set; }

        public string FundingDateDay
        {
            get
            {
                return DateUtility.GetDayWord(FundingDate.Day);
            }
            set { } //To enable serialization
        }

        public string FundingDateYear
        {
            get
            {
                return Conversions.NumberToText(Convert.ToInt32(FundingDate.ToString("yy")));
            }
            set { } //To enable serialization
        }

        public DateTime LetterDate { get; set; }

        public string LetterDateDay
        {
            get
            {
                return DateUtility.GetDayWord(LetterDate.Day);
            }
            set { } //To enable serialization
        }

        public string LetterDateYear
        {
            get
            {
                return Conversions.NumberToText(Convert.ToInt32(LetterDate.ToString("yy")));
            }
            set { } //To enable serialization
        }

        public double TotalOwnedAmount { get; set; }

        public string TotalOwnedAmountWords
        {
            get
            {
                return Conversions.NumberToText((int)TotalOwnedAmount);
            }
            set { }
        }

        public long StateId { get; set; }

        public long BusinesTypeId { get; set; }

        public IOUOwnerList AuthorizedOwner
        {
            get
            {
                return OwnerList.FirstOrDefault(a => a.IsAuthorised == true);
            }
            set { }
        }

        public List<IOUOwnerList> OwnerList { get; set; }
    }

    public class IOUOwnerList
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Province { get; set; }
        public string Address { get; set; }
        public bool IsAuthorised { get; set; }
    }
}
