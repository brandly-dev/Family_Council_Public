using ESS.SFS.DAL;
using ESS.SFS.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESS.SFS.BLL.ICollection
{
    public interface ISurvey
    {
        string SendSurvey(TblPoolSurvey tblPoolSurvey);
        long AddSurvey(TblSurvey tblSurvey);
        MyPagedList<TblSurvey> GetArchiveSurvey(int page = 1, int pageSize = 5);
        List<TblPoolSurvey> GetPoolBySurveyId(long Id);
        MyPagedList<TblSurvey> GetActiveSurvey(int page = 1, int pageSize = 5);
        string SendSurveyFromPool(long surveyId, long PoolId,string userIds,TblPool tblPool);
        string DeleteSurvey(long Id);
        long UpdateSurveyDueDate(long id, DateTime updatedDate);
        MyPagedList<TblSurvey> GetArchiveSurvey(string text, int page = 1, int pageSize = 5);
    }
}
