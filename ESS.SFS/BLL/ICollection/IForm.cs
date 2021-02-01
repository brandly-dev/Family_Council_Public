using ESS.SFS.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESS.SFS.BLL.ICollection
{
    public interface IForm
    {
        List<TblForm> GetForms();
        string ImportData(List<TblForm> tblForms);
        void addFormForTesting(TblForm tblForm);
        TblForm GetFormByGoogleId(string googleId);
    }
}
