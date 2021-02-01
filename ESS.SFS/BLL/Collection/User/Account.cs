using ESS.SFS.BLL.ICollection.IUser;
using ESS.SFS.DAL;
using ESS.SFS.Helper;
using ESS.SFS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace ESS.SFS.BLL.Collection.User
{
    public class Account : IAccount
    {
        public SmartFamilySurveyContext _context;
        public Account(SmartFamilySurveyContext context)
        {
            _context = context;
        }
        public int getUser()
        {
            var counter = _context.TblUser.ToList().Count;
            return counter;
        }

        public List<TblCity> getCities()
        {
            return _context.TblCity.OrderBy(I => I.Name).ToList();
        }

        public TblUser getUserByID(Int64? Id)
        {
            return _context.TblUser.Where(i => i.Id == Id).FirstOrDefault();
        }


        public TblUser updateUser(UserViewModel user)
        {
            TblUser oldUser = _context.TblUser.Where(i => i.Id == user.Id).FirstOrDefault();
            if (oldUser != null)
            {
                if(!string.IsNullOrEmpty(user.updatePassword))
                {
                    oldUser.Password = Common.EncryptPassword(user.updatePassword);
                }
                oldUser.MaritalStatus = Convert.ToInt32(user.maritalStatus);
                oldUser.FullName = user.surname;
                oldUser.Gender = user.gender;
                oldUser.Age = user.age;
                oldUser.Country = user.country;
                oldUser.City = Convert.ToInt64(user.city);
                oldUser.Status = InvitationStatuses.Approved;
                if(!string.IsNullOrEmpty(user.profilePic))
                {
                    oldUser.ProfilePicture = user.profilePic;
                }
                _context.SaveChanges();
            }
            return oldUser;
        }

        public TblUser saveSignUp(UserViewModel user)
        {
            TblUser oldUser = _context.TblUser.Where(i => i.Id == user.Id).FirstOrDefault();
            if (oldUser != null)
            {
                if (!string.IsNullOrEmpty(user.password))
                {
                    oldUser.Password = Common.EncryptPassword(user.password);
                }
                oldUser.MaritalStatus = Convert.ToInt32(user.maritalStatus);
                oldUser.FullName = user.surname;
                oldUser.Gender = user.gender;
                oldUser.Age = user.age;
                oldUser.Country = user.country;
                oldUser.City = Convert.ToInt64(user.city);
                oldUser.Role = UserRole.User;
                oldUser.Status = InvitationStatuses.Approved;
                if (!string.IsNullOrEmpty(user.profilePic))
                {
                    oldUser.ProfilePicture = user.profilePic;
                }
                _context.SaveChanges();
            }
            return oldUser;
        }

        public TblUser updateAdminUser(UserViewModel user)
        {
            TblUser oldUser = _context.TblUser.Where(i => i.Id == user.Id).FirstOrDefault();
            if (oldUser != null)
            {
                if (!string.IsNullOrEmpty(user.updatePassword))
                {
                    oldUser.Password = Common.EncryptPassword(user.updatePassword);
                }
                oldUser.FullName = user.surname;
                _context.SaveChanges();
            }
            return oldUser;
        }

        public TblUser getUserByEmail(string Email)
        {
            return _context.TblUser.Where(i => i.Username.Trim() == Email.Trim()).FirstOrDefault();
        }

        public bool updateUserStatus(Int64? Id)
        {
            bool updateStatus = false;
            TblUser oldUser = _context.TblUser.Where(i => i.Id == Id).FirstOrDefault();
            if (oldUser != null)
            {
                oldUser.Status = InvitationStatuses.Approved;
                _context.SaveChanges();
                updateStatus = true;
            }
            return updateStatus;
        }


        public bool updatePassword(Int64? Id, string Password)
        {
            bool updateStatus = false;
            TblUser oldUser = _context.TblUser.Where(i => i.Id == Id).FirstOrDefault();
            if (oldUser != null)
            {
                oldUser.Password = Password;
                _context.SaveChanges();
                updateStatus = true;
            }
            return updateStatus;
        }

    }
}
