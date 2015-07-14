using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bridge.Repository;
using Bridge.BusinessTier;
using Bridge.Models;
using System.Data;

namespace Bridge.BusinessTier
{
   public interface ICollections:IRepository<CollectionsModel, int>
   {
       #region Prevention and Collections
       /// <summary>
       /// For Prevention
       /// </summary>
       /// <param name="percentage"></param>
       /// <returns></returns>
       IList<PreventionModel> RetrivePrevention(decimal? percentage, Int64 AssignedUserId);

       /// <summary>
       /// 
       /// </summary>
       /// <param name="strConfig"></param>
       /// <param name="strConfigSystem"></param>
       /// <returns></returns>
       IList<DropDown> ListValuesForDays(string strConfig, string strConfigSystem);

       /// <summary>
       /// For Inactive days
       /// </summary>
       /// <param name="days"></param>
       /// <returns></returns>
       IList<CollectionDaysModel> ListCollectionbyInactiveDays(int days, Int64 AssignedUserId);

       #endregion

       #region Collection Workflow
       /// <summary>
       /// For Contract Information
       /// </summary>
       /// <param name="merchantId"></param>
       /// <param name="contractID"></param>
       /// <returns></returns>
       IList<CollectionContractInformation> RetrieveContractInfo(long merchantId, long contractID);

       /// <summary>
       /// Retrieve Owner Info
       /// </summary>
       /// <param name="merchantId"></param>
       /// <param name="contractId"></param>
       /// <returns></returns>
       IList<OwnerColModel> ListOwnerInfo(long merchantId, long contractId);

       /// <summary>
       /// TO return check while activity notes
       /// </summary>
       /// <param name="merchantId"></param>
       /// <param name="contractId"></param>
       /// <returns></returns>
       IList<CollectionActivities> ColActivityCheck(long merchantId, long contractId);
       #endregion

       #region Affiliations
       /// <summary>
       /// 
       /// </summary>
       /// <param name="isRNC"></param>
       /// <returns></returns>
       IList<ActiveContracts> ListActiveContractAffiliations(Int64 merchantId, bool isRNC); 
       /// <summary>
       /// 
       /// </summary>
       /// <param name="merchantId"></param>
       /// <param name="IsRNC"></param>
       /// <returns></returns>
       IList<InActiveContracts> ListAffiliationsNonActiveContracts(Int64 merchantId, bool IsRNC);
       #endregion

       #region Dcouments
       /// <summary>
       /// 
       /// </summary>
       /// <param name="DocumentIds"></param>
       /// <returns></returns>
       bool MrcDeleteDocuments(string DocumentIds);

       IList<DocumentsModel> ListDocuments(Int64 merchantId, Int64 contractId, int documentTypeId);

       IList<DocumentsModel> ListDocuments(int documentId);
       #endregion

       #region Landlord

       IList<CollectionLandlordModel> ListLandLordDetails(Int64 merchantId);
       bool UpdateMerchantLandlordInformation(CollectionLandlordModel entity);

       #endregion

       #region Legal

       /// <summary>
       /// To retrieve assigned lawyers
       /// </summary>
       /// <param name="merchantId"></param>
       /// <param name="contractId"></param>
       /// <returns></returns>
       IList<LawyerModel> RetrieveAssignedLawyers(Int64 merchantId, Int64 contractId);

       /// <summary>
       /// 
       /// </summary>
       /// <param name="merchantId"></param>
       /// <param name="contractId"></param>
       /// <returns></returns>
       IList<LawyerModel> RetrieveAllLawyersToAssign(Int64 merchantId, Int64 contractId);

       /// <summary>
       /// 
       /// </summary>
       /// <param name="merchantId"></param>
       /// <param name="contractId"></param>
       /// <returns></returns>
       IList<LegalDocuments> RetrieveLegalDocuments(Int64 merchantId, Int64 contractId);


       ///// <summary>
       ///// To retrieve all lawyers
       ///// </summary>
       ///// <param name="merchantId"></param>
       ///// <param name="contractId"></param>
       ///// <returns></returns>
       //DataSet RetrieveAllLawyersToAssign(Int64 merchantId, Int64 contractId);

       /// <summary>
       /// 
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       bool AssignLawyersToCollection(LawyerModel model);

       IList<LawyerModel> RetrieveAllLawyers();

       bool InsUpdateLawyers(string strLawyersXml, string strDeleteLawyers);

       #endregion

       #region Payment Agreement

       /// <summary>
       /// 
       /// </summary>
       /// <param name="merchantId"></param>
       /// <param name="contractId"></param>
       /// <returns></returns>
       IList<CollectionPaymentAgreementModel> RetrievePaymentDetails(Int64 merchantId, Int64 contractId);
       /// <summary>
       /// 
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       bool InsertPaymentAgreement(CollectionPaymentAgreementModel model);

       #endregion

       #region Generate Letter
       IList<GenerateLetter> GetCollectionLetter(Int64 merchantId, Int64 contractId);       
       
       #endregion      

       #region Collection Activities
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
       IList<DropDown> ListCollectionActivities();

       /// <summary>
       /// 
       /// </summary>
       /// <param name="merchantid"></param>
       /// <param name="model"></param>
       /// <returns></returns>
       bool InsertCollectionActivity(CollectionActivityModel model);
       #endregion
   }
}
