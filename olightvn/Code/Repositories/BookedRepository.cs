using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using olightvn.Models;
using Dapper;
using T.Core.Authenticate;

namespace olightvn.Repositories
{
    public class BookedRepository:BaseRepository,IBookedRepository
    {
        public IEnumerable<Booked> GetAllByUser(string userNanme)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Booked>(@"
                    exec sp_GetProductBooked @Id=@userNanme
                    ".AddParametersDefaul(), new { userNanme });
            }
        }

        public IEnumerable<Booked> GetAll()
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Booked>(@"
                    exec sp_GetProductBooked
                    ".AddParametersDefaul());
            }
        }

        public Booked GetInfo(object id)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Booked>(@"
                    exec sp_GetProductBooked @Id=@id                   
                    ".AddParametersDefaul(), new { id }).FirstOrDefault();
            }
        }

        public bool Insert(Booked entity, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object id, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Update(Booked entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"
                    exec sp_UpdateBooked @Id=@Id,@OrdererName = @OrdererName,@OrdererPhone = @OrdererPhone,@ReceiverName = @ReceiverName,@ReceiverAddress = @ReceiverAddress
                    ,@ReceiverPhone = @ReceiverPhone,@ReceiverEmail = @ReceiverEmail,@Note = @Note,@ReceiveLocation = @ReceiveLocation,@StartDTS = @StartDTS
                    ,@EndDTS = @EndDTS,@Quantity = @Quantity,@ActiveStatus = @ActiveStatus,@UserName =@UserName,@UserType =@UserType                   
                    ".AddParametersDefaul(), new { entity, userLogin }).Any();
            }
        }
        public bool UpdateProcess(Booked entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"
                    exec sp_UpdateBooked @Id=@Id,@ProcessStatus = @ProcessStatus,@UserName =@UserName,@UserType = @UserType                  
                    ".AddParametersDefaul(), new { entity, userLogin }).Any();
            }
        }

        public bool Insert(int id, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"
                    exec sp_TransferBasketToBooked @Id=@id                 
                    ".AddParametersDefaul(), new { id }).Any();
            }
        }
    }
}
