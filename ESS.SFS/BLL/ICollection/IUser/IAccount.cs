using ESS.SFS.DAL;
using ESS.SFS.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESS.SFS.BLL.ICollection.IUser
{
    public interface IAccount
    {
        int getUser();

        List<TblCity> getCities();

        TblUser getUserByID(Int64? Id);

        TblUser updateUser(UserViewModel user);

        TblUser saveSignUp(UserViewModel user);

        TblUser updateAdminUser(UserViewModel user);

        TblUser getUserByEmail(string Email);

        bool updateUserStatus(Int64? Id);

        bool updatePassword(Int64? Id, string Password);
    }
}
