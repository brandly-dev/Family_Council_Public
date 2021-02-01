using ESS.SFS.BLL.ICollection.IUser;
using ESS.SFS.DAL;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using ESS.SFS.ViewModel;
using ESS.SFS.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace ESS.SFS.BLL.Collection.User
{
    public class User : IUser
    {
        public SmartFamilySurveyContext _context;

        public User(SmartFamilySurveyContext context)
        {
            _context = context;
        }

        public TblUser saveUserInvitation(TblUser user)
        {
            TblUser objUser = null;
            if (!string.IsNullOrEmpty(user.Username))
            {
                objUser = _context.TblUser.Where(i => i.Username.Trim() == user.Username.Trim()).FirstOrDefault();
                if (objUser == null)
                {
                    _context.TblUser.Add(user);
                    _context.SaveChanges();
                    return user;
                }
                else
                {
                    return objUser;
                }
            }
            return objUser;
        }

        public Int64 userExist(TblUser user)
        {
            return _context.TblUser.Where(i => i.Username.Trim() == user.Username.Trim()).Count();
        }

        public PagerResultForSurvey getActiveSurvey(Int64? userId, int page, int pageSize)
        {
            PagerResultForSurvey objResult = new PagerResultForSurvey();
            List<TblUserSurvey> lstSurvey;
            if (page == 0)
                page = 1;

            if (pageSize == 0)
                pageSize = int.MaxValue;

            var skip = (page - 1) * pageSize;
            lstSurvey = _context.TblUserSurvey.Include(x => x.Survey).Where(x => x.UserId == userId && x.Survey.DueDate.Value.Date >= DateTime.Now && x.IsCompleted == null).OrderByDescending(x => x.Survey.CreatedDate).ToList();
            lstSurvey = lstSurvey.Skip(skip).Take(pageSize).ToList();
            objResult.RowCount = _context.TblUserSurvey.Include(x => x.Survey).Where(x => x.UserId == userId && x.Survey.DueDate.Value.Date >= DateTime.Now && x.IsCompleted == null).OrderByDescending(x => x.Survey.CreatedDate).Count();
            var pageCount = (double)objResult.RowCount / pageSize;
            objResult.PageCount = (int)Math.Ceiling(pageCount);
            objResult.lstSurvey = lstSurvey;
            return objResult;
        }

        public PagerResultForSurvey getArchiveSurvey(Int64? userId, string FromDate, string ToDate, int page, int pageSize)
        {

            PagerResultForSurvey objResult = new PagerResultForSurvey();
            string fromDate = String.Format("{0:MM/dd/yyyy}", FromDate);
            string toDate = String.Format("{0:MM/dd/yyyy}", ToDate);
            List<TblUserSurvey> lstSurvey;
            if (page == 0)
                page = 1;

            if (pageSize == 0)
                pageSize = int.MaxValue;

            var skip = (page - 1) * pageSize;
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                lstSurvey = _context.TblUserSurvey.Include(x => x.Survey).Where(x => x.UserId == userId && ((x.Survey.DueDate.Value.Date >= Convert.ToDateTime(fromDate) && x.Survey.DueDate.Value.Date <= Convert.ToDateTime(toDate)) || x.IsCompleted == true)).OrderByDescending(i => i.Survey.CreatedDate).ToList();
                lstSurvey = lstSurvey.Skip(skip).Take(pageSize).ToList();
                objResult.RowCount = _context.TblUserSurvey.Include(x => x.Survey).Where(x => x.UserId == userId && ((x.Survey.DueDate.Value.Date >= Convert.ToDateTime(fromDate) && x.Survey.DueDate.Value.Date <= Convert.ToDateTime(toDate)) || x.IsCompleted == true)).OrderByDescending(i => i.Survey.CreatedDate).Count();
            }
            else
            {
                lstSurvey = _context.TblUserSurvey.Include(x => x.Survey).Where(x => x.UserId == userId && (x.Survey.DueDate.Value.Date <= DateTime.Now || x.IsCompleted == true)).OrderByDescending(i => i.Survey.CreatedDate).ToList();
                lstSurvey = lstSurvey.Skip(skip).Take(pageSize).ToList();
                objResult.RowCount = _context.TblUserSurvey.Include(x => x.Survey).Where(x => x.UserId == userId && (x.Survey.DueDate.Value.Date <= DateTime.Now || x.IsCompleted == true)).OrderByDescending(i => i.Survey.CreatedDate).Count();
            }

            var pageCount = (double)objResult.RowCount / pageSize;
            objResult.PageCount = (int)Math.Ceiling(pageCount);
            objResult.lstSurvey = lstSurvey;
            return objResult;
        }

        public TblSurvey GetActiveSurveyDetail(string sid)
        {
            Int64? SurveyId = Convert.ToInt64(Common.Decryption(sid));
            return _context.TblSurvey.Where(i => i.Id == SurveyId).Include(i => i.Form).FirstOrDefault();
        }

        public bool UpdateSurveyDetail(Int64? sid, Int64? uid)
        {
            TblUserSurvey userSurvey = _context.TblUserSurvey.Include(i => i.Survey).Where(i => i.SurveyId == sid && i.UserId == uid).FirstOrDefault();
            TblUser user = _context.TblUser.Where(i => i.Id == uid).FirstOrDefault();
            if (userSurvey != null)
            {
                userSurvey.IsCompleted = true;
                userSurvey.CompletedDate = DateTime.Now;
                if (userSurvey.Survey != null)
                {
                    userSurvey.AwardedPoints = userSurvey.Survey.Points;
                    if (user != null)
                    {
                        if (user.CurrentCreditPoints == null || user.CurrentCreditPoints == 0)
                        {
                            user.CurrentCreditPoints = userSurvey.Survey.Points;
                        }
                        else
                        {
                            user.CurrentCreditPoints = user.CurrentCreditPoints + userSurvey.Survey.Points;
                        }
                    }
                    if (userSurvey.Survey.SurveyAnswer == null || userSurvey.Survey.SurveyAnswer == 0)
                    {
                        userSurvey.Survey.SurveyAnswer = 1;
                    }
                    else
                    {
                        userSurvey.Survey.SurveyAnswer = userSurvey.Survey.SurveyAnswer + 1;
                    }
                }
            }

            return _context.SaveChanges() > 0 ? true : false;
        }


        public bool SendPaymentRequest(Int64? uid, string PaymentType, Int32? RequestedPoint)
        {
            TblPayment payment = new TblPayment();
            payment.UserId = uid;
            payment.RequestedDate = DateTime.Now;
            payment.RequestedPoints = RequestedPoint;
            payment.PaymentMethod = PaymentType;
            payment.Status = PaymentStatus.Pending;
            _context.TblPayment.Add(payment);
            return _context.SaveChanges() > 0 ? true : false;
        }

        public List<TblPayment> GetPayments(Int64? uid, string PaymentStatus)
        {
            List<TblPayment> lstPayment = new List<TblPayment>();

            lstPayment = _context.TblPayment.Where(i => i.UserId == uid).OrderByDescending(i => i.RequestedDate).ToList();

            if (!string.IsNullOrEmpty(PaymentStatus))
            {
                lstPayment = lstPayment.Where(i => i.Status == PaymentStatus).ToList();
            }
            return lstPayment;
        }

        public TblUser GetUser(Int64? uid)
        {
            return _context.TblUser.Where(I => I.Id == uid).FirstOrDefault();
        }

        public bool RequestedPayment(string Points, string PaymentType, Int64? userID)
        {
            TblPayment payment = new TblPayment();
            payment.PaymentMethod = PaymentType == "1" ? "gift card" : "money";
            payment.RequestedDate = DateTime.Now;
            payment.RequestedPoints = Convert.ToInt32(Points);
            payment.UserId = userID;
            payment.Status = PaymentStatus.Pending;
            _context.TblPayment.Add(payment);
            return _context.SaveChanges() > 0 ? true : false;
        }

        public List<TblPayment> GetPaymentForPending(string FromDate, string ToDate)
        {
            List<TblPayment> lstPayment;
            string SearchDate = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
            string fromDate = String.Format("{0:MM/dd/yyyy}", FromDate);
            string toDate = String.Format("{0:MM/dd/yyyy}", ToDate);

            lstPayment = _context.TblPayment.Where(x => x.Status == PaymentStatus.Pending).Include(i => i.User).OrderByDescending(i => i.RequestedDate).ToList();


            if (fromDate != null && ToDate != null)
            {
                lstPayment = lstPayment.Where(x => x.RequestedDate >= Convert.ToDateTime(fromDate) && x.RequestedDate <= Convert.ToDateTime(toDate)).ToList();
            }
            return lstPayment;

        }

        public List<TblPayment> GetPaymentForArchive(string FromDate, string ToDate)
        {
            List<TblPayment> lstPayment;
            string SearchDate = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
            string fromDate = String.Format("{0:MM/dd/yyyy}", FromDate);
            string toDate = String.Format("{0:MM/dd/yyyy}", ToDate);

            lstPayment = _context.TblPayment.Where(x => x.Status == PaymentStatus.Approved).Include(i => i.User).OrderByDescending(i => i.ApprovedDate).ToList();


            if (fromDate != null && ToDate != null)
            {
                lstPayment = lstPayment.Where(x => x.RequestedDate >= Convert.ToDateTime(fromDate) && x.RequestedDate <= Convert.ToDateTime(toDate)).ToList();
            }
            return lstPayment;
        }


        public bool updatePaymentStatus(Int64? paymentId)
        {
            TblPayment oldPayment = _context.TblPayment.Where(i => i.Id == paymentId).FirstOrDefault();
            if (oldPayment != null)
            {
                TblUser user = _context.TblUser.Where(i => i.Id == oldPayment.UserId).FirstOrDefault();
                if (user != null)
                {
                    user.CurrentCreditPoints = user.CurrentCreditPoints - oldPayment.RequestedPoints;
                }

                oldPayment.Status = PaymentStatus.Approved;
                oldPayment.ApprovedDate = DateTime.Now;
            }
            return _context.SaveChanges() > 0 ? true : false;
        }

        public List<TblForm> GetForms(Int64? count)
        {
            if (count > 0)
            {
                return _context.TblForm.OrderByDescending(i => i.Id).Take(Convert.ToInt32(count)).ToList();
            }
            else
            {
                return _context.TblForm.OrderByDescending(i => i.Id).ToList();
            }
        }

        public bool SaveForm(TblForm form)
        {
            form.IsActive = 1;
            _context.TblForm.Add(form);
            return _context.SaveChanges() > 0 ? true : false;
        }

        public bool UpdateForm(TblForm form)
        {
            TblForm oldForm = _context.TblForm.Where(i => i.Id == form.Id).FirstOrDefault();
            if (oldForm != null)
            {
                oldForm.Name = form.Name;
                oldForm.CreatedDate = form.CreatedDate;
                oldForm.Url = form.Url;
            }
            return _context.SaveChanges() > 0 ? true : false;
        }

        public string deleteForm(Int64? Id)
        {
            string message = string.Empty;
            try
            {
                TblForm form = _context.TblForm.Where(i => i.Id == Id).FirstOrDefault();
                if (form != null)
                {
                    _context.TblForm.Remove(form);
                }

                _context.SaveChanges();
                message = Constants.templateDelete;
            }
            catch (SqlException sqlEx)
            {
                //sqlEx.
            }
            catch (Exception ex)
            {
                message = "form is used within this system";
                // other exception...
            }
            return message;
        }

        public TblUserSurvey getSurveyUser(Int64? userId)
        {
            return _context.TblUserSurvey.Where(i => i.UserId == userId && i.IsCompleted == true && i.CompletedDate != null).OrderByDescending(i => i.CompletedDate).FirstOrDefault();
        }
        public TblUser getUserByEmail(string email)
        {
            return _context.TblUser.Where(x => x.Username == email).SingleOrDefault();
        }
        public TblUserSurvey getSurveryIdForClientForm(long userId,long formId)
        {
            return _context.TblUserSurvey.Where(x => x.UserId == userId && x.Survey.DueDate.Value.Date > DateTime.Now.Date && x.Survey.FormId==formId).FirstOrDefault();
        }
    }
}
