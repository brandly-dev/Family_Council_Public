using System;
using System.Collections.Generic;
using System.Text;

namespace ESS.SFS.ViewModel
{
    public class MyPagedList<T>
    {

       
        public int page { get; set; }
        public int pageSize { get; set; }
        public int rowCount { get; set; }
        public int pageCount { get; set; }
        public List<T> model {get;set;}

    }
}
