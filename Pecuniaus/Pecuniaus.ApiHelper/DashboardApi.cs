using Pecuniaus.Models;
using System.Collections.Generic;

namespace Pecuniaus.ApiHelper
{
    public class DashboardApi
    {
        public UserDashboardModel GetDashboardDetails(long userId)
        {
            var query = string.Format("users/GetDashboardDetails?UserId={0}", userId);
            return BaseApiData.GetAPIResult<UserDashboardModel>(query, () => new UserDashboardModel());
        }
    }
}
