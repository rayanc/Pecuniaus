using Pecuniaus.Models;
using System.Collections.Generic;

namespace Pecuniaus.ApiHelper
{
    public static class CommonFunctions
    {

        public static IEnumerable<GeneralModel> GetTaskNames()
        {
            return RetrieveGeneralTypes("1006");
        }

        public static IEnumerable<GeneralModel> GetTaskStatuses()
        {
            return RetrieveGeneralTypes("1007");
        }

        public static IEnumerable<GeneralModel> GetNotesTypes()
        {
            return RetrieveGeneralTypes("1011");
        }

        public static IEnumerable<GeneralModel> GetDeclineReasons()
        {
            return RetrieveGeneralTypes("1004");
        }

        public static IEnumerable<GeneralModel> GetIndustries()
        {
            return RetrieveGeneralTypes("1001");
        }


        public static IEnumerable<GeneralModel> GetProcessors()
        {
            return RetrieveGeneralTypes("1010");
        }

        public static IEnumerable<GeneralModel> GetStates()
        {
            return RetrieveGeneralTypes("1009");
        }
        
        public static IEnumerable<GeneralModel> GetWorkFlowTypes()
        { 
            return RetrieveGeneralTypes("1014"); 
        }
        public static IEnumerable<GeneralModel> GetGroupTypes()
        {
            return RetrieveGeneralTypes("1016"); 
        }
        public static IEnumerable<GeneralModel> GetGroups()
        {
            return RetrieveGeneralTypes("1017"); 
        }
        public static IEnumerable<GeneralModel> GetUsers() 
        {
            return RetrieveGeneralTypes("1019");
        }
        public static IEnumerable<GeneralModel> GetParentModules() 
        {
            return RetrieveGeneralTypes("1020");
        }
        private static IEnumerable<GeneralModel> RetrieveGeneralTypes(string id)
        {
            string apiQuery = "gen/retrieve/" + id;
            return BaseApiData.GetAPIResult<IEnumerable<GeneralModel>>(apiQuery, () => new List<GeneralModel>());
        }

        

    }
}
