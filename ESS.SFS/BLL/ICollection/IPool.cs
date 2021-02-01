using ESS.SFS.DAL;
using ESS.SFS.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESS.SFS.BLL.ICollection
{
    public interface IPool
    {
        MyPagedList<TblPool> GetPools(int page = 1, int pageSize = 5);
        TblPool GetPoolById(long Id);
        string Save(TblPool tblPool, ref long? Id);
        string Save(TblPool tblPool);
        string DeletePool(long Id);
        List<TblPool> GetPools();

        List<TblUser> GetParticipant();
    }
}
