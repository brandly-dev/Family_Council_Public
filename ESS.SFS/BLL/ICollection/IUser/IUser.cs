using ESS.SFS.DAL;
using ESS.SFS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESS.SFS.BLL.ICollection.IUser
{
    public interface IUser
    {
         TblUser saveUserInvitation(TblUser user);
        Int64 userExist(TblUser user);


        PagerResultForSurvey getActiveSurvey(Int64? userId, int page, int pageSize);
        PagerResultForSurvey getArchiveSurvey(Int64? userId, string FromDate, string ToDate, int page, int pageSize);

        TblSurvey GetActiveSurveyDetail(string sid);

        bool UpdateSurveyDetail(Int64? sid, Int64? uid);

        bool SendPaymentRequest(Int64? uid, string PaymentType, Int32? RequestedPoint);

        List<TblPayment> GetPayments(Int64? uid, string PaymentStatus);

        TblUser GetUser(Int64? uid);

        bool RequestedPayment(string Points, string PaymentType, Int64? userID);

        List<TblPayment> GetPaymentForPending(string FromDate, string ToDate);

        List<TblPayment> GetPaymentForArchive(string FromDate, string ToDate);

       bool updatePaymentStatus(Int64? paymentId);

        List<TblForm> GetForms(Int64? count);

        bool SaveForm(TblForm form);

        bool UpdateForm(TblForm form);

        string deleteForm(Int64? Id);

        TblUserSurvey getSurveyUser(Int64? userId);
        TblUser getUserByEmail(string email);
        TblUserSurvey getSurveryIdForClientForm(long userId, long formId);

    }
}
