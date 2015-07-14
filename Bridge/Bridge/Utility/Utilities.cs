using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Bridge.Models;


namespace Bridge.Utility
{
    public class Utilities
    {

        /// <summary>
        /// Enum for file extensions
        /// </summary>
        public enum FileExtensions
        {
            pdf,
            doc,
            docx,
            png,
            jpg,
            gif
        }
        public enum Types
        {
            indusrty=1001,
            business=1002,
            address=1003,
            declinereason=1004,
            entityType=1005,
            tasktypes=1006,
            taskstatus=1007,
            collectiontypes=1008,
            province=1009,
            processor=1010,
            notetype=1011,
            propertyType=1012,
            documentType=1013,
            workflowType = 1014,
            salesreps=1015,
            groupType=1016,
            group = 1017,
            advance=1018,
            user=1019,
            module=1020
           
        }

        public enum EmailTemplates
        {
            CollectionLetter
            //,ContactUsEmail
        }
       
        //array contains values for content type
        public static Dictionary<string, string> contentType = new Dictionary<string, string>();

        static Utilities()
        {
            contentType.Add("json", "application/json");
            contentType.Add("pdf", "application/pdf");
            contentType.Add("csv", "text/csv");
            contentType.Add("image", "image/png");
            contentType.Add("stream", "application / octet - stream");
        }

        /// <summary>
        /// Method used to handle errors
        /// </summary>
        /// <param name="errorType"></param>
        /// <param name="errorTarget"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static ResponseBase HandleError(string errorType, string errorTarget, string errorMessage)
        {
            ResponseBase ResultErrors = new ResponseBase();
            List<HandleErrorModel> errorList = new List<HandleErrorModel>();
            HandleErrorModel ErrorModel = new HandleErrorModel();
            ErrorModel.type = errorType;
            ErrorModel.message = errorMessage;
            errorList.Add(ErrorModel);
            ResultErrors.errors = errorList;
            return ResultErrors;
        }

        /// <summary>
        /// To Check whether the input parameter is number or not
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsValueConvertibleToInteger(string number)
        {
            int result;
            return int.TryParse(number, out result);
        }

        /// <summary>
        /// To validate Email Address
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public static bool IsValueConvertibleToEmail(string Email)
        {
            Regex EmailRegex = new Regex(Resources.Common.String1);
            Match m = EmailRegex.Match(Email);
            if (m.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// To Check whether the input parameter is number or not
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsValueConvertibleToDateTime(string date)
        {
            DateTime result;
            if (DateTime.TryParse(date, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out result))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Method used to get variable name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static string GetVariableName<T>(Expression<Func<T>> expr)
        {
            var body = (MemberExpression)expr.Body;

            return body.Member.Name;
        }

        /// <summary>
        /// Get error target from keys
        /// </summary>
        /// <param name="index"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static string GetErrorTarget(int index, ICollection<string> keys)
        {
            string result = string.Empty;

            int i = 0;

            foreach (var key in keys)
            {
                if (i == index)
                {
                    result = key.Split('.')[key.Split('.').Length - 1];
                    break;
                }

                i++;
            }

            return result;
        }

     

        /// <summary>
        /// Method used to generate parsed address
        /// </summary>
        /// <param name="addressLine1"></param>
        /// <param name="city"></param>
        /// <param name="stateID"></param>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        public static string ParseAddress(string addressLine1, string city, string stateID, string zipCode)
        {
            string parsedAddress = string.Empty;
            parsedAddress = addressLine1 + ", " + city + ", " + stateID;

            if (!string.IsNullOrEmpty(zipCode))
                parsedAddress = parsedAddress + ", " + zipCode;

            return parsedAddress;
        }

        /// <summary>
        /// Property for getting base URI 
        /// </summary>
        public static string GetBaseURI
        {
            get
            {
                var Request = System.Web.HttpContext.Current.Request;
                return Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            }
        }

        /// <summary>
        /// get Uri for Notes
        /// </summary>
        public static string GetUriForNotes
        {
            get
            {
                return GetBaseURI + "/";
            }
        }
        /// <summary>
        /// Implement a custom sorting algorithm for all the lists
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="SortOrder"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static List<T> SortList<T>(List<T> collection, string SortOrder, string propertyName) where T : class
        {
            try
            {
                if (SortOrder == "desc")
                {
                    return collection.OrderByDescending(cc => cc.GetType().GetProperty(propertyName).GetValue(cc, null)).ToList();
                }
                return collection.OrderBy(cc => cc.GetType().GetProperty(propertyName).GetValue(cc, null)).ToList();
            }
            catch (Exception )
            {
                return collection.ToList();
            }
        }
    }

}
