using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESS.SFS.BLL.ICollection;
using ESS.SFS.DAL;
using ESS.SFS.ViewModel;
using MatSource.Library.Helper;
using Microsoft.AspNetCore.Mvc;

namespace SmartFamilySurvey.WebUI.Controllers
{
    [AuthorizeFilter]
    public class PoolController : Controller
    {
        private readonly IPool _pool;
        private readonly ILookUps _lookUps;
        public PoolController(IPool pool, ILookUps lookUps)
        {
            _pool = pool;
            _lookUps = lookUps;
        }
        public IActionResult Index(long? id = 0)
        {
            ViewBag.CityList = _lookUps.GetCity();
            ViewBag.FamilyStatusList = _lookUps.GetFamilyStatus();
            ViewBag.GenderList = _lookUps.GetGender();
            if (id > 0)
            {
                // this is the call for update
            }
            TblPool tblPool = new TblPool();
            tblPool.Participant = 20;
            return View(tblPool);
        }
        [HttpPost]
        public IActionResult Index(TblPool tblPool)
        {
            if (!string.IsNullOrEmpty(tblPool.Gender))
            {
                tblPool.Gender = tblPool.Gender.Trim();

            }
            if (!string.IsNullOrEmpty(tblPool.FamilyStatus))
            {
                tblPool.FamilyStatus = tblPool.FamilyStatus.Trim();

            }
            string error = _pool.Save(tblPool);
            if (string.IsNullOrEmpty(error))
            {
                return RedirectToAction("Poolstemplates");
            }
            return View();
        }
        public IActionResult Poolstemplates(int page = 1, int pageSize = 5)
        {
            var CityList = _lookUps.GetCity();
            var FamilyStatusList = _lookUps.GetFamilyStatus();
            var GenderList = _lookUps.GetGender();
            MyPagedList<TblPool> tblPool = _pool.GetPools(page, pageSize);
            foreach (var item in tblPool.model)
            {
                if (!string.IsNullOrEmpty(item.Gender))
                {
                    item.Gender = GenderList.Where(x => x.Id == Convert.ToInt64(item.Gender)).SingleOrDefault().Name;
                }
                if (!string.IsNullOrEmpty(item.FamilyStatus))
                {
                    item.FamilyStatus = FamilyStatusList.Where(x => x.Id == Convert.ToInt64(item.FamilyStatus)).SingleOrDefault().Name;
                }
                if (!string.IsNullOrEmpty(item.CityIds))
                {
                    string[] cityIdList = item.CityIds.Split(",");
                    int counter = 0;
                    foreach (var id in cityIdList)
                    {
                        if (counter < 1)
                        {
                            item.CityIds = CityList.Where(x => x.Id == Convert.ToInt64(id)).SingleOrDefault().Name;
                        }
                        else
                        {
                            item.CityIds = item.CityIds + "," + CityList.Where(x => x.Id == Convert.ToInt64(id)).SingleOrDefault().Name;
                        }
                        counter++;
                    }
                }
            }
            ViewBag.list = tblPool;
            ViewBag.PageSize = pageSize;
            ViewBag.PageIndex = page;

            return View(tblPool.model.ToList());
        }

        public IActionResult Participants()
        {
            UserViewModel model = new UserViewModel();
            model.lstUserModel = _pool.GetParticipant();
            return View(model);
        }

        public IActionResult Invitation()
        {
            return View();
        }
        public JsonResult GetPool(long Id = 0)
        {
            TblPool tblPool = _pool.GetPoolById(Id);
            if (tblPool != null)
            {
                return Json(new
                {
                    status = "success",
                    data = tblPool
                });
            }

            return Json(new
            {
                status = "fail",
                data = ""
            });
        }
    }
}
