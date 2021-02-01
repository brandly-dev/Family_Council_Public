using ESS.SFS.BLL.ICollection;
using ESS.SFS.BLL.ICollection.IUser;
using ESS.SFS.DAL;
using ESS.SFS.Helper;
using ESS.SFS.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SmartFamilySurvey.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILookUps _lookUp;
        private readonly IAccount _account;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IForm _form;
        public readonly IUser _user;
        public AccountController(IAccount account, ILookUps lookUp, IWebHostEnvironment webHostEnvironment,IForm form, IUser user)
        {
            _account = account;
            _lookUp = lookUp;
            _webHostEnvironment = webHostEnvironment;
            _form = form;
            _user = user;
        }
        public IActionResult Index(string uid)
        {

            if (!string.IsNullOrEmpty(uid))
            {
                Int64? userId = Convert.ToInt64(Common.Decryption(uid));
                var statusId = _account.updateUserStatus(userId);
                if (statusId)
                {
                    TempData["success"] = Constants.VerificationCompleted;
                }
            }

            return View();
        }
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(AdminLogin mdl)
        {
            if (ModelState.IsValid)
            {
                TblUser objUser = _account.getUserByEmail(mdl.email);
                if (objUser != null)
                {
                    if (objUser.Status == InvitationStatuses.Invited)
                    {
                        ModelState.AddModelError(string.Empty, Constants.VerificationEmailSendMessage);
                    }

                    if (objUser.Password != null)
                    {
                        string encryptPassword = Common.EncryptPassword(mdl.password);
                        if (objUser.Password == encryptPassword)
                        {
                            HttpContext.Session.SetString("userID", Common.Encryption(objUser.Id.ToString()));
                            HttpContext.Session.SetString("role", objUser.Role);
                            HttpContext.Session.SetString("email", objUser.Username);
                            if (objUser.FullName != null)
                            {
                                HttpContext.Session.SetString("userName", objUser.FullName);
                                var test = HttpContext.Session.GetString("userID");
                            }

                            if (objUser.Role == "Admin")
                            {
                                return RedirectToAction("ActiveSurveys", "Survey");
                            }
                            else
                            {

                                return RedirectToAction("ActiveSurveyUser", "User");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, Constants.InvalidPassword);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, Constants.VerificationEmailSendMessage);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, Constants.InvalidUser);
                }
            }

            return View(mdl);
        }
        public IActionResult SignUp(string uid)
        {
            UserViewModel model = new UserViewModel();
            model.lstCity = _account.getCities();
            model.lstGender = _lookUp.GetGender();
            model.lstCountry = Common.GetCountry();

            if (!string.IsNullOrEmpty(uid))
            {
                Int64? userId = Convert.ToInt64(Common.Decryption(uid));
                TblUser user = _account.getUserByID(userId);


                if (user != null)
                {
                    model.userName = user.Username;
                    model.email = user.Username;
                    model.Id = user.Id;
                }
            }

            model.maritalStatus = MaritalStatuses.Single;
            return View(model);
        }
        [HttpPost]
        public IActionResult SignUp(UserViewModel model)
        {
            TblUser user = _account.saveSignUp(model);
            if (user != null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }


        public IActionResult VerificationEmail()
        {
            return View();
        }

        public IActionResult SetPassword(string uid)
        {
            AdminSetPassword setPassword = new AdminSetPassword();
            setPassword.id = uid;
            return View(setPassword);
        }

        [HttpPost]
        public IActionResult SetPassword(AdminSetPassword mdl)
        {
            if (ModelState.IsValid)
            {
                Int64? userId = Convert.ToInt64(Common.Decryption(mdl.id));
                var user = _account.getUserByID(userId);
                if (user != null)
                {
                    user.Password = Common.EncryptPassword(mdl.password);
                    if (_account.updatePassword(userId, user.Password))
                    {
                        TempData["Message"] = Constants.PasswordReset;
                        return RedirectToAction("Message");
                    }
                }
                else
                {
                    TempData["Error"] = "User not found.";
                }
            }
            return View(mdl);
        }

        [AllowFilter]
        public IActionResult Logout()
        {
            //clear all sessions
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgetPassword(AdminForgetPass mdl)
        {
            if (ModelState.IsValid)
            {
                var user = _account.getUserByEmail(mdl.email);
                if (user != null && user.Username == mdl.email)
                {
                    var host = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
                    //encrypt userid
                    string EncryptedUserId = Common.Encryption(user.Id.ToString());
                    //controller action path
                    var verifyUrl = "/Account/SetPassword?uid=" + EncryptedUserId;

                    //link
                    var link = host + verifyUrl;

                    string webRootPath = _webHostEnvironment.ContentRootPath;
                    Hashtable ht = new Hashtable();
                    ht.Add("<!--LINK-->", link);
                    ht.Add("<!--EMAIL-->", user.Username);
                    string path = Path.Combine(webRootPath, "EmailTemplate/ForgotPassword.html");
                    String EmailBody = Common.GetContextFromHTML(ht, path);

                    Common.sendEmailFromAWS(EmailBody, "forgot password", user.Username, "");

                    TempData["Message"] = Constants.ForgetPassword;
                    return RedirectToAction("Message");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User Not Exist");
                }
            }
            return View();
        }

        public IActionResult Message()
        {
            return View();
        }
        [AllowFilter]
        public IActionResult GoogleForm() {
           
            return View();
        }
        [HttpPost]
        public IActionResult getData()
        {
            GetData getData = new GetData(_form);
            getData.GetDriveData();
            return RedirectToAction("GoogleForm");
        }
        [AllowFilter]
        [HttpPost]
        public IActionResult WebHock([FromBody]Root obj)
        {
            var tblUser = _user.getUserByEmail(obj.email);
            var tblForm = _form.GetFormByGoogleId(obj.formId);
            
            if(tblUser != null && tblForm != null)
            {
                var survey = _user.getSurveryIdForClientForm(tblUser.Id,tblForm.Id);
                bool val=_user.UpdateSurveyDetail(survey.SurveyId, tblUser.Id);
            }
            return Ok();
            //GetData getData = new GetData(_form);
            //getData.GetDriveData();
            //return RedirectToAction("GoogleForm");
        }
    }
    public class Root
    {
       
        public string email { get; set; }
      
        public string formId { get; set; }
        public string formTitle { get; set; }
    }
}
