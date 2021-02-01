using System;
using System.Collections.Generic;
using System.Text;

namespace ESS.SFS.Helper
{

    public class Constants
    {
        public static string ForgetPassword = "the forgot password email has been sent successfully.";

        public static string InvitationSend = "invitation has been sent successfully.";

        public static string InvalidInvitationRequest = "invalid invitation request.";

        public static string VerificationeEmailSend = "we have sent the verification email on your email.";

        public static string VerificationEmailSendMessage = "user not exist.";

        public static string VerificationCompleted = "user approved.";

        public static string InvalidPassword = "invalid password.";

        public static string  ExistUser = "user is already exist.";

        public static string UpdateUserProfile = "user profile have been updated successfully.";

        public static string UpdateAdminProfile = "admin profile have been updated successfully.";

        public static string InvalidUser = "invalid user.";

        public static string SurveyDetailMessage = "your request has been submitted successfully.";

        public static string RequestSendApproval = "the request has been sent for approval.";

        public static string PaymentApproved = "payment approved.";

        public static string FormSaved = "template has been saved successfully.";

        public static string FormUpdated = "template has been updated successfully.";

        public static string templateDelete = "template has been deleted successfully.";

        public static string PasswordReset = "password has been reset successfully.";
        public static string GooglePageIFrame = "<iframe src='<<url>>viewform?embedded=true' width='640' height='943' frameborder='0' marginheight='0' marginwidth='0'>Loading…</iframe>";
       // public static string GooglePageIFrame = "<div id='ff-compose'></div><script async defer src = 'https://formfacade.com/include/111125699878020615623/form/<<url>>/classic.js?div=ff-compose' ></ script > ";
    }

    public static class InvitationStatuses
    {
        public static string Invited { get { return "Invited"; } }
        public static string Approved { get { return "Approved"; } }

    }

    public static class UserRole
    {
        public static string Admin { get { return "Admin"; } }
        public static string User { get { return "User"; } }

    }

    public static class PaymentStatus
    {
        public static string Pending { get { return "Pending"; } }
        public static string Approved { get { return "Approved"; } }

    }

    public static class PaymentType
    {
        public static string GiftCard { get { return "Gift Card"; } }
        public static string Money { get { return "Money"; } }

    }

    public static class MaritalStatuses
    {
        public static string Single { get { return "1"; } }
        public static string Married { get { return "2"; } }
        public static string Children { get { return "3"; } }
    }

}
