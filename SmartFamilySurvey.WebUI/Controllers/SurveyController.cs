using ESS.SFS.BLL.ICollection;
using ESS.SFS.BLL.ICollection.IUser;
using ESS.SFS.DAL;
using ESS.SFS.Helper;
using ESS.SFS.ViewModel;
using MatSource.Library.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartFamilySurvey.WebUI.Controllers
{
    [AuthorizeFilter]
    public class SurveyController : Controller
    {
        private readonly IPool _pool;
        private readonly ILookUps _lookUps;
        private readonly IForm _form;
        private readonly ISurvey _survey;
        public readonly IUser _user;
        public SurveyController(IPool pool, ILookUps lookUps, IForm form, ISurvey survey, IUser user)
        {
            _pool = pool;
            _lookUps = lookUps;
            _form = form;
            _survey = survey;
            _user = user;
        }
       
        public IActionResult Index()
        {
            ViewBag.FormList = _form.GetForms();
            ViewBag.PoolList = _pool.GetPools();
            ViewBag.CityList = _lookUps.GetCity();
            ViewBag.FamilyStatusList = _lookUps.GetFamilyStatus();
            ViewBag.GenderList = _lookUps.GetGender();
            TblPoolSurvey tblPoolSurvey = new TblPoolSurvey();
            tblPoolSurvey.PoolTemplate = new TblPool();
            tblPoolSurvey.PoolTemplate.Participant = 20;

            return View();
            
        }
        [HttpPost]
        public IActionResult Index(TblPoolSurvey tblPoolSurvey)
        {
            if(!string.IsNullOrEmpty(tblPoolSurvey.PoolTemplate.Gender))
            {
            tblPoolSurvey.PoolTemplate.Gender = tblPoolSurvey.PoolTemplate.Gender.Trim();

            }
            if (!string.IsNullOrEmpty(tblPoolSurvey.PoolTemplate.FamilyStatus))
            {

            tblPoolSurvey.PoolTemplate.FamilyStatus = tblPoolSurvey.PoolTemplate.FamilyStatus.Trim();
            }
            _survey.SendSurvey(tblPoolSurvey);
            ViewBag.FormList = _form.GetForms();
            ViewBag.PoolList = _pool.GetPools();
            ViewBag.CityList = _lookUps.GetCity();
            ViewBag.FamilyStatusList = _lookUps.GetFamilyStatus();
            ViewBag.GenderList = _lookUps.GetGender();
            return RedirectToAction("successScreen");
        }
        public IActionResult successScreen()
        {
            return View();
        }
        public IActionResult ActiveSurveys(int page = 1, int pageSize = 5)
        {
            MyPagedList<TblSurvey> list = _survey.GetActiveSurvey(page, pageSize);
            ViewBag.list = list;
            ViewBag.pageIndex = page;
            ViewBag.pageSize = pageSize;
            return View(list.model);
        } 
        public IActionResult SurveyDetailedView(long Id)
        {
            ViewBag.Id = Id;
            List<TblPoolSurvey> tblPoolSurveys = _survey.GetPoolBySurveyId(Id);
            var CityList = _lookUps.GetCity();
            var FamilyStatusList = _lookUps.GetFamilyStatus();
            var GenderList = _lookUps.GetGender();
            var PoolList = _pool.GetPools();
            
            foreach (var item in tblPoolSurveys)
            {
                if (!string.IsNullOrEmpty(item.PoolTemplate.Gender))
                {
                    item.PoolTemplate.GenderName = GenderList.Where(x => x.Id == Convert.ToInt64(item.PoolTemplate.Gender)).SingleOrDefault().Name;
                }
                if (!string.IsNullOrEmpty(item.PoolTemplate.FamilyStatus))
                {
                    item.PoolTemplate.FamilySName = FamilyStatusList.Where(x => x.Id == Convert.ToInt64(item.PoolTemplate.FamilyStatus)).SingleOrDefault().Name;
                }
                if (!string.IsNullOrEmpty(item.PoolTemplate.CityIds))
                {
                    string[] cityIdList = item.PoolTemplate.CityIds.Split(",");
                    int counter = 0;
                    foreach (var id in cityIdList)
                    {
                        if (counter < 1)
                        {
                            item.PoolTemplate.CityName = CityList.Where(x => x.Id == Convert.ToInt64(id)).SingleOrDefault().Name;
                        }
                        else
                        {
                            item.PoolTemplate.CityName = item.PoolTemplate.CityName + "," + CityList.Where(x => x.Id == Convert.ToInt64(id)).SingleOrDefault().Name;
                        }
                        counter++;
                    }

                }
                var pool = PoolList.Where(x => x.Id == item.PoolTemplate.Id).SingleOrDefault();
                PoolList.Remove(pool);
            }
            //TblPool tblPool = new TblPool();
            //List<TblPoolSurvey> list = new List<TblPoolSurvey>();
            //TblPoolSurvey item;
            //for (int i = 0; i < tblPoolSurveys.Count; i++)
            //{
            //    item = new TblPoolSurvey();
            //    item = tblPoolSurveys[i];
               
            //    if (!string.IsNullOrEmpty(item.PoolTemplate.Gender))
            //    {                   
            //        item.PoolTemplate.Gender = GenderList.Where(x => x.Id == Convert.ToInt64(item.PoolTemplate.Gender)).SingleOrDefault().Name;
            //    }
            //    if (!string.IsNullOrEmpty(item.PoolTemplate.FamilyStatus))
            //    {
            //        item.PoolTemplate.FamilyStatus = FamilyStatusList.Where(x => x.Id == Convert.ToInt64(item.PoolTemplate.FamilyStatus)).SingleOrDefault().Name;
            //    }
            //    if (!string.IsNullOrEmpty(item.PoolTemplate.CityIds))
            //    {
            //        string[] cityIdList = item.PoolTemplate.CityIds.Split(",");
            //        int counter = 0;
            //        foreach (var id in cityIdList)
            //        {
            //            if (counter < 1)
            //            {
            //                item.PoolTemplate.CityIds = CityList.Where(x => x.Id == Convert.ToInt64(id)).SingleOrDefault().Name;
            //            }
            //            else
            //            {
            //                item.PoolTemplate.CityIds = item.PoolTemplate.CityIds + "," + CityList.Where(x => x.Id == Convert.ToInt64(id)).SingleOrDefault().Name;
            //            }
            //            counter++;
            //        }

            //    }
            //    var pool = PoolList.Where(x => x.Id == tblPoolSurveys[i].PoolTemplate.Id).SingleOrDefault();
            //    PoolList.Remove(pool);
            //    list.Add(item);
            //}

            ViewBag.PoolList = PoolList;
            return View(tblPoolSurveys);
        }
        public IActionResult SurveyArchive(int page = 1, int pageSize = 5)
        {
            MyPagedList<TblSurvey> list = _survey.GetArchiveSurvey(page, pageSize);
            ViewBag.list = list;
            ViewBag.pageIndex = page;
            ViewBag.pageSize = pageSize;
            return View(list.model);
        }
        public IActionResult TemplateDetailedView()
        {
            return View();
        }

        public IActionResult SurveyDetail(string sid)
        {
            TblSurvey objSurvey = _user.GetActiveSurveyDetail(sid);
            return View(objSurvey);
        }

        [HttpPost]
        public string SubmitSurveyForm(string sid)
        {
            Int64? surveyId = Convert.ToInt64(Common.Decryption(sid));
            if (HttpContext.Session.GetString("userID") != null)
            {
                Int64? userId = Convert.ToInt64(Common.Decryption(HttpContext.Session.GetString("userID")));
                if (_user.UpdateSurveyDetail(surveyId, userId))
                {
                    return Constants.SurveyDetailMessage;
                }
            }
            else
            {
                return null;
            }
            return null;
        }

        public IActionResult Message()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SendServey(string PoolIds,string SurveyId)
        {
            long sId = Convert.ToInt64(SurveyId);
            string[] cityIdList = PoolIds.Split(",");
            foreach (var item in cityIdList)
            {
                
                _survey.SendSurveyFromPool(sId,Convert.ToInt64(item),null,_pool.GetPoolById(Convert.ToInt64(item)));
            }
            return Json(new
            {
                status="success",
            });

        }
        [HttpPost]
        public JsonResult DeleteServey(long Id)
        {
                _survey.DeleteSurvey(Id);
          
            return Json(new
            {
                status = "success",
            });

        }
        [HttpPost]
        public JsonResult ResendServey(long SurveyId,DateTime dueDate)
        {
            string userId ="";
           long Id= _survey.UpdateSurveyDueDate(SurveyId, dueDate);
            if (Id>0)
            {
                List<TblPoolSurvey> tblPools = _survey.GetPoolBySurveyId(SurveyId);
                foreach (var item in tblPools)
                {
                    var users = item.TblUserSurvey.Where(x=>x.PoolSurveyId==item.Id).Select(x => x.UserId).ToList();
                    userId = "";
                    foreach (var id in users)
                    {
                        if (string.IsNullOrEmpty(userId))
                        {
                            userId = id.ToString();
                        }
                        else
                        {
                            userId = userId + id.ToString();
                        }
                    }

                     _survey.SendSurveyFromPool(Id, item.PoolTemplate.Id,userId,item.PoolTemplate);
                }
            }

            return Json(new
            {
                status = "success",
            });

        }
        [HttpPost]
        public IActionResult SearchSurveyArchive(string text,int page = 1, int pageSize = 5)
        {
            MyPagedList<TblSurvey> list = _survey.GetArchiveSurvey(text,page, pageSize);
            List<TblSurvey> modal = list.model;
            ViewBag.list = list;
            return PartialView("_SurveyArchive", modal);
        }
    }
}
