using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using olightvn.Models;
using Dapper;
using T.Core.Authenticate;
using System.Dynamic;
using System.ComponentModel;
using T.Core.Common;

namespace olightvn.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public User User { get; set; }
        public UserRepository()
        {
            if (User == null)
                User = new User();
        }

        public User Authenticate(string userName, string password)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<User>(@"
                    sp_AuthenticateUser @UserName=@userName, @Password=@password
                ".AddParametersDefaul(), new { userName, password }).FirstOrDefault();
            }
        }
        public IEnumerable<User> GetAll()
        {
            using (var connection= GetConnection())
            {
                return connection.Query<User>(@"SELECT * FROM SEC_User".AddParametersDefaul()).ToList();
            }
        }
        public IEnumerable<User> GetAllByParent(string userName)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<User>(@"SELECT * FROM SEC_User WHERE ParentId=@userName".AddParametersDefaul(), new { userName }).ToList();
            }
        }

        public User GetInfo(object id)
        {
            using (var connection= GetConnection())
            {
                return connection.Query<User>(@"
                sp_GetUserById @UserName=@id
                ".AddParametersDefaul(), new { id }).FirstOrDefault();
            }
        }

        public bool Insert(User User, string userLogin)
        {
            using (var connection = GetConnection())
            {
                connection.Execute(@"
                sp_InsertUser @UserName=@UserName, @Password=@Password, @FullName=@FullName ,@FirstName=@FirstName, @LastName=@LastName, @Email=@Email
                                    ,@SecurityAnswer=@SecurityAnswer,@SecurityQuestion=@SecurityQuestion,@ActiveStatus=@ActiveStatus,@Role=@Role,@Sex=@Sex
                ".AddParametersDefaul(), User);

                return connection.Query(@"
                sp_GetUserById @UserName=@UserName
                ".AddParametersDefaul(), new { User.UserName }).Any();
            }
        }

        public bool Delete(object id, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                    exec sp_DeleteUser @UserName=@id".AddParametersDefaul(), new { id });
                    return true;
                }
                catch
                {
                    return false;
                }
                
            }
        }

        public bool Update(User User, string userLogin)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Execute(@"
                     sp_InsertUser @UserName=@UserName, @Password=@Password, @FullName=@FullName ,@FirstName=@FirstName, @LastName=@LastName, @Email=@Email
                                    ,@SecurityAnswer=@SecurityAnswer,@SecurityQuestion=@SecurityQuestion,@ActiveStatus=@ActiveStatus,@Role=@Role
                                    , @UpdateBy=@RecId,@Sex=@Sex
                    ".AddParametersDefaul(), User);
                    return true;
                }
                catch
                {
                    return false;
                }
                
            }
        }


        public IEnumerable<User> GetAllByRole(string roleName)
        {
            using (var connection = GetConnection())
            {
              
                return connection.Query<User>(@"
                     sp_GetUserByRole @Role=@roleName
                ".AddParametersDefaul(), new { roleName });

            }
        }

        public IEnumerable<User> GetAllByPermission(string permissionName)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<User>(@"
                     sp_GetUserByPermission @Permission=@permissionName".AddParametersDefaul()
                    , new { permissionName });

            }
        }

        public IEnumerable<User> GetAll(Paging paging)
        {
             using (var connection = GetConnection())
            {
                return connection.Query<User>(@"
                EXEC sp_GetAllUsers @PageSize=@PageSize,@Page=@CurrentPage,@GetTotalRow=@GetTotalRow,@SortBy=@SortBy
                ".AddParametersDefaul(), new { paging.PageSize, paging.CurrentPage, paging.GetTotalRow, paging.SortBy }).ToList();
            }
        }

        public int GetAll_ToTal(Paging paging)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<int>(@"
                EXEC sp_GetAllUsers @PageSize=@PageSize,@Page=@CurrentPage,@GetTotalRow=@GetTotalRow,@SortBy=@SortBy
                ".AddParametersDefaul(), new { paging.PageSize, paging.CurrentPage, paging.GetTotalRow, paging.SortBy }).FirstOrDefault();
            }
        }

        public bool ValidateUserEmail(string userName, string email)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"
                EXEC sp_ValidateUserEmail @UserName=@userName,@Email=@Email
                ".AddParametersDefaul(), new { userName, email }).Any();
            }
        }
    }
}