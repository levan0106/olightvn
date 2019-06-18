using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using olightvn.Models;
using Dapper;
using System.Data;

namespace olightvn.Repositories
{
    public class EmailRepository:BaseRepository, IEmailRepository
    {
        public IEnumerable<Email> GetAll()
        {
            throw new NotImplementedException();
        }

        public Email GetInfo(object id)
        {
            using (var connection = GetConnection())
            {
                var result = connection.Query(
                    @"sp_GetEmailConfig".AddParametersDefaul(firstParam:true)).ToList();
                Email emailConfig= new Email();
                foreach(var item in result)
                {
                    if (item.Key == "From")
                        emailConfig.From = item.Value;
                    else if (item.Key == "To")
                        emailConfig.To = item.Value;
                    else if (item.Key == "Password")
                        emailConfig.Password = item.Value;
                    else if (item.Key == "EnableSSL")
                        emailConfig.EnableSSL = int.Parse(item.Value);
                    else if (item.Key == "SMTPPort")
                        emailConfig.SMTPPort = int.Parse(item.Value);
                }
                return emailConfig;
            }
        }

        public bool Insert(Email entity, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object id, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Update(Email entity, string userLogin)
        {
            using(var connection = GetConnection())
            {
                return connection.Query(
                    @" sp_UpdateEmailConfig  @To=@To, @From =@From, @Password =@Password, @SMTPPort =@SMTPPort, @EnableSSL = @EnableSSL
                    ".AddParametersDefaul(),entity ).Any();
            }
        }
    }
}