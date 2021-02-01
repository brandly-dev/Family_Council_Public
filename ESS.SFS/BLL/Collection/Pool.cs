using ESS.SFS.BLL.ICollection;
using ESS.SFS.DAL;
using ESS.SFS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESS.SFS.BLL.Collection
{
    public class Pool : IPool
    {
        public SmartFamilySurveyContext _context;
        public Pool(SmartFamilySurveyContext context)
        {
            _context = context;
        }
        public MyPagedList<TblPool> GetPools(int page = 1, int pageSize = 5)
        {
            var skip = (page - 1) * pageSize;
            MyPagedList<TblPool> pagelist = new MyPagedList<TblPool>();
            pagelist.model = _context.TblPool.OrderByDescending(i=>i.Id).Skip(skip).Take(pageSize).ToList();
            pagelist.page = page;
            pagelist.pageSize = pageSize;
            pagelist.rowCount = _context.TblPool.Count();
            var pageCount = (double)pagelist.rowCount / pageSize;
            pagelist.pageCount = (int)Math.Ceiling(pageCount);
            return pagelist;
        }
        public List<TblPool> GetPools()
        {
            return _context.TblPool.ToList(); 
        }
        public TblPool GetPoolById(long Id)
        {
            return _context.TblPool.Find(Id);
        }
        public string Save(TblPool tblPool, ref long? Id)
        {
            try
            {
                if (tblPool.Id>0)
                { 
                    _context.Update(tblPool);
                    _context.SaveChanges();
                }
                else
                {
                    _context.TblPool.Add(tblPool);
                    _context.SaveChanges();
                    Id = tblPool.Id;
                }
                return "";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        public string Save(TblPool tblPool)
        {
            try
            {
                if (tblPool.Id > 0)
                {
                    _context.Update(tblPool);
                    _context.SaveChanges();
                }
                else
                {
                    _context.TblPool.Add(tblPool);
                    _context.SaveChanges();
                }
                return "";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        public string DeletePool(long Id)
        {
            try
            {
                TblPool tblPool = GetPoolById(Id);
                if(tblPool != null)
                {
                  _context.Remove(tblPool);
                    _context.SaveChanges();

                }
                return "";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public List<TblUser> GetParticipant()
        {
            return _context.TblUser.OrderByDescending(I => I.Id).ToList();
        }

    }
}
