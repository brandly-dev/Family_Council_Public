using ESS.SFS.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESS.SFS.ViewModel
{
    public class PagerResultForSurvey
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }

        public List<TblUserSurvey> lstSurvey { get; set; }
    }
}
