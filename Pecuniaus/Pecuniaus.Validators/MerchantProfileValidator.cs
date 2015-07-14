using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Pecuniaus.MerchantProfile.Models;
using Pecuniaus.Resources.MerchantProfile;
using Pecuniaus.Utilities.Validation;

namespace Pecuniaus.Validators
{
    public class MPMerchantBusinessInfoValidator : AbstractValidator<MPMerchantBusinessInfoModel>
    {
        public MPMerchantBusinessInfoValidator()
        {
            RuleFor(m => m.nameOfCompany)
                .NotEmpty().WithMessage(ValidationMessages.NameofCompanyReq);
            RuleFor(m => m.nameOfBusiness)
                .NotEmpty().WithMessage(ValidationMessages.NameofBusinessReq);
            RuleFor(m => m.businessStatus)
                .NotEmpty().WithMessage(ValidationMessages.BusinessStatusReq);
            RuleFor(m => m.RNC)
                .NotEmpty().WithMessage(ValidationMessages.RNCReq)
                .Matches(ValidationRules.RNC).WithMessage(ValidationMessages.RNCVal);
            RuleFor(m => m.businessStartDate)
                .NotNull().WithMessage(ValidationMessages.DateBusinessStartedReq)
                .LessThanOrEqualTo(DateTime.Today).WithMessage(ValidationMessages.DateBusinessStartedVal);
            RuleFor(m => m.businessCCStartDate)
                .NotNull().WithMessage(ValidationMessages.DateBusinessStartedReq)
                .LessThanOrEqualTo(DateTime.Today).WithMessage(ValidationMessages.DateBusinessStartedCCVal);
        }
    }
    public class MPMerchantAddressInfoValidator : AbstractValidator<MPMerchantAddressInfoModel>
    {
        public MPMerchantAddressInfoValidator()
        {
            RuleFor(m => m.Address)
                .NotEmpty().WithMessage(ValidationMessages.AddressReq);
            RuleFor(m => m.Cell)
                .NotEmpty().WithMessage(ValidationMessages.CellphoneReq)
                .Matches(ValidationRules.Telephone).WithMessage(ValidationMessages.CellphoneVal);
            RuleFor(m => m.City)
                .NotEmpty().WithMessage(ValidationMessages.CityReq);
            RuleFor(m => m.Email)
                .NotEmpty().WithMessage(ValidationMessages.EmailReq)
                .EmailAddress().WithMessage(ValidationMessages.EmailVal);
            RuleFor(m => m.Phone1)
                .NotEmpty().WithMessage(ValidationMessages.Telephone1Req)
                .Matches(ValidationRules.Telephone).WithMessage(ValidationMessages.Telephone1Val);
        }
    }
    public class MPMerchantLandlordValidator : AbstractValidator<MPMerchantLandlordModel>
    {
        public MPMerchantLandlordValidator()
        {
            RuleFor(m => m.LandlordCompanyName)
                .NotEmpty().WithMessage(ValidationMessages.LandlordCompanyNameReq);
            RuleFor(m => m.LandlordName)
                .NotEmpty().WithMessage(ValidationMessages.LandlordNameReq);
            RuleFor(m => m.MonthlyRentAmount)
                .NotNull().WithMessage(ValidationMessages.MonthlyRentAmountReq)
                .GreaterThan(0).WithMessage(ValidationMessages.MonthlyRentAmountGreaterThanZero);
        }
    }
    public class MPMerchantContactsValidator : AbstractValidator<MPMerchantContactsModel>
    {
        public MPMerchantContactsValidator()
        {
            RuleFor(m => m.FirstName)
                .NotEmpty().WithMessage(ValidationMessages.FirstNameReq);
            RuleFor(m => m.LastName)
                .NotEmpty().WithMessage(ValidationMessages.LastNameReq);
            RuleFor(m => m.OwnerIdentification)
                .NotEmpty().WithMessage(ValidationMessages.IdentificationReq);
            RuleFor(m => m.DateofBirth)
                .NotNull().WithMessage(ValidationMessages.DOBReq)
                .LessThanOrEqualTo(DateTime.Today).WithMessage(ValidationMessages.DOBVal);
            RuleFor(m => m.Position)
                .NotEmpty().WithMessage(ValidationMessages.PositionReq);
        }
        //private bool BeAValidDate(DateTime? value)
        //{
        //    DateTime date;
        //    return DateTime.TryParse(value.ToString(), out date);
        //}
    }
    public class MPMerchantOwnersValidator : AbstractValidator<MPMerchantOwnersModel>
    {
        public MPMerchantOwnersValidator()
        {
            RuleFor(m => m.FirstName)
                .NotEmpty().WithMessage(ValidationMessages.FirstNameReq);
            RuleFor(m => m.LastName)
                .NotEmpty().WithMessage(ValidationMessages.LastNameReq);
            RuleFor(m => m.OwnerIdentification)
                .NotEmpty().WithMessage(ValidationMessages.IdentificationReq);
            RuleFor(m => m.DateofBirth)
                .NotNull().WithMessage(ValidationMessages.DOBReq)
                .LessThanOrEqualTo(DateTime.Today).WithMessage(ValidationMessages.DOBVal);
            RuleFor(m => m.DateBecameOwner)
                .NotNull().WithMessage(ValidationMessages.DateBecameOwnerReq)
                .LessThanOrEqualTo(DateTime.Today).WithMessage(ValidationMessages.DateBecameOwnerVal);
        }
    }
    public class MPMerchantActivityValidator : AbstractValidator<MPMerchantActivityDetailModel>
    {
        public MPMerchantActivityValidator()
        {
            RuleFor(m => m.ActivityFrom)
                .LessThanOrEqualTo(DateTime.Today).WithMessage(ValidationMessages.DateBecameOwnerVal).When(m => m.ActivityFrom.HasValue);

            RuleFor(m => m.ActivityTo)
            .NotEmpty().WithMessage(ValidationMessages.ToDateReq)
            .GreaterThan(m => m.ActivityFrom).WithMessage(ValidationMessages.ToDateVal).When(m => m.ActivityFrom.HasValue);
        }
    }
    public class MPMerchantStatementsValidator : AbstractValidator<MPMerchantStatementsDetailModel>
    {
        public MPMerchantStatementsValidator()
        {
            RuleFor(m => m.StatementsFrom)
                .LessThanOrEqualTo(DateTime.Today).WithMessage(ValidationMessages.DateBecameOwnerVal).When(m => m.StatementsFrom.HasValue);

            RuleFor(m => m.StatementsTo)
            .NotEmpty().WithMessage(ValidationMessages.ToDateReq)
            .GreaterThan(m => m.StatementsFrom).WithMessage(ValidationMessages.ToDateVal).When(m => m.StatementsFrom.HasValue);
        }
    }
    public class MPMerchantHistoryValidator : AbstractValidator<MPMerchantHistoryDetailModel>
    {
        public MPMerchantHistoryValidator()
        {
            RuleFor(m => m.HistoryStartDate)
                .LessThanOrEqualTo(DateTime.Today).WithMessage(ValidationMessages.DateBecameOwnerVal).When(m => m.HistoryStartDate.HasValue);

            RuleFor(m => m.HistoryEndDate)
            .NotEmpty().WithMessage(ValidationMessages.ToDateReq)
            .GreaterThan(m => m.HistoryEndDate).WithMessage(ValidationMessages.ToDateVal).When(m => m.HistoryStartDate.HasValue);
        }
    }
    public class MPMerchantRiskEvaluationValidator : AbstractValidator<MPMerchantRiskEvaluationDetailModel>
    {
        public MPMerchantRiskEvaluationValidator()
        {
            RuleFor(m => m.StartDate)
                .LessThanOrEqualTo(DateTime.Today).WithMessage(ValidationMessages.DateBecameOwnerVal).When(m => m.StartDate.HasValue);

            RuleFor(m => m.EndDate)
            .NotEmpty().WithMessage(ValidationMessages.ToDateReq)
            .GreaterThan(m => m.EndDate).WithMessage(ValidationMessages.ToDateVal).When(m => m.StartDate.HasValue);
        }
    }
    public class MPMerchantContractDetailValidator : AbstractValidator<MPMerchantContractDetailModel>
    {
        public MPMerchantContractDetailValidator()
        {
            RuleFor(m => m.StartDate)
                .LessThanOrEqualTo(DateTime.Today).WithMessage(ValidationMessages.DateBecameOwnerVal).When(m => m.StartDate.HasValue);

            RuleFor(m => m.EndDate)
            .NotEmpty().WithMessage(ValidationMessages.ToDateReq)
            .GreaterThan(m => m.EndDate).WithMessage(ValidationMessages.ToDateVal).When(m => m.StartDate.HasValue);
        }
    }
    public class MPMerchantContractValidator : AbstractValidator<MPMerchantContractModel>
    {
        public MPMerchantContractValidator()
        {
            RuleFor(m => m.RetentionPercentage)
                .NotNull().WithMessage(ValidationMessages.RetentionPercentageReq);

            RuleFor(m => m.RetentionChangeReason)
                .NotNull().WithMessage(ValidationMessages.RetentionChangeReasonReq);
        }
    }
    public class MPMerchantCollectionValidator : AbstractValidator<MPMerchantCollectionDetailModel>
    {
        public MPMerchantCollectionValidator()
        {
            RuleFor(m => m.StartDate)
                .LessThanOrEqualTo(DateTime.Today).WithMessage(ValidationMessages.DateBecameOwnerVal).When(m => m.StartDate.HasValue);

            RuleFor(m => m.EndDate)
            .NotEmpty().WithMessage(ValidationMessages.ToDateReq)
            .GreaterThan(m => m.EndDate).WithMessage(ValidationMessages.ToDateVal).When(m => m.StartDate.HasValue);
        }
    }
}