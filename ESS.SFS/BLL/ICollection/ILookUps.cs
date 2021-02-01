using ESS.SFS.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESS.SFS.BLL.ICollection
{
    public interface ILookUps
    {
        List<TblCity> GetCity();
        List<TblFamilyStatus> GetFamilyStatus();
       List<TblGender> GetGender();
    }
}
