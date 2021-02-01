using ESS.SFS.BLL.ICollection;
using ESS.SFS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESS.SFS.BLL.Collection
{
    public class Form:IForm
    {
        public SmartFamilySurveyContext _context;
        public Form(SmartFamilySurveyContext context)
        {
            _context = context;
        }
        public List<TblForm> GetForms()
        {
           return _context.TblForm.Where(x=>x.IsActive.HasValue==true).ToList();
        }
        public TblForm GetFormByGoogleId(string googleId)
        {
            return _context.TblForm.Where(x => x.GoogleFormId == googleId).FirstOrDefault();
        }
        public string ImportData(List<TblForm> tblForms)
        {
            try
            {
                List<TblForm> oldList =_context.TblForm.Where(x => x.IsActive.HasValue == true).ToList();
                foreach (var item in tblForms)
                {
                    if (oldList.Where(x=>x.Name==item.Name).ToList().Count == 0)
                    {
                        _context.TblForm.Add(item);
                    }
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "";
    }
        public void addFormForTesting(TblForm tblForm)
        {
            _context.TblForm.Add(tblForm);
            _context.SaveChanges();
        }
}
}
