using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bridge.BusinessTier;
using Bridge.Models;
using Bridge.DataAccess;
using System.Data;

namespace Bridge.Repository
{
    public class MerchantProfileRepository : IDisposable, IMerchantProfile
    {
        public void Dispose()
        {
            // throw new NotImplementedException();
        }

        public IList<GeneralModel> RetrieveStatus(string statusType = "")
        {
            IList<GeneralModel> statusList = new DataAccess.DataAccess().ExecuteReader<GeneralModel>("avz_MRC_spRetrieveStatus", new { statusType = statusType == null ? string.Empty : statusType });

            return statusList;
        }

        #region Business
        public Dictionary<string, string> RetrieveMerchantBusinessInformation(int merchantId)
        {
            Dictionary<string, string> Info = new DataAccess.DataAccess().ExecuteReaderMultiple("avz_MRC_spretrieveMerchantBusinessInformation", new { merchantId = merchantId });

            return Info;
        }

        public IList<MPMerchantProcessorInfoModel> RetrieveMerchantBusinessProcessorInformation(int merchantId)
        {
            IList<MPMerchantProcessorInfoModel> Info = new DataAccess.DataAccess().ExecuteReader<MPMerchantProcessorInfoModel>("avz_MRC_spretrieveMerchantBusinessProcessorInformation", new { merchantId = merchantId });

            return Info;
        }
        public bool UpdateMerchantBusinessInformation(MPMerchantBusinessModel entity)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_MRC_spUpdateMerchantBusinessInformation", new
           {
               merchantId = entity.MerchantID,
               LegalName = entity.Business.nameOfCompany,
               BusinessName = entity.Business.nameOfBusiness,
               BusinessStatus = entity.Business.businessStatus,
               EntityTypeID = entity.Business.entityTypeID,
               RNCNumber = entity.Business.RNC,
               businessStartDate = entity.Business.businessStartDate,
               businessCCStartDate = entity.Business.businessCCStartDate,
               IndustryTypeID = entity.Business.industryTypeID,
               AffiliationNumber = entity.Business.affilliationNumber == null ? "" : entity.Business.affilliationNumber,
               PAddress = entity.PhysicalAddress.Address,
               PprovinceId = entity.PhysicalAddress.ProvinceID,
               PPhone1 = entity.PhysicalAddress.Phone1 == null ? "" : entity.PhysicalAddress.Phone1,
               PPhone2 = entity.PhysicalAddress.Phone2 == null ? "" : entity.PhysicalAddress.Phone2,
               Pemail = entity.PhysicalAddress.Email,
               PFax = entity.PhysicalAddress.Fax == null ? "" : entity.PhysicalAddress.Fax,
               LAddress = entity.LegalAddress.Address,
               LprovinceId = entity.LegalAddress.ProvinceID,
               LPhone1 = entity.LegalAddress.Phone1 == null ? "" : entity.LegalAddress.Phone1,
               LPhone2 = entity.LegalAddress.Phone2 == null ? "" : entity.LegalAddress.Phone2,
               Lemail = entity.LegalAddress.Email,
               LFax = entity.LegalAddress.Fax == null ? "" : entity.LegalAddress.Fax,
               LCity = entity.LegalAddress.City,
               UserId=entity.UserId
           });
        }
        public bool UpdateMerchantBusinessProcessorInformation(Int64 MerchantId, int Terminal, int processorTypeId, string processorNumber, DateTime dateGracePeriod, int industryTypeID, DateTime firstProcessedDate, int processorId)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_MRC_spUpdateMerchantBusinessProcessorInformation", new
           {
               terminal = Terminal,
               processorTypeId = processorTypeId,
               processorNumber = processorNumber,
               dateGracePeriod = dateGracePeriod,
               industryTypeID = industryTypeID,
               firstProcessedDate = firstProcessedDate,
               processorId = processorId,
               merchantId = MerchantId
           });
        }
        public bool UpdateMerchantBusinessProcessorInformation(MPMerchantProcessorInfoModel entity)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_MRC_spUpdateMerchantBusinessProcessorInformation", new
            {
                terminal = entity.Terminal,
                processorTypeId = entity.processorTypeId,
                processorNumber = entity.ProcessorNumber,
                dateGracePeriod = entity.DateGracePeriod,
                industryTypeID = entity.IndustryTypeID,
                firstProcessedDate = entity.FirstProcessedDate,
                processorId = entity.processorId
            });
        }
        #endregion

        #region Landlords
        public Dictionary<string, string> RetrieveMerchantLandlordInformation(int merchantId)
        {
            Dictionary<string, string> Info = new DataAccess.DataAccess().ExecuteReaderMultiple("avz_MRC_spretrieveMerchantLandlordInformation", new { merchantId = merchantId });

            return Info;
        }

        //public MPMerchantLandlordQuestionsModel RetrieveMerchantLandlordQuestionInformation(int merchantId)
        //{
        //    //IList<MPMerchantLandlordQuestionsModel> Info = new DataAccess.DataAccess().ExecuteReader<MPMerchantLandlordQuestionsModel>("avz_MRC_spretrieveMerchantLandlordQuestions", new { merchantId = merchantId });
        //    MPMerchantLandlordQuestionsModel Info = GetMerchantDetailsForVerification(merchantId);

        //    return Info;
        //}

        public MPMerchantLandlordQuestionsModel RetrieveMerchantLandlordQuestionInformation(int merchantId)
        {

            //IList<MPMerchantLandlordQuestionsModel> Info = new DataAccess.DataAccess().ExecuteReader<MPMerchantLandlordQuestionsModel>("avz_MRC_spretrieveMerchantLandlordQuestions", new { merchantId = merchantId });
            MPMerchantLandlordQuestionsModel Info = GetMerchantDetailsForVerification(merchantId);

            var t = new DataAccess.DataAccess().ExecuteReader<QuestionsModel, MPMerchantLandlordModel>("AVZ_MRC_spRetrieveLandlordVerificationCall",
               new
               {
                   MerchantID = merchantId,
                   Entity = "Landlord"
               });
            var vc = t.Item2.Count > 0 ? t.Item2[0] : new MPMerchantLandlordModel();
            Info.Questions = t.Item1;           


            return Info;
        }
        public bool UpdateMerchantLandlordInformation(MPMerchantLandlordModel entity)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_MRC_spUpdateMerchantLandlordInformation", new
            {
                merchantId = entity.MerchantID,
                TypeofPropertyId = entity.TypeofPropertyId,
                LandlordCompanyName = entity.LandlordCompanyName,
                LandlordFirstName = entity.landlordFirstName,
                LandlordLastName = entity.landlordLastName,
                RentedAmount = entity.MonthlyRentAmount,
                Address = entity.LandlordAddress.Address,
                City = entity.LandlordAddress.City,
                Phone = entity.LandlordAddress.Phone1,
                CellPhone = entity.LandlordAddress.Cell,
                Email = entity.LandlordAddress.Email,
                UserId=entity.UserId
            });
        }

        public DataSet GetMerchantDetailsForQuestion(Int64 merchantId)
        {
            DataSet dsData = new DataSet();
            dsData = new DataAccess.DataAccess().ExecuteDataSet("avz_sp_reterieveMerchantLandlord", new { merchantId = merchantId });
            return dsData;
        }

        public MPMerchantLandlordQuestionsModel GetMerchantDetailsForVerification(Int64 merchantId)
        {
            MPMerchantLandlordQuestionsModel details = new MPMerchantLandlordQuestionsModel();
            //List<UseOfAdvanceModel> AdvancesList = new List<UseOfAdvanceModel>();            
            DataSet dsData = new DataSet();
            dsData = GetMerchantDetailsForQuestion(merchantId);
            if (dsData.Tables.Count > 0)
            {
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    details.MerchantId = Convert.ToInt16(dsData.Tables[0].Rows[0]["MerchantId"]);
                    details.LegalName = Convert.ToString(dsData.Tables[0].Rows[0]["LegalName"]);
                    details.BusinessName = Convert.ToString(dsData.Tables[0].Rows[0]["BusinessName"]);
                    details.SalesRepName = Convert.ToString(dsData.Tables[0].Rows[0]["SalesRepName"]);

                    details.OwnerFirstName = Convert.ToString(dsData.Tables[0].Rows[0]["OwnerFirstName"]);
                    details.OwnerLastName = Convert.ToString(dsData.Tables[0].Rows[0]["OwnerLastName"]);

                    //details.OwnerName = Convert.ToString(dsData.Tables[0].Rows[0]["OwnerName"]);
                    if (string.IsNullOrEmpty(dsData.Tables[0].Rows[0]["retention"].ToString()) == false)
                        details.RetensionPct = Convert.ToInt16(dsData.Tables[0].Rows[0]["retention"]);
                    if (string.IsNullOrEmpty(dsData.Tables[0].Rows[0]["IsAuthorized"].ToString()) == false)
                        details.IsAuthorised = Convert.ToInt16(dsData.Tables[0].Rows[0]["IsAuthorized"]);
                    if (string.IsNullOrEmpty(dsData.Tables[0].Rows[0]["LoanedAmount"].ToString()) == false)
                        details.LoanAmount = Convert.ToDouble(dsData.Tables[0].Rows[0]["LoanedAmount"]);
                    if (string.IsNullOrEmpty(dsData.Tables[0].Rows[0]["ownedAmount"].ToString()) == false)
                        details.OwnedAmount = Convert.ToDouble(dsData.Tables[0].Rows[0]["ownedAmount"]);
                    details.OwnerPassport = Convert.ToString(dsData.Tables[0].Rows[0]["PassportNbr"]);
                    if (string.IsNullOrEmpty(dsData.Tables[0].Rows[0]["GrossAnnualSales"].ToString()) == false)
                        details.GrossAnnualSales = Convert.ToDouble(dsData.Tables[0].Rows[0]["GrossAnnualSales"]);
                    if (string.IsNullOrEmpty(dsData.Tables[0].Rows[0]["AdminExp"].ToString()) == false)
                        details.AdminExp = Convert.ToDouble(dsData.Tables[0].Rows[0]["AdminExp"]);
                    if (dsData.Tables[0].Rows[0]["BusinessStartDate"] != null)
                    {
                        if (dsData.Tables[0].Rows[0]["BusinessStartDate"].ToString() != "")
                            details.BusinessStartDate = Convert.ToDateTime(dsData.Tables[0].Rows[0]["BusinessStartDate"]);
                    }
                    if (string.IsNullOrEmpty(dsData.Tables[0].Rows[0]["TypeOfAdvanceId"].ToString()) == false)
                        details.TypeOfAdvanceId = Convert.ToInt64(dsData.Tables[0].Rows[0]["TypeOfAdvanceId"]);

                    if (dsData.Tables[0].Rows[0]["RentFlag"] != null)
                    {
                        long v = 0;
                        long.TryParse(dsData.Tables[0].Rows[0]["RentFlag"].ToString(), out v);
                        details.RentFlag = v;
                    }
                    if (dsData.Tables[0].Rows[0]["ContractStartDate"] != null)
                    {
                        if (dsData.Tables[0].Rows[0]["ContractStartDate"].ToString() != "")
                            details.ContractStartDate = Convert.ToDateTime(dsData.Tables[0].Rows[0]["ContractStartDate"]);
                    }
                    if (dsData.Tables[0].Rows[0]["ContractExpireDate"] != null)
                    {
                        if (dsData.Tables[0].Rows[0]["ContractExpireDate"].ToString() != "")
                            // details.ContractExpireDate = (dsData.Tables[0].Rows[0]["ContractExpireDate"]).ToString();
                            details.ContractExpireDate = Convert.ToDateTime(dsData.Tables[0].Rows[0]["ContractExpireDate"]);
                    }
                }


                if (dsData.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dRow in dsData.Tables[1].Rows)
                    {
                        details.MerchantAddress = Convert.ToString(dRow["MerchantAddress"]);
                        details.LandlordAddress = Convert.ToString(dRow["LandlordAddress"]);
                        details.RentedAmount = dRow.Field<double?>("RentedAmount");
                        //details.LandlordName = Convert.ToString(dRow["LandlordName"]);
                        details.LLFirstName = Convert.ToString(dRow["LLFirstName"]);
                        details.LLLastName = Convert.ToString(dRow["LLLastName"]);
                        details.LLCompany = Convert.ToString(dRow["LLCompany"]);
                    }
                }
            }

            return details;
        }
        #endregion

        #region Contacts
        public IList<MPMerchantContactsModel> RetrieveMerchantContacts(int merchantId)
        {
            return new DataAccess.DataAccess().ExecuteReader<MPMerchantContactsModel>("avz_MRC_spretrieveMerchantContactsInformation", new { merchantId = merchantId, ContactID = 0 });
        }

        public IList<MPMerchantContactsModel> RetrieveMerchantContact(int contactId)
        {
            return new DataAccess.DataAccess().ExecuteReader<MPMerchantContactsModel>("avz_MRC_spretrieveMerchantContactsInformation", new { merchantId = 0, ContactID = contactId });
        }

        public IList<MPMerchantAddressInfoModel> RetrieveMerchantContactAddress(int contactId)
        {
            return new DataAccess.DataAccess().ExecuteReader<MPMerchantAddressInfoModel>("avz_MRC_spretrieveMerchantContactAddressInformation", new { contactId = contactId });
        }

        public MPMerchantContactDetailModel RetrieveMerchantContactsInformation(int contactId)
        {
            MPMerchantContactDetailModel details = new MPMerchantContactDetailModel();
            details.Contact = RetrieveMerchantContact(contactId)[0];
            details.Address = RetrieveMerchantContactAddress(contactId)[0];
            return details;
        }
        public bool UpdateMerchantContactInformation(MPMerchantContactDetailModel entity)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_MRC_spUpdateMerchantContactInformation", new
            {
                contactId = entity.Contact.ContactId,
                Address = entity.Address.Address,
                City = entity.Address.City,
                Phone = entity.Address.Phone1,
                CellPhone = entity.Address.Cell,
                Email = entity.Address.Email,
                ProvinceID = entity.Address.ProvinceID,
                FirstName = entity.Contact.FirstName,
                LastName = entity.Contact.LastName,
                OwnerIdentification = entity.Contact.OwnerIdentification,
                DateofBirth = entity.Contact.DateofBirth,
                Position = entity.Contact.Position,
                UserId=entity.UserId
            });
        }
        public bool AddMerchantContactInformation(MPMerchantContactDetailModel entity)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_MRC_spAddMerchantContactInformation", new
            {
                merchantId = entity.MerchantID,
                Address = entity.Address.Address,
                City = entity.Address.City,
                Phone = entity.Address.Phone1,
                CellPhone = entity.Address.Cell,
                Email = entity.Address.Email,
                ProvinceID = entity.Address.ProvinceID,
                FirstName = entity.Contact.FirstName,
                LastName = entity.Contact.LastName,
                OwnerIdentification = entity.Contact.OwnerIdentification,
                DateofBirth = entity.Contact.DateofBirth,
                Position = entity.Contact.Position,
                UserId = entity.UserId
            });
        }
        public bool DeleteMerchantContacts(int contactId)
        {
            new DataAccess.DataAccess().ExecuteNonQuery("avz_MRC_spDeleteMerchantContactsInformation", new { contactId = contactId });
            return true;
        }
        #endregion

        #region Owners
        public IList<MPMerchantOwnersModel> RetrieveMerchantOwners(int merchantId)
        {
            return new DataAccess.DataAccess().ExecuteReader<MPMerchantOwnersModel>("avz_MRC_spretrieveMerchantOwnersInformation", new { merchantId = merchantId, OwnerID = 0 });
        }

        public IList<MPMerchantOwnersModel> RetrieveMerchantOwner(int OwnerId)
        {
            return new DataAccess.DataAccess().ExecuteReader<MPMerchantOwnersModel>("avz_MRC_spretrieveMerchantOwnersInformation", new { merchantId = 0, OwnerID = OwnerId });
        }

        public IList<MPMerchantAddressInfoModel> RetrieveMerchantOwnerAddress(int OwnerId)
        {
            return new DataAccess.DataAccess().ExecuteReader<MPMerchantAddressInfoModel>("avz_MRC_spretrieveMerchantOwnerAddressInformation", new { OwnerId = OwnerId });
        }

        public MPMerchantOwnerDetailModel RetrieveMerchantOwnersInformation(int OwnerId)
        {
            MPMerchantOwnerDetailModel details = new MPMerchantOwnerDetailModel();
            details.Owner = RetrieveMerchantOwner(OwnerId)[0];
            details.Address = RetrieveMerchantOwnerAddress(OwnerId)[0];
            return details;
        }
        public bool UpdateMerchantOwnerInformation(MPMerchantOwnerDetailModel entity)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_MRC_spUpdateMerchantOwnerInformation", new
            {
                OwnerId = entity.Owner.OwnerId,
                Address = entity.Address.Address,
                City = entity.Address.City,
                Phone = entity.Address.Phone1,
                CellPhone = entity.Address.Cell,
                Email = entity.Address.Email,
                ProvinceID = entity.Address.ProvinceID,
                FirstName = entity.Owner.FirstName,
                LastName = entity.Owner.LastName,
                OwnerIdentification = entity.Owner.OwnerIdentification,
                DateofBirth = entity.Owner.DateofBirth,
                DateBecameOwner = entity.Owner.DateBecameOwner,
                IsAuthorized = entity.Owner.IsAuthorized,
                UserId=entity.UserId
            });
        }
        public bool AddMerchantOwnerInformation(MPMerchantOwnerDetailModel entity)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_MRC_spAddMerchantOwnerInformation", new
            {
                merchantId = entity.MerchantID,
                Address = entity.Address.Address,
                City = entity.Address.City,
                Phone = entity.Address.Phone1,
                CellPhone = entity.Address.Cell,
                Email = entity.Address.Email,
                ProvinceID = entity.Address.ProvinceID,
                FirstName = entity.Owner.FirstName,
                LastName = entity.Owner.LastName,
                OwnerIdentification = entity.Owner.OwnerIdentification,
                DateofBirth = entity.Owner.DateofBirth,
                DateBecameOwner = entity.Owner.DateBecameOwner,
                IsAuthorized = entity.Owner.IsAuthorized,
                UserId = entity.UserId
            });
        }
        public bool DeleteMerchantOwners(int OwnerID)
        {
            new DataAccess.DataAccess().ExecuteNonQuery("avz_MRC_spDeleteMerchantOwnersInformation", new { OwnerID = OwnerID });
            return true;
        }
        #endregion

        #region Profiles
        public IList<MPMerchantProfileDetailModel> RetrieveMerchantProfiles(int merchantId, int ProcessorId, string ProcessorNumber)
        {
            return new DataAccess.DataAccess().ExecuteReader<MPMerchantProfileDetailModel>("avz_MRC_spretrieveMerchantProfilesInformation", new { merchantId = merchantId, ActivityID = 0, ProcessorId = ProcessorId, ProcessorNumber = ProcessorNumber });
        }
        public IList<MPMerchantProfilesModel> RetrieveMerchantProfileDetails(int merchantId, int ProcessorId, string ProcessorNumber)
        {
            return new DataAccess.DataAccess().ExecuteReader<MPMerchantProfilesModel>("avz_MRC_spretrieveMerchantProfileDetailsInformation", new { merchantId = merchantId, ActivityID = 0, ProcessorId = ProcessorId, ProcessorNumber = ProcessorNumber });
        }
        public IList<MPMerchantProfilesModel> RetrieveMerchantProfileDetails(int ActivityId)
        {
            return new DataAccess.DataAccess().ExecuteReader<MPMerchantProfilesModel>("avz_MRC_spretrieveMerchantProfileDetailsInformation", new { merchantId = 0, ActivityID = ActivityId, ProcessorId = 0, ProcessorNumber = "All" });
        }
        public bool UpdateMerchantProfileInformation(MPMerchantProfileDetailModel entity)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_MRC_spUpdateMerchantProfileInformation", new
            {
                activityId = entity.ProfileDetail[0].ActivityID,
                month = entity.ProfileDetail[0].Month,
                year = entity.ProfileDetail[0].Year,
                processorTypeId = entity.ProfileDetail[0].ProcessorId,
                amount = entity.ProfileDetail[0].Amount,
                tickets = entity.ProfileDetail[0].Tickets
            });
        }
        #endregion

        #region Documents
        public IList<MPMerchantDocumentModel> RetrieveMerchantDocuments(int merchantId, int documentTypeId)
        {
            return new DataAccess.DataAccess().ExecuteReader<MPMerchantDocumentModel>("avz_MRC_spretrieveMerchantDocumentsInformation", new { merchantId = merchantId, documentTypeId = documentTypeId });
        }
        public bool DeleteMerchantDocuments(int ID)
        {
            new DataAccess.DataAccess().ExecuteNonQuery("avz_MRC_spDeleteMerchantDocumentsInformation", new { documentId = ID });
            return true;
        }
        #endregion

        #region Activities
        public IList<MPMerchantActivityModel> RetrieveMerchanActivitiesMonthly(int merchantId, int ProcessorTypeId, string ProcessorNumber, DateTime ActivityStartDate, DateTime ActivityEndDate)
        {
            return new DataAccess.DataAccess().ExecuteReader<MPMerchantActivityModel>("avz_MRC_spretrieveMerchantActivitiesGroupedInformation", new { merchantId = merchantId, ProcessorTypeId = ProcessorTypeId, ProcessorNumber = ProcessorNumber, ActivityStartDate = ActivityStartDate, ActivityEndDate = ActivityEndDate });
        }
        public IList<MPMerchantActivityModel> RetrieveMerchantActivities(int merchantId, int ProcessorTypeId, string MonthName)
        {
            return new DataAccess.DataAccess().ExecuteReader<MPMerchantActivityModel>("avz_MRC_spretrieveMerchantActivitiesInformation", new { merchantId = merchantId, ProcessorTypeId = ProcessorTypeId, Month = MonthName.Split('/')[0], year = MonthName.Split('/')[1] });
        }

        #endregion

        #region Statements
        public IList<MPMerchantStatementModel> RetrieveMerchantStatements(int merchantId, DateTime StatementFrom, DateTime StatementTo)
        {
            return new DataAccess.DataAccess().ExecuteReader<MPMerchantStatementModel>("avz_MRC_spretrieveMerchantStatementsInformation", new { merchantId = merchantId, StatementFrom = StatementFrom, StatementTo = StatementTo });
            //return new List<MPMerchantStatementModel>() { new MPMerchantStatementModel() { PaymentsReceived = 0.00, ProcessorName = "Visa Net", RetentionPercentage = 10, TotalTransaction = 10, TransactionDate = System.DateTime.Today }, new MPMerchantStatementModel() { PaymentsReceived = 0.00, ProcessorName = "Visa Net", RetentionPercentage = 10, TotalTransaction = 10, TransactionDate = System.DateTime.Today } };
        }
        public IList<MPMerchantStatementsDetailModel> RetrieveMerchantStatementsSummary(int merchantId, DateTime StatementFrom, DateTime StatementTo)
        {
            List<MPMerchantStatementsDetailModel> Details = new DataAccess.DataAccess().ExecuteReader<MPMerchantStatementsDetailModel>("avz_MRC_spretrieveMerchantStatementSummaryInformation", new { merchantId = merchantId, StatementFrom = StatementFrom, StatementTo = StatementTo }).ToList();
            Details[0].AddressDetail = new DataAccess.DataAccess().ExecuteReader<MPMerchantAddressInfoModel>("avz_MRC_spretrieveMerchantStatementSummaryInformation", new { merchantId = merchantId, StatementFrom = StatementFrom, StatementTo = StatementTo }).FirstOrDefault();
            return Details;
        }

        #endregion

        #region History
        public IList<MPMerchantHistoryModel> RetrieveMerchantHistory(int merchantId, DateTime HistoryStartDate, DateTime HistoryEndDate)
        {
            return new DataAccess.DataAccess().ExecuteReader<MPMerchantHistoryModel>("avz_MRC_spretrieveMerchantHistoryInformation", new { merchantId = merchantId, HistoryStartDate = HistoryStartDate, HistoryEndDate = HistoryEndDate });
        }

        #endregion

        #region Risk Evaluation
        public IList<MPMerchantRiskEvaluationModel> RetrieveMerchantRiskEvaluation(int merchantId, DateTime StartDate, DateTime EndDate)
        {
            return new DataAccess.DataAccess().ExecuteReader<MPMerchantRiskEvaluationModel>("avz_MRC_spretrieveMerchantRiskEvaluationInformation", new { merchantId = merchantId, StartDate = StartDate, EndDate = EndDate });
        }

        #endregion

        #region Contracts
        public IList<MPMerchantContractModel> RetrieveMerchantContracts(int merchantId, DateTime StartDate, DateTime EndDate)
        {
            return new DataAccess.DataAccess().ExecuteReader<MPMerchantContractModel>("avz_MRC_spretrieveMerchantContractInformation", new { merchantId = merchantId, StartDate = StartDate, EndDate = EndDate });
        }
        public MPMerchantContractModel RetrieveContractGeneralInformation(int contractId)
        {
            return new DataAccess.DataAccess().ExecuteReader<MPMerchantContractModel>("avz_MRC_spretrieveMerchantContractGeneralInformation", new { contractId = contractId }).FirstOrDefault();
        }
        public bool UpdateContractGeneralInformation(MPMerchantContractModel entity)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_MRC_spUpdateMerchantContractGeneralInformation", new
            {
                contractId = entity.ContractId,
                RetentionPercentage = entity.RetentionPercentage,
                RetentionChangeReason = entity.RetentionChangeReason,
                UserId=entity.UserId
            });
        }
        public IList<MPMerchantHistoryModel> RetrieveContractHistory(int contractId, DateTime HistoryStartDate, DateTime HistoryEndDate)
        {
            return new DataAccess.DataAccess().ExecuteReader<MPMerchantHistoryModel>("avz_MRC_spretrieveMerchantContractHistoryInformation", new { contractId = contractId, HistoryStartDate = HistoryStartDate, HistoryEndDate = HistoryEndDate });
        }
        public IList<MPMerchantActivityModel> RetrieveContractActivities(int contractId, int ProcessorTypeId, string ProcessorNumber, DateTime ActivityStartDate, DateTime ActivityEndDate)
        {
            return new DataAccess.DataAccess().ExecuteReader<MPMerchantActivityModel>("avz_MRC_spretrieveMerchantContractActivitiesInformation", new { contractId = contractId, ProcessorTypeId = ProcessorTypeId, ProcessorNumber = ProcessorNumber, ActivityStartDate = ActivityStartDate, ActivityEndDate = ActivityEndDate });
        }
        public IList<MPMerchantContractSalesRepresentativeModel> RetrieveContractSalesRepresentative(int contractId)
        {
            return new DataAccess.DataAccess().ExecuteReader<MPMerchantContractSalesRepresentativeModel>("avz_MRC_spretrieveMerchantSalesRepsInformation", new { contractId = contractId, salesRepID=0 });
        }
        public MPMerchantContractSalesRepresentativeModel RetrieveContractSalesRepresentativeDetail(int salesRepID)
        {
            return new DataAccess.DataAccess().ExecuteReader<MPMerchantContractSalesRepresentativeModel>("avz_MRC_spretrieveMerchantSalesRepsInformation", new { contractId = 0, salesRepID = salesRepID }).FirstOrDefault();
        }
        public bool UpdateContractSalesRepresentative(MPMerchantContractSalesRepresentativeModel entity)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_MRC_spUpdateMerchantContractSalesRep", new
            {
                contractId = entity.ContractId,
                SalesRepId = entity.SalesRepId,
                IsPrimarySalesRep = entity.IsPrimary,
                UserId = entity.UserId
            });
        }
        public bool AddContractSalesRepresentative(MPMerchantContractSalesRepresentativeModel entity)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_MRC_spAddMerchantContractSalesRep", new
            {
                contractId = entity.ContractId,
                SalesRepId = entity.SalesRepId,
                IsPrimarySalesRep = entity.IsPrimary,
                UserId=entity.UserId
            });
        }

        #endregion

        #region Collection
        public IList<MPMerchantCollectionModel> RetrieveMerchantCollection(int merchantId, DateTime StartDate, DateTime EndDate)
        {
            return new DataAccess.DataAccess().ExecuteReader<MPMerchantCollectionModel>("avz_MRC_spretrieveMerchantCollectionInformation", new { merchantId = merchantId, StartDate = StartDate, EndDate = EndDate });
        }
        #endregion

        #region Affiliation
        public MPMerchantAffiliationDetailModel RetrieveMerchanAffiliations(int merchantId, string RequestType)
        {
            var T = new DataAccess.DataAccess().ExecuteReader<MPMerchantAffiliationModel, MPMerchantAffiliationModel>("avz_MRC_spretrieveMerchantAffiliationInformation", new { merchantId = merchantId, RequestType = RequestType });
            MPMerchantAffiliationDetailModel returnValue = new MPMerchantAffiliationDetailModel();
            returnValue.ActiveAffiliations = T.Item1;
            returnValue.InActiveAffiliations = T.Item2;
            return returnValue;
        }
        #endregion

        #region Credit Report

        public IList<GeneralModel> RetriveMerchantContracts(int merchantId)
        {
            IList<GeneralModel> statusList = new DataAccess.DataAccess().ExecuteReader<GeneralModel>("avz_MRC_spRetrieveAllMerchantContracts", new { merchantId = merchantId });

            return statusList;
        }

        public IList<GeneralModel> RetriveMerchantContractCreditReports(int contractId)
        {
            IList<GeneralModel> statusList = new DataAccess.DataAccess().ExecuteReader<GeneralModel>("avz_MRC_spRetrieveAllMerchantCreditReports", new { contractId = contractId });

            return statusList;
        }
        public IList<MPMerchantCreditReportModel> RetrieveMerchantCreditReport(int reportId)
        {
            return new DataAccess.DataAccess().ExecuteReader<MPMerchantCreditReportModel>("avz_MRC_spretrieveMerchantCreditReportInformation", new { reportId = reportId });
        }

        public IList<MPMerchantCreditReportModel> RetrieveMerchantCreditReportInformation(int contractId)
        {
            return new DataAccess.DataAccess().ExecuteReader<MPMerchantCreditReportModel>("avz_MRC_spretrieveMerchantCreditReportInformation", new { merchantId = 0, ContractID = contractId });
        }

        #endregion
    }
}