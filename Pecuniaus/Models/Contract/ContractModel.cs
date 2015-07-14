using System.ComponentModel.DataAnnotations;

namespace Pecuniaus.Models.Contract
{
    public class ContractModel
    {
        [DataType(DataType.Currency)]
        public decimal AdminExp { get; set; }
//        public string AdministrativeExpenses { get; set; }
        public string FileName { get; set; }

    }
}
