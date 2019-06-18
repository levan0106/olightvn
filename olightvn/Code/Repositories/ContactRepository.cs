using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using olightvn.Models;
using Dapper;

namespace olightvn.Repositories
{
    public class ContactRepository : BaseRepository, IContactRepository
    {
        public IEnumerable<Contact> GetAll()
        {
            throw new NotImplementedException();
        }

        public Contact GetInfo(object id)
        {
            using(var connection=GetConnection())
            {
                return connection.Query<Contact>(@"
                sp_GetContact
                ".AddParametersDefaul(firstParam: true)).FirstOrDefault();
            }
        }

        public bool Insert(Contact entity, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object id, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Update(Contact entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"
                sp_UpdateContact 
                            @ContactID = @ContactID
                            ,@Name=@Name
							,@Address=@Address
							,@Fax=@Fax
							,@Phone=@Phone
							,@Email=@Email
                            ,@Facebook=@Facebook
                            ,@Youtube=@Youtube
                            ,@Map_Label=@Map_Label
                            ,@Map_Lng=@Map_Lng
                            ,@Map_Lat=@Map_Lat
                            ,@Zoom=@Zoom
                ".AddParametersDefaul(), entity).Any();
            }
        }
    }
}