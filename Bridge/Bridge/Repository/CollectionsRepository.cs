using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bridge.Models;
using Bridge.Repository;
using Bridge.BusinessTier;
using System.Data;

namespace Bridge.Repository
{
    public class CollectionsRepository:ICollections,IDisposable
    {
        #region Collections

        #region Prevention and Collections
        /// <summary>
        /// For Prevention
        /// </summary>
        /// <param name="SlowPercentage"></param>
        /// <returns></returns>
        public IList<PreventionModel> RetrivePrevention(decimal? SlowPercentage, Int64 AssignedUserId)
        {
            return new DataAccess.DataAccess().ExecuteReader<PreventionModel>("AVZ_col_spRetrieveCollectionForPrevention", new { SlowPercentage = SlowPercentage, AssignedUserId = AssignedUserId });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strConfig"></param>
        /// <param name="strConfigSystem"></param>
        /// <returns></returns>
        public IList<DropDown> ListValuesForDays(string strConfig, string strConfigSystem)
        {
            return new DataAccess.DataAccess().ExecuteReader<DropDown>("AVZ_GEN_spRetrieveConfigurationValues", new { Config = strConfig, ConfigSystem = strConfigSystem });
        }

        /// <summary>
        /// For Inactivity days in Collections
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public IList<CollectionDaysModel> ListCollectionbyInactiveDays(int days, Int64 AssignedUserId)
        {
            return new DataAccess.DataAccess().ExecuteReader<CollectionDaysModel>("AVZ_col_spRetrieveCollectionDays", new { Days = days, AssignedUserId = AssignedUserId });
        }

        #endregion

        #region Collection Workflow
        /// <summary>
        /// TO retrieve Contract Info on Collection screen
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public IList<CollectionContractInformation> RetrieveContractInfo(long merchantId, long contractId)
        {
            return new DataAccess.DataAccess().ExecuteReader<CollectionContractInformation>("AVZ_COL_spRetrieveContractInfo", new { merchantId = merchantId, contractId = contractId });
        }

        /// <summary>
        /// Retrieve Owner Info
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public IList<OwnerColModel> ListOwnerInfo(long merchantId, long contractId)
        {
            return new DataAccess.DataAccess().ExecuteReader<OwnerColModel>("AVZ_COL_spRetrieveOneAuthorizedOwners", new { merchantId = merchantId, contractId = contractId });
        }

        /// <summary>
        /// TO return check while activity notes
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public IList<CollectionActivities> ColActivityCheck(long merchantId, long contractId)
        {
            return new DataAccess.DataAccess().ExecuteReader<CollectionActivities>("AVZ_col_spActivityCheck", new { merchantId = merchantId, contractId = contractId });
        }

        #endregion

        #region Affiliations
        /// <summary>
        /// To retrieve Active Contract affiliations
        /// </summary>
        /// <param name="IsRNC"></param>
        /// <returns></returns>
        public IList<ActiveContracts> ListActiveContractAffiliations(Int64 merchantId, bool IsRNC)
        {
            return new DataAccess.DataAccess().ExecuteReader<ActiveContracts>("AVZ_col_spRetrieveAffiliation_ActiveContracts", new { MerchantId=merchantId, isRNC = IsRNC });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IsRNC"></param>
        /// <returns></returns>
        public IList<InActiveContracts> ListAffiliationsNonActiveContracts(Int64 merchantId, bool IsRNC)
        {
            return new DataAccess.DataAccess().ExecuteReader<InActiveContracts>("AVZ_COL_spRetrieveNonActiveContracts_Affiliation", new { MerchantId = merchantId, isRNC = IsRNC });
        }

        #endregion

        #region Documents

        public bool MrcDeleteDocuments(string DocumentIds)
        {
            if (new DataAccess.DataAccess().ExecuteNonQuery("AVZ_COL_spDeleteDocuments", new { DocumentIds = DocumentIds }))
                return true;
            else
                return false;
        }

        public IList<DocumentsModel> ListDocuments(Int64 merchantId, Int64 contractId, int documentTypeId)
        {
            DocumentsModel model = new DocumentsModel();
            return new DataAccess.DataAccess().ExecuteReader<DocumentsModel>("AVZ_DOC_spListDocs", new { MerchantID = merchantId, ContractId = contractId, DocumentTypeId = documentTypeId });
        }
        public IList<DocumentsModel> ListDocuments(int documentId)
        {
            DocumentsModel model = new DocumentsModel();
            return new DataAccess.DataAccess().ExecuteReader<DocumentsModel>("AVZ_COL_spPreviewDocument", new { DocumentId = documentId });
        }
        #endregion

        #region Landlord

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public IList<CollectionLandlordModel> ListLandLordDetails(Int64 merchantId)
        {
            return new DataAccess.DataAccess().ExecuteReader<CollectionLandlordModel>("AVZ_COL_spRetrieveLandLordDetails", new { MerchantId = merchantId});
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateMerchantLandlordInformation(CollectionLandlordModel entity)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_MRC_spUpdateMerchantLandlordInformation", new
            {
                merchantId = entity.merchantId,
                TypeofPropertyId = entity.typeOfPropertyId,
                LandlordCompanyName = entity.landlordCompanyName,
                LandlordFirstName = entity.landlordFirstName,
                LandlordLastName = entity.landlordLastName,
                RentedAmount = entity.monthlyRentAmount,
                Address = entity.address,
                City = entity.city,
                Phone = entity.telePhone,
                CellPhone = entity.cellPhone,
                Email = entity.email,
                UserId= entity.userId
            });
        }

        #endregion

        #region Legal

        /// <summary>
        /// Retrive Lawyer details
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public IList<LawyerModel> RetrieveAssignedLawyers(Int64 merchantId, Int64 contractId)
        {
            return new DataAccess.DataAccess().ExecuteReader<LawyerModel>("AVZ_COL_spRetrieveAssignedLawyers", new { MerchantId = merchantId, ContractId = contractId });
        }

        /// <summary>
        /// Retrive Lawyer details
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public IList<LawyerModel> RetrieveAllLawyersToAssign(Int64 merchantId, Int64 contractId)
        {
            return new DataAccess.DataAccess().ExecuteReader<LawyerModel>("AVZ_COL_spRetrieveLawyers");
        }

        public IList<LegalDocuments> RetrieveLegalDocuments(Int64 merchantId, Int64 contractId)
        {
            return new DataAccess.DataAccess().ExecuteReader<LegalDocuments>("AVZ_COL_spRetrieveLegalDocuments", new { MerchantId = merchantId, ContractId = contractId });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AssignLawyersToCollection(LawyerModel model)
        {
            if (new DataAccess.DataAccess().ExecuteNonQuery("AVZ_COL_spAssignLawyers", new { MerchantId = model.merchantId, ContractId = model.contractId, LawyerId = model.lawyerId, DocumentIds = model.documentType, InsertUserId = model.insertUserId }))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Retrive Lawyer details
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public IList<LawyerModel> RetrieveAllLawyers()
        {
            return new DataAccess.DataAccess().ExecuteReader<LawyerModel>("AVZ_COL_spRetrieveLawyers");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strLawyersXml"></param>
        /// <returns></returns>
        public bool InsUpdateLawyers(string strLawyersXml, string strDeleteLawyers)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("AVZ_COL_spAddUpdateLawyers", new { LawyersXml = strLawyersXml, DeletedLawyers = strDeleteLawyers });
        }

        #endregion

        #region Payment Agreement
        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public IList<CollectionPaymentAgreementModel> RetrievePaymentDetails(Int64 merchantId, Int64 contractId)
        {
            return new DataAccess.DataAccess().ExecuteReader<CollectionPaymentAgreementModel>("AVZ_COL_spRetrievePaymentDetails", new { MerchantId = merchantId, ContractId = contractId });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool InsertPaymentAgreement(CollectionPaymentAgreementModel model)
        {
            if (new DataAccess.DataAccess().ExecuteNonQuery("AVZ_COL_spInsUpdatePaymentAgreement", new 
            { 
                MerchantId = model.merchantId, 
                ContractId = model.contractId, 
                DateOfAgreement = model.dateOfAgreement, 
                StartDate = model.startDate, 
                EndDate = model.endDate, 
                IntervalofDays = model.intervalofDays, 
                InsertUserId=model.insertUserId 
            }))
                return true;
            else
                return false;
        }

        #endregion

        #region Generate Letters

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public IList<GenerateLetter> GetCollectionLetter(Int64 merchantId, Int64 contractId)
        {
            return new DataAccess.DataAccess().ExecuteReader<GenerateLetter>("AVZ_COL_spGenerateCollectionLetter", new { MerchantId = merchantId, ContractId = contractId });
        }
        #endregion

        #region Collection Activities
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<DropDown> ListCollectionActivities()
        {
            return new DataAccess.DataAccess().ExecuteReader<DropDown>("avz_col_spRetrieveActivityTypes");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantid"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool InsertCollectionActivity(CollectionActivityModel model)
        {
            if (new DataAccess.DataAccess().ExecuteNonQuery("avz_col_spInsertCollectionActivity", new
           {
               ActivityTypeId = model.activityTypeId,
               Notes = model.notes,
               MerchantId = model.merchantId,
               ContractId = model.contractId,
               InsertUserId = model.insertUserId
           }))
                return true;
            else
                return false;
        }
        #endregion

        #region Base Methods
        public bool Create(CollectionsModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(CollectionsModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion
    }
}