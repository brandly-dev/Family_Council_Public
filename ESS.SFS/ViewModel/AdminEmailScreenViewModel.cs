using System;
using System.Collections.Generic;
using System.Text;

namespace ESS.SFS.ViewModel
{
    public class AdminEmailScreenViewModel
    {
        public string first_name { get; set; }
        public string old_email { get; set; }
        public string new_email { get; set; }
        public string link { get; set; }
        //--------------////
        public string email_verification_link { get; set; }
        /// <summary>
        public string msLogin { get; set; }
        /// </summary>
        public string date_time { get; set; }
        public string quote_ref_numb { get; set; }
        public string TemplateName { get; set; }

        public string customer_purchase_order_number { get; set; }
        public string supplier_first_name { get; set; }

        public string supplier_quote_ref { get; set; }
        public string CCEmail { get; set; }
        public string text { get; set; }
        public string customer_first_name { get; set; }
        public string ms_order_ref { get; set; }

        public string mspo_ref { get; set; }
        public string supplier_order_ref { get; set; }
        public string provisional_order_ref { get; set; }

        public string supplier_invoice_number { get; set; }

        public string supplier_payment { get; set; }

        public string customer_name { get; set; }

        public string company_name { get; set; }
        public string address_line1 { get; set; }

        public string address_line2 { get; set; }

        public string address_line3 { get; set; }

        public string town { get; set; }
        public string postcode { get; set; }
        public Boolean isSchedulerCall { get; set; }

        public Int64? BranchId { get; set; }

        public string body { get; set; }

        public string subject { get; set; }

        public string emailLogo { get; set; }
    }
}
