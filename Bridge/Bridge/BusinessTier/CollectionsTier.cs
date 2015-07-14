using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Bridge.Models;
using Bridge.Repository;
using System.Xml;
using System.Text;
using Bridge.Utility;

namespace Bridge.BusinessTier
{
    public class CollectionsTier:IDisposable
    {
        #region Private Variables

        private ICollections collectionRepository;

        #endregion

        #region Contructors

        public CollectionsTier() : this(new CollectionsRepository()) { }
        public CollectionsTier(ICollections collectionRepository)
        {
            this.collectionRepository = collectionRepository;
        }

        #endregion

        #region Prevention and Collections        
        /// <summary>
        /// For Prevention
        /// </summary>
        /// <param name="percentage"></param>
        /// <returns></returns>
        public IList<PreventionModel> RetrivePrevention(decimal percentage, Int64 AssignedUserId)
        {
            return collectionRepository.RetrivePrevention(percentage, AssignedUserId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strConfig"></param>
        /// <param name="strConfigSystem"></param>
        /// <returns></returns>
        public IList<DropDown> ListValuesForDays(string strConfig, string strConfigSystem)
        {
            return collectionRepository.ListValuesForDays(strConfig, strConfigSystem);
        }

        /// <summary>
        /// For Inactive Days
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public IList<CollectionDaysModel> ListCollectionbyInactiveDays(int days, Int64 AssignedUserId)
        {
            return collectionRepository.ListCollectionbyInactiveDays(days, AssignedUserId);
        }
        #endregion

        #region Collection Workflow
        /// <summary>
        /// For Contract Information
        /// </summary>
        /// <param name="merchantID"></param>
        /// <param name="contractID"></param>
        /// <returns></returns>
        public IList<CollectionContractInformation> RetrieveContractInfo(long merchantID, long contractID)
        {
            return collectionRepository.RetrieveContractInfo(merchantID, contractID);
        }

        /// <summary>
        /// Retrieve Owner Info
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public IList<OwnerColModel> ListOwnerInfo(long merchantId, long contractId)
        {
            return collectionRepository.ListOwnerInfo(merchantId, contractId);
        }

        /// <summary>
        /// TO return check while activity notes
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public IList<CollectionActivities> ColActivityCheck(long merchantId, long contractId)
        {
            return collectionRepository.ColActivityCheck(merchantId, contractId);
        }

        #endregion

        #region Affiliations
        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="isRNC"></param>
        /// <returns></returns>
        public IList<ActiveContracts> ListActiveContractAffiliations(Int64 merchantId, bool isRNC)
        {
            return collectionRepository.ListActiveContractAffiliations(merchantId, isRNC);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="IsRNC"></param>
        /// <returns></returns>
        public IList<InActiveContracts> ListAffiliationsNonActiveContracts(Int64 merchantId, bool IsRNC)
        {
            return collectionRepository.ListAffiliationsNonActiveContracts(merchantId, IsRNC);
        }

        #endregion

        #region Documents
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentIds"></param>
        /// <returns></returns>
        public bool MrcDeleteDocuments(string DocumentIds)
        {
            return collectionRepository.MrcDeleteDocuments(DocumentIds);
        }

        /// <summary>
        /// To retrieve all documents related to a MerchantId
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="documentTypeId"></param>
        /// <returns></returns>
        public IList<DocumentsModel> RetrieveDocuments(Int64 merchantId, Int64 contractId, int documentTypeId)
        {
            return collectionRepository.ListDocuments(merchantId, contractId, documentTypeId);
        }


        public IList<DocumentsModel> RetrieveDocuments(int documentId)
        {
            return collectionRepository.ListDocuments(documentId);
        }
        #endregion

        #region LandLord

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public IList<CollectionLandlordModel> ListLandLordDetails(Int64 merchantId)
        {
            return collectionRepository.ListLandLordDetails(merchantId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateMerchantLandlordInformation(CollectionLandlordModel entity)
        {
            return collectionRepository.UpdateMerchantLandlordInformation(entity);
        }

        #endregion

        #region Lawyer Details

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public IList<LawyerModel> RetrieveAssignedLawyers(Int64 merchantId, Int64 contractId)
        {
            return collectionRepository.RetrieveAssignedLawyers(merchantId, contractId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public IList<LawyerModel> RetrieveAllLawyersToAssign(Int64 merchantId, Int64 contractId)
        {
            return collectionRepository.RetrieveAllLawyersToAssign(merchantId, contractId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public IList<LegalDocuments> RetrieveLegalDocuments(Int64 merchantId, Int64 contractId)
        {
            return collectionRepository.RetrieveLegalDocuments(merchantId, contractId);
        }       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AssignLawyersToCollection(LawyerModel model)
        {
            return collectionRepository.AssignLawyersToCollection(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public IList<LawyerModel> RetrieveAllLawyers()
        {
            return collectionRepository.RetrieveAllLawyers();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listLawyers"></param>
        /// <returns></returns>
        public bool InsUpdateLawyers(IList<LawyerModel> listLawyers)
        {
            string strDelLawyers = string.Empty;
            string strEditedlLawyers = string.Empty;
            StringBuilder sbLawyerXml = new StringBuilder();
            if (listLawyers.Count > 0)
            {
                List<string> deleteLawyer = listLawyers.Where(a => a.isDeleted == true).Select(a => a.lawyerId.ToString()).ToList();
                if (deleteLawyer.Count() > 0)
                    strDelLawyers = string.Join(",", deleteLawyer.ToArray());

                listLawyers = listLawyers.Where(a => a.isDeleted == false).ToList();
                if (listLawyers.Count > 0)
                {
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;
                    settings.CloseOutput = true;
                    settings.OmitXmlDeclaration = false;
                    XmlWriter writer = XmlWriter.Create(sbLawyerXml, settings);
                    writer.WriteStartElement("lawyerslist");
                    foreach (LawyerModel llw in listLawyers)
                    {
                        writer.WriteStartElement("lawyers");
                        writer.WriteAttributeString("LawyerId", Convert.ToString(llw.lawyerId));
                        writer.WriteAttributeString("FirstName", llw.firstName);
                        writer.WriteAttributeString("LastName", llw.lastName);
                        writer.WriteAttributeString("CompanyName", llw.companyName);
                        writer.WriteAttributeString("InsertUserId", Convert.ToString(llw.insertUserId));
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.Flush();
                    strEditedlLawyers = sbLawyerXml.ToString();
                }
            }
            sbLawyerXml = null;
            return collectionRepository.InsUpdateLawyers(strEditedlLawyers, strDelLawyers);
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
            return collectionRepository.RetrievePaymentDetails(merchantId, contractId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool InsertPaymentAgreement(CollectionPaymentAgreementModel model)
        {
            return collectionRepository.InsertPaymentAgreement(model);
        }
        #endregion

        #region Generate Letter
        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public IList<GenerateLetter> SendCollectionLetter(Int64 merchantId, Int64 contractId)
        {            
            return collectionRepository.GetCollectionLetter(merchantId, contractId);
        }
        #endregion

        #region Collection Activities
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<DropDown> ListCollectionActivities()
        {
            return collectionRepository.ListCollectionActivities();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantid"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool InsertCollectionActivity(CollectionActivityModel model)
        {
            return collectionRepository.InsertCollectionActivity(model);
        }
        #endregion

        public void Dispose()
        {
            collectionRepository = null;
        }         
    }
}