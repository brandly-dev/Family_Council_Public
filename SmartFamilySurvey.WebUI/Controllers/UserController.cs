using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ESS.SFS.BLL.ICollection;
using ESS.SFS.BLL.ICollection.IUser;
using ESS.SFS.DAL;
using ESS.SFS.Helper;
using ESS.SFS.ViewModel;
using MatSource.Library.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartFamilySurvey.WebUI.Controllers
{

    public class UserController : Controller
    {
        public IUser _user;
        private readonly ILookUps _lookUp;
        private readonly IAccount _account;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(IUser user, IAccount account, ILookUps lookUp, IWebHostEnvironment webHostEnvironment)
        {
            _user = user;
            _lookUp = lookUp;
            _account = account;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult SendInvitation()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendInvitation(UserViewModel uvm)
        {
            TblUser user = new TblUser();
            user.Username = uvm.email;
            user.Message = uvm.message;
            user.Status = InvitationStatuses.Invited;
            user.Role = UserRole.User;
            // var IsExist = _user.userExist(user);

            user = _user.saveUserInvitation(user);
            if (user != null)
            {
                string webRootPath = _webHostEnvironment.ContentRootPath;
                //get base url
                var host = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
                //encrypt userid
                string EncryptedUserId = Common.Encryption(user.Id.ToString());
                //controller action path
                var verifyUrl = "/Account/SignUp?uid=" + EncryptedUserId;
                //link
                var link = host + verifyUrl;

                Hashtable ht = new Hashtable();
                ht.Add("<!--EMAIL-->", user.Username);
                ht.Add("<!--LINK-->", link);
                // string fileName = "~//EmailTemp/HostUs.html";
                string path = Path.Combine(webRootPath, "EmailTemplate/SendInvitation.html");
                String EmailBody = Common.GetContextFromHTML(ht, path);

                Common.sendEmailFromAWS(EmailBody, "send invitation", user.Username, "");
                //TempData["Message"] = Constants.InvitationSend;

                return RedirectToAction("Invitation", "Pool");
            }

            return View();
        }

        public IActionResult InvitationMessage()
        {
            return View();
        }
        [AuthorizeFilter]
        public IActionResult ActiveSurveyUser(Int32? pg = 1, Int32? ps = 5)
        {

            if (HttpContext.Session.GetString("userID") != null)
            {
                Int64? userId = Convert.ToInt64(Common.Decryption(HttpContext.Session.GetString("userID")));
                var lstResult = _user.getActiveSurvey(userId, Convert.ToInt32(pg), Convert.ToInt32(ps));
                ViewBag.RowCount = lstResult.RowCount;
                ViewBag.lstActiveSurveyUser = lstResult;
                ViewBag.PageSize = ps;
                ViewBag.PageIndex = pg;

            }
            else
            {
                ViewBag.lstActiveSurveyUser = null;
            }
            return View();
        }

        [AuthorizeFilter]
        public IActionResult ArchiveUser(string fd, string td, Int32? pg = 1, Int32? ps = 5)
        {
            if (HttpContext.Session.GetString("userID") != null)
            {
                Int64? userId = Convert.ToInt64(Common.Decryption(HttpContext.Session.GetString("userID")));
                var lstResult = _user.getArchiveSurvey(userId, fd, td, Convert.ToInt32(pg), Convert.ToInt32(ps));
                ViewBag.lstActiveSurveyUser = lstResult;
                ViewBag.RowCount = lstResult.RowCount;
                ViewBag.PageSize = ps;
                ViewBag.PageIndex = pg;
                ViewBag.FromDate = fd;
                ViewBag.ToDate = td;
            }
            else
            {
                ViewBag.lstArchiveSurveyUser = null;
            }

            return View();
        }

        
        [AuthorizeFilter]
        public IActionResult UserPayment()
        {

            if (HttpContext.Session.GetString("userID") != null)
            {
                Int64? userId = Convert.ToInt64(Common.Decryption(HttpContext.Session.GetString("userID")));
                ViewBag.lstPayments = _user.GetPayments(userId, null);
                ViewBag.userInfo = _user.GetUser(userId);
                ViewBag.surveryInfo = _user.getSurveyUser(userId);
            }
            else
            {
                ViewBag.lstPayments = null;
            }

            return View();
        }


        [HttpPost]
        public IActionResult UserPaymentType(string PaymentType)
        {

            if (HttpContext.Session.GetString("userID") != null)
            {
                Int64? userId = Convert.ToInt64(Common.Decryption(HttpContext.Session.GetString("userID")));
                ViewBag.lstPayments = _user.GetPayments(userId, PaymentType);
            }
            else
            {
                ViewBag.lstPayments = null;
            }

            return PartialView("/Views/User/_UserPayment.cshtml", ViewBag.lstPayments);
        }

        [HttpPost]
        public string RequestPayment(string points, string paymentType)
        {
            if (HttpContext.Session.GetString("userID") != null)
            {
                Int64? userId = Convert.ToInt64(Common.Decryption(HttpContext.Session.GetString("userID")));
                if (_user.RequestedPayment(points, paymentType, userId))
                {
                    return Constants.RequestSendApproval;
                }
            }
            return null;
        }


        [HttpGet]
        public IActionResult UserProfile()
        {
            if (HttpContext.Session.GetString("userID") != null)
            {
                Int64? userId = Convert.ToInt64(Common.Decryption(HttpContext.Session.GetString("userID")));
                TblUser user = _account.getUserByID(userId);
                UserViewModel model = new UserViewModel();
                model.lstCity = _account.getCities();
                model.lstGender = _lookUp.GetGender();
                model.lstCountry = Common.GetCountry();
                model.lstFamilyStatus = _lookUp.GetFamilyStatus();
                if (user != null)
                {
                    model.userName = user.Username;
                    model.email = user.Username;
                    model.Id = user.Id;
                    model.password = user.Password;
                    model.surname = user.FullName;
                    model.maritalStatus = Convert.ToString(user.MaritalStatus);
                    model.gender = user.Gender;
                    model.age = user.Age;
                    model.country = user.Country;
                    model.city = Convert.ToString(user.City);
                    model.profilePic = user.ProfilePicture;
                }

                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public IActionResult UserProfile(UserViewModel model)
        {
            string unqiueFileName = string.Empty;
            if (model.ProfileImage != null)
            {
                if (model.ProfileImage.FileName != null)
                {
                    unqiueFileName = UploadedFile(model);
                    if (model.profilePic != null)
                    {
                        System.IO.File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, "App_Shared/" + model.profilePic));
                    }
                }
            }
            model.lstCity = _account.getCities();
            model.lstGender = _lookUp.GetGender();
            model.lstCountry = Common.GetCountry();
            model.lstFamilyStatus = _lookUp.GetFamilyStatus();
            model.profilePic = unqiueFileName;

            TblUser user = _account.updateUser(model);

            if (user != null)
            {
                TempData["Message"] = Constants.UpdateUserProfile;
            }

            return RedirectToAction("UserProfile");
        }

        [HttpGet]
        public IActionResult AdminProfile()
        {
            if (HttpContext.Session.GetString("userID") != null)
            {
                Int64? userId = Convert.ToInt64(Common.Decryption(HttpContext.Session.GetString("userID")));
                TblUser user = _account.getUserByID(userId);
                UserViewModel model = new UserViewModel();
                if (user != null)
                {
                    model.userName = user.Username;
                    model.email = user.Username;
                    model.Id = user.Id;
                    model.password = user.Password;
                    model.surname = user.FullName;
                }

                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }


        [HttpPost]
        public IActionResult AdminProfile(UserViewModel model)
        {
            TblUser user = _account.updateAdminUser(model);

            if (user != null)
            {
                TempData["Message"] = Constants.UpdateAdminProfile;
            }

            return RedirectToAction("AdminProfile");
        }

        private string UploadedFile(UserViewModel model)
        {
            string uniqueFileName = null;

            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "App_Shared");
                var extension = Path.GetExtension(model.ProfileImage.FileName);
                uniqueFileName = DateTime.Now.ToString("MMddyyHHmmss") + extension;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public IActionResult Message()
        {
            return View();
        }

        [AuthorizeFilter]
        public IActionResult PendingPaymentList()
        {
            ViewBag.lstPayments = _user.GetPaymentForPending(null, null);
            return View();
        }

        [AuthorizeFilter]
        public IActionResult ArchivePaymentList()
        {
            ViewBag.lstPayments = _user.GetPaymentForArchive(null, null);
            return View();
        }

        [HttpPost]
        public string UpdatePaymentStatus(Int64? paymentId)
        {

            if (_user.updatePaymentStatus(paymentId))
            {
                return "update";
            }
            return "";
        }

        public IActionResult PaymentMessage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PendingPaymentListSearch(string FromDate, string ToDate)
        {
            ViewBag.lstPayments = _user.GetPaymentForPending(FromDate, ToDate);
            return PartialView("/Views/User/_PendingPaymentList.cshtml", ViewBag.lstPayments);
        }

        [HttpPost]
        public IActionResult ArchivePaymentListSearch(string FromDate, string ToDate)
        {
            ViewBag.lstPayments = _user.GetPaymentForPending(FromDate, ToDate);
            return PartialView("/Views/User/_ArchivePaymentList.cshtml", ViewBag.lstPayments);
        }
    }
}
