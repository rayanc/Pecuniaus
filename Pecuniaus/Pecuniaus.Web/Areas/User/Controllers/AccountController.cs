using System;
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
using System.Net.Http;
using Pecuniaus.UICore;
namespace Pecuniaus.User.Controllers
{
    public class AccountController : BaseController
    {
        UserSesssionRepository userSessionRepository;
        //
        // GET: /Account/
        public AccountController()
        {
            userSessionRepository = new UserSesssionRepository();
        }

        #region  Actions
        public ActionResult ManageUsers()
        {
            UserModel usrModel = new UserModel();
            usrModel.ListUsers = GetAllUsers(string.Empty);
            usrModel.States = GetProvince();
            usrModel.Groups = GetGroups();
            return View(usrModel);
        }
        public ActionResult Index()
        {
            UserModel usrModel = new UserModel();
            usrModel.ListUsers = GetAllUsers(string.Empty);
            usrModel.States = GetProvince();
            usrModel.Groups = GetGroups();
            userSessionRepository.Set(usrModel.ListUsers);
            return View("ManageUsers", usrModel);
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            return PartialView("_AddUser", new UserModel
            {
                DateofBirth = DateTime.Now.AddYears(-10),
                States = GetProvince(),
                Groups = GetGroups(),
                ListUsers = GetAllUsers(string.Empty)

            });
        }

        [HttpPost]
        public ActionResult AddUpdateUser(string submitButton, UserModel userModel)
        {
            SetDefaultValues(userModel);
            var GroupTypeID = Request["GroupTypeID"];

            switch (submitButton)
            {
                case "Save":
                    if (ModelState.IsValid)
                    {
                        if (CheckUserExist(userModel) == false)
                        {
                            userModel.Password = new PaswordHelper().HashPassword(userModel.Password);
                            userModel.SecurityStamp = Guid.NewGuid().ToString();
                            AddUser(userModel);
                            if (ModelState.IsValid)
                            {
                                userSessionRepository.AddUser(userModel);
                                return RedirectToAction("AddUser");
                            }
                            base.SetSuccessMessage(Pecuniaus.Resources.User.Messages.UserAddSuccess);
                            return PartialView("_AddUser", userModel);
                        }
                    }

                    break;
                case "Update":
                    ModelState.Remove("Password");
                    ModelState.Remove("ConfirmPassword");
                    if (ModelState.IsValid)
                    {
                        if (CheckUserExist(userModel) == false)
                        {
                            if (userModel.GroupID == null)
                            {
                                userModel.GroupID = 0;
                            }

                            UpdateUser(userModel);
                            userSessionRepository.Update(userModel);
                            base.SetSuccessMessage(Pecuniaus.Resources.User.Messages.UserUpdateSuccess);
                            return RedirectToAction("AddUser");
                        }
                    }

                    break;
                default:
                    return RedirectToAction("AddUser");

            }


            return View("ManageUsers", userModel);


        }

        private static void SetDefaultValues(UserModel userModel)
        {
            userModel.UserID = userModel.UserName;
            if (userModel.PostalCode == null)
            {
                userModel.PostalCode = "";
            }
            if (userModel.AddressLine1 == null)
            {
                userModel.AddressLine1 = "";
            }
            if (userModel.AddressLine2 == null)
            {
                userModel.AddressLine2 = "";
            }
            if (string.IsNullOrEmpty(userModel.Email))
            {
                userModel.Email = "";
            }
        }

        [HttpGet]
        public ActionResult EditUser(long id)
        {
            UserModel userModel = new UserModel();
            List<UserModel> LstUsers = GetAllUsers(string.Empty);
            if (LstUsers != null && LstUsers.Count > 0)
            {
                userModel = (from usr in LstUsers
                             where usr.ID == id
                             select usr).SingleOrDefault();
                if (userModel != null)
                {
                    userModel.States = GetProvince();
                    userModel.Groups = GetGroups();
                    userModel.IsUpdate = true;
                }

            }
            return PartialView("_AddUser", userModel);
        }



        [HttpPost]
        public ActionResult DelUser(long id)
        {
            userSessionRepository.DelUser(id);

            return Json("OK");
        }

        [HttpGet]
        public ActionResult ListUsers()
        {
            List<UserModel> lstUsers = GetAllUsers(string.Empty);

            return PartialView("_UserListing", lstUsers);
        }
        #endregion

        #region Methods
        public List<UserModel> GetAllUsers(string searchText)
        {
            List<UserModel> lstUsers = null;
            if (string.IsNullOrEmpty(searchText))
            {
                searchText = "null";
            }
            //if (searchText != null)
            //{
            IList<UserModel> objBE;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["APIURI"] + "users/GetAllUsers/" + searchText);

            using (Stream responseStream = objRequest.GetResponse().GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                objBE = JsonConvert.DeserializeObject<IList<UserModel>>(reader.ReadToEnd());
                lstUsers = objBE.ToList();
            }
            //}
            return lstUsers;
        }

        [HttpGet]
        public JsonResult CheckUsername(string username, string userID)
        {
            UserModel usr = new UserModel();
            usr.UserID = userID;
            usr.UserName = username;

            var isDuplicate = CheckUserExist(usr);
            var jsonData = new { isDuplicate };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public bool CheckUserExist(UserModel usr)
        {
            bool result = false;

            var apiMethod = "users/CheckUserExist";

            var response = BaseApiData.PostAPIData<UserModel>(apiMethod, usr);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = true;
            }

            else
                result = false;

            return result;
        }
        public void UpdateUser(UserModel obj)
        {
            var apiMethod = "users/updateuser";
            var response = BaseApiData.PostAPIData<UserModel>(apiMethod, obj);
            //Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ReadAsStringAsync().Result);

            if (response.StatusCode == HttpStatusCode.OK)
                base.SetSuccessMessage(Pecuniaus.Resources.User.Messages.UserUpdateSuccess);
            else
                base.SetSuccessMessage(Pecuniaus.Resources.User.Messages.UserUpdateFailure);
        }
        public void AddUser(UserModel obj)
        {
            var apiMethod = "users/createuser";



            HttpResponseMessage response = BaseApiData.PostAPIData<UserModel>(apiMethod, obj);

            if (response.StatusCode == HttpStatusCode.OK)
                base.SetSuccessMessage(Pecuniaus.Resources.User.Messages.UserAddSuccess);
            else
                base.SetSuccessMessage(Pecuniaus.Resources.User.Messages.UserAddFailure);
        }

        #endregion
    }
}