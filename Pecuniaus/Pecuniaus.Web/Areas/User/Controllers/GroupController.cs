using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Pecuniaus.ApiHelper;
using Pecuniaus.User.Models;
using Pecuniaus.User.Repository;
using Newtonsoft.Json;
using System.Text;

namespace Pecuniaus.User.Controllers
{
    public class GroupController : BaseController
    {

        GroupSessionRepository groupSessionRepository;
        #region Constructor
        public GroupController()
        {
            groupSessionRepository = new GroupSessionRepository();
        }
        #endregion

        // GET: /Group/
        #region Actions
        public ActionResult Index()
        {
            GroupModel groupModel = new GroupModel();
            groupModel.GroupTypes = GetGroupTypes();
            groupModel.LstGroups = GetAllGroups(string.Empty);
            groupSessionRepository.Set(groupModel.LstGroups);
            return View("ManageGroups", groupModel);
        }

        [HttpGet]
        public ActionResult AddGroup() 
        {
            return PartialView("_AddGroup", new GroupModel
            {
                GroupTypes = GetGroupTypes(),
                LstGroups = GetAllGroups(string.Empty)
            });
        }

        [HttpPost] 
        public ActionResult AddUpdateGroup(string submitButton, GroupModel groupModel)
        {
                    if (ModelState.IsValid)
                    {
                        if (CheckGroupExist(groupModel) == false)
                        {
                            if (groupModel.GroupID==0)
                            {
                                groupModel.InsertUserId = base.CurrentUserID;
                            }
                            else
                            {
                                groupModel.ModifyUserId = base.CurrentUserID;
                            }
                            AddUpdateGroup(groupModel);
                            if (ModelState.IsValid)
                            {                                
                                return RedirectToAction("AddGroup");
                            }

                           
                        }
                    }

                    return PartialView("_AddGroup", groupModel);
            }



        [HttpGet]
        public ActionResult EditGroup(long id)
        {
            GroupModel groupModel=new GroupModel();
            List<GroupModel> LstGroups = GetAllGroups(string.Empty);
            if(LstGroups!=null && LstGroups.Count>0)
            {
                  groupModel = (from grp in LstGroups
                                  where grp.GroupID==id
                                  select grp).SingleOrDefault();
                groupModel.GroupTypes = GetGroupTypes();
               
                groupModel.IsUpdate = true;
                groupModel.GroupTypeID = groupModel.GroupTypeID;
               
            }

            return PartialView("_AddGroup", groupModel);
            
        }
        [HttpGet]
        public ActionResult ListGroups() 
        {
            List<GroupModel> lstGroups = GetAllGroups(string.Empty);
            return PartialView("_GroupListing", lstGroups);
        }

        [HttpGet]
        public JsonResult CheckGroupname(string groupname, int groupID)   
        {
            GroupModel grp = new GroupModel();
            grp.GroupID = groupID;
            grp.GroupName = groupname;

            var isDuplicate = CheckGroupExist(grp);
            var jsonData = new { isDuplicate };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Methods

        public bool CheckGroupExist(GroupModel grp) 
        {
            bool result = false;

            var apiMethod = "users/CheckGroupExist";

            var response = BaseApiData.PostAPIData<GroupModel>(apiMethod, grp);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = true;
            }

            else
                result = false;

            return result;
        }
        public void AddUpdateGroup(GroupModel obj)
        {
            
            var apiMethod = "users/addUpdateGroup";
            var response = BaseApiData.PostAPIData<GroupModel>(apiMethod, obj);            
            if (response.StatusCode == HttpStatusCode.OK)
                if (obj.GroupID==0)
                {
                    base.SetSuccessMessage(Pecuniaus.Resources.User.Messages.GroupAddSuccess);
                }
                else
                {
                    base.SetSuccessMessage(Pecuniaus.Resources.User.Messages.GroupUpdateSuccess);
                }
               
            else
                base.SetSuccessMessage(Pecuniaus.Resources.User.Messages.GroupUpdateFailure);
        }

        public List<GroupModel> GetAllGroups(string searchText) 
        {
            List<GroupModel> lstGroups = null;           
            if (string.IsNullOrEmpty(searchText))
            {
                searchText = "null";
            }
            var apiMethod = "users/GetAllGroups/" + searchText;
            IList<GroupModel> objBE;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["APIURI"] + apiMethod);
            using (Stream responseStream = objRequest.GetResponse().GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                objBE = JsonConvert.DeserializeObject<IList<GroupModel>>(reader.ReadToEnd());
                lstGroups = objBE.ToList();
            }
           
            return lstGroups;
        }

        #endregion
    }
}