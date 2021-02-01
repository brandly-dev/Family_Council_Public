using ESS.SFS.BLL.ICollection;
using ESS.SFS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESS.SFS.BLL.Collection
{
    public class LookUps : ILookUps
    {
        public SmartFamilySurveyContext _context;
        public LookUps(SmartFamilySurveyContext context)
        {
            _context = context;
        }
        public List<TblCity> GetCity()
        {
            return _context.TblCity.ToList();
        }
        public List<TblFamilyStatus> GetFamilyStatus()
        {
            return _context.TblFamilyStatus.ToList();
        }
        public List<TblGender> GetGender()
        {
            return _context.TblGender.ToList();
        }
    }
}
