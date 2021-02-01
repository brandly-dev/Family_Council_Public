using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESS.SFS.BLL.ICollection.IUser;
using ESS.SFS.DAL;
using ESS.SFS.Helper;
using MatSource.Library.Helper;
using Microsoft.AspNetCore.Mvc;

namespace SmartFamilySurvey.WebUI.Controllers
{
    [AuthorizeFilter]
    public class googleFormController : Controller
    {
        public IUser _user;
        public googleFormController(IUser user)
        {
            _user = user;
        }
        public IActionResult Index(Int64? count)
        {
            ViewBag.lstForms = _user.GetForms(count);
            return View();
        }
        public IActionResult iframeView()
        {
            return View();
        }

        public IActionResult SaveUpdate(Int64? Id, string Name, string CreatedDate, string Url)
        {
            TblForm form = new TblForm();
            form.Name = Name;
            form.CreatedDate = Convert.ToDateTime(CreatedDate);
            form.Url = Url;
            if (Id > 0)
            {
                form.Id = Convert.ToInt64(Id);
                if (_user.UpdateForm(form))
                {
                    TempData["Success"] = Constants.FormUpdated;
                }
            }
            else
            {
                if (_user.SaveForm(form))
                {

                    TempData["Success"] = Constants.FormSaved;
                }
            }
            return View("Index");
        }


        public string DeleteForm(Int64? Id)
        {
            string message = _user.deleteForm(Id);
            TempData["Success"] = message;
            return message;
        }
    }
}
