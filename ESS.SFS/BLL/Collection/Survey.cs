using ESS.SFS.BLL.ICollection;
using ESS.SFS.DAL;
using ESS.SFS.ViewModel;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESS.SFS.BLL.Collection
{
    public class Survey:ISurvey
    {
        private readonly SmartFamilySurveyContext _context;
        private readonly IPool _pool;
        public Survey(SmartFamilySurveyContext context, IPool pool)
        {
            _context = context;
            _pool = pool;
        }
        public string SendSurvey(TblPoolSurvey tblPoolSurvey)
        {
            long? poolId = 0;
            if (tblPoolSurvey.IsNewTemplate==true)
            {
                tblPoolSurvey.PoolTemplate.Id = 0;
                _pool.Save(tblPoolSurvey.PoolTemplate,ref poolId);
            }
            else
            {
                poolId = tblPoolSurvey.PoolTemplateId;
            }
            tblPoolSurvey.Survey.Participants = tblPoolSurvey.PoolTemplate.Participant;
            tblPoolSurvey.Survey.CreatedDate = System.DateTime.Now;
            long id = AddSurvey(tblPoolSurvey.Survey);
            TblPoolSurvey poolSurvey = new TblPoolSurvey();
              poolSurvey.Id = 0;
              poolSurvey.SurveyId= id;
            poolSurvey.PoolTemplateId = poolId;
            poolSurvey.CreatedDate = DateTime.Now;
            long poolSurveyId = addJunctionEntery(poolSurvey);
            List<TblUser> tblUsers;
            if (tblPoolSurvey.PoolTemplate.AgeMin == null)
            {
                tblUsers = _context.TblUser.FromSqlRaw("SearchUserSurvey @p0, @p1, @p2, @p3, @p4, @p5, @p6", tblPoolSurvey.PoolTemplate.Participant, "", "", tblPoolSurvey.PoolTemplate.CityIds, tblPoolSurvey.PoolTemplate.Gender, tblPoolSurvey.PoolTemplate.FamilyStatus, null).ToList();
            }
            else
            {
                tblUsers = _context.TblUser.FromSqlRaw("SearchUserSurvey @p0, @p1, @p2, @p3, @p4, @p5, @p6", tblPoolSurvey.PoolTemplate.Participant, tblPoolSurvey.PoolTemplate.AgeMin, tblPoolSurvey.PoolTemplate.AgeMax, tblPoolSurvey.PoolTemplate.CityIds, tblPoolSurvey.PoolTemplate.Gender, tblPoolSurvey.PoolTemplate.FamilyStatus, null).ToList();
            }
            // var tblUsers = _context.Database.ExecuteSqlCommand("SearchUserSurvey @p0,@p1,@p2,@p3,@p4,@p5", 1,10,45,null,null,null).ToList();
            foreach (var item in tblUsers)
            {
                SendToUser(id, poolSurveyId, item.Id);
               
            }
            string htmlString = ESS.SFS.Helper.Common.SendToSurveyHtml;
            htmlString = htmlString.Replace("[SurveyName]", tblPoolSurvey.Survey.Title);
            string body = "";
            foreach (var item in tblUsers)
            {
                body = htmlString.Replace("[Name]", item.FullName);

                   ESS.SFS.Helper.Common.sendEmailFromAWS(body, "New Smart Family Survey Received", item.Username ,"");
            }

            return "";
        }
        public long AddSurvey(TblSurvey tblSurvey)
        {
            _context.TblSurvey.Add(tblSurvey);
            _context.SaveChanges();
            return tblSurvey.Id;
        }
        private long addJunctionEntery(TblPoolSurvey poolSurvey)
        {
            _context.TblPoolSurvey.Add(poolSurvey);
            _context.SaveChanges();
            return poolSurvey.Id;
        }
        private void SendToUser (long surveyId, long poolSurveyId, long userId)
        {
            TblUserSurvey tblUserSurvey = new TblUserSurvey();
            tblUserSurvey.SurveyId = surveyId;
            tblUserSurvey.PoolSurveyId = poolSurveyId;
            tblUserSurvey.UserId = userId;

            _context.TblUserSurvey.Add(tblUserSurvey);
            _context.SaveChanges();
            //return true;

        }
        public MyPagedList<TblSurvey> GetActiveSurvey(int page=1,int pageSize=5)
        {
            var skip = (page - 1) * pageSize;
            MyPagedList<TblSurvey> pagelist= new MyPagedList<TblSurvey>();
            var lstResult = _context.TblSurvey.Where(x => x.DueDate.Value.Date >= DateTime.Now.Date && x.IsDeleted != true).OrderByDescending(x=>x.CreatedDate).ToList();
            pagelist.model = lstResult.Skip(skip).Take(pageSize).ToList();
            pagelist.page = page;
            pagelist.pageSize = pageSize;
            pagelist.rowCount = _context.TblSurvey.Where(x => x.DueDate.Value.Date >= DateTime.Now.Date && x.IsDeleted != true).Count();
            var pageCount = (double)pagelist.rowCount / pageSize;
            pagelist.pageCount = (int)Math.Ceiling(pageCount); 
            return pagelist;
        }
        public MyPagedList<TblSurvey> GetArchiveSurvey(int page = 1, int pageSize = 5)
        {
            var skip = (page - 1) * pageSize;
            MyPagedList<TblSurvey> pagelist = new MyPagedList<TblSurvey>();
            var lstResult = _context.TblSurvey.Where(x => x.DueDate.Value.Date < DateTime.Now.Date && x.IsDeleted != true).OrderByDescending(x => x.CreatedDate).ToList();
            pagelist.model = lstResult.Skip(skip).Take(pageSize).ToList();
            pagelist.page = page;
            pagelist.pageSize = pageSize;
            pagelist.rowCount = _context.TblSurvey.Where(x => x.DueDate.Value.Date < DateTime.Now.Date && x.IsDeleted != true).Count();
            var pageCount = (double)pagelist.rowCount / pageSize;
            pagelist.pageCount = (int)Math.Ceiling(pageCount);
            return pagelist;
        }
        public MyPagedList<TblSurvey> GetArchiveSurvey( string text,int page = 1, int pageSize = 5)
        
        {
            var skip = (page - 1) * pageSize;
            MyPagedList<TblSurvey> pagelist = new MyPagedList<TblSurvey>();
            pagelist.model = _context.TblSurvey.Where(x => x.DueDate.Value.Date < DateTime.Now.Date && x.IsDeleted != true ).ToList();
            pagelist.model = pagelist.model.Skip(skip).Take(pageSize).ToList();
            pagelist.model = pagelist.model.Where(x =>x.Title.Contains(text)).ToList();
            pagelist.page = page;
            pagelist.pageSize = pageSize;
            pagelist.rowCount = _context.TblSurvey.Where(x => x.DueDate.Value.Date < DateTime.Now.Date && x.IsDeleted != true ).Count();
            var pageCount = (double)pagelist.rowCount / pageSize;
            pagelist.pageCount = (int)Math.Ceiling(pageCount);
            return pagelist;
        }
        public List<TblPoolSurvey> GetPoolBySurveyId(long Id)
        {
            List<TblPoolSurvey> tblPoolSurveys = _context.TblPoolSurvey.Where(x => x.SurveyId == Id).Include(x => x.PoolTemplate).Include(x => x.Survey).Include(x=>x.TblUserSurvey).ToList();
            return tblPoolSurveys;
        }
        public string SendSurveyFromPool(long surveyId, long PoolId,string ids,TblPool tblPool)
        {
            TblPoolSurvey poolSurvey = new TblPoolSurvey();
            poolSurvey.Id = 0;
            poolSurvey.SurveyId = surveyId;
            poolSurvey.PoolTemplateId = PoolId;
            poolSurvey.CreatedDate = DateTime.Now;
            long poolSurveyId = addJunctionEntery(poolSurvey);

            List<TblUser> tblUsers;
            if (tblPool.AgeMin == null)
            {
             tblUsers = _context.TblUser.FromSqlRaw("SearchUserSurvey @p0, @p1, @p2, @p3, @p4, @p5, @p6",tblPool.Participant,"","",tblPool.CityIds,tblPool.Gender,tblPool.FamilyStatus, ids).ToList();
            }
            else
            {
               tblUsers = _context.TblUser.FromSqlRaw("SearchUserSurvey @p0, @p1, @p2, @p3, @p4, @p5, @p6", tblPool.Participant, tblPool.AgeMin, tblPool.AgeMax, tblPool.CityIds, tblPool.Gender, tblPool.FamilyStatus, ids).ToList();
            }
            foreach (var item in tblUsers)
            {
                SendToUser(surveyId, poolSurveyId, item.Id);
            }
            string htmlString = ESS.SFS.Helper.Common.SendToSurveyHtml;
            var tblSurvey=_context.TblSurvey.Find(surveyId);
            htmlString = htmlString.Replace("[SurveyName]", tblSurvey.Title);
            string body = "";
            foreach (var item in tblUsers)
            {
                body = htmlString.Replace("[Name]", item.FullName);
                   ESS.SFS.Helper.Common.sendEmailFromAWS(body, "New Smart Family Survey Received", item.Username,"");
            }
            return "";
        }
        public string DeleteSurvey(long Id)
        {
            TblSurvey tblSurvey = _context.TblSurvey.Where(x => x.Id == Id).SingleOrDefault();
            tblSurvey.IsDeleted = true;
            _context.Update(tblSurvey);
            _context.SaveChanges();
            return "";
        }
        public long UpdateSurveyDueDate(long id,DateTime updatedDate)
        {
            TblSurvey tblSurvey = _context.TblSurvey.Where(x => x.Id == id).SingleOrDefault();
            TblSurvey newSurvey = new TblSurvey();
            newSurvey.FormId = tblSurvey.FormId;
            newSurvey.Title = tblSurvey.Title;
            newSurvey.Points = tblSurvey.Points;
            newSurvey.Participants = tblSurvey.Participants;
            newSurvey.CreatedDate = tblSurvey.CreatedDate;
            newSurvey.DueDate = updatedDate;
            _context.TblSurvey.Add(newSurvey);
            _context.SaveChanges();
            return newSurvey.Id;
        }
    }
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }

        public int FirstRowOnPage
        {

            get { return (CurrentPage - 1) * PageSize + 1; }
        }

        public int LastRowOnPage
        {
            get { return Math.Min(CurrentPage * PageSize, RowCount); }
        }
    }

    public class PagedResult<T> : PagedResultBase where T : class
    {
        public IList<T> Results { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }
    }
}
