using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using provaFenox.Models;
using provaFenox.IRepository;
using System.Data.SqlClient;
using System.Data;

namespace provaFenox.Data
{
    public class CoresRepository : IGenericRepository<Cores>
    {
        private readonly IConfiguration _configuration;
        public CoresRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Cores>> GetAllList()
        {
            List<Cores> _List = new List<Cores>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLConnection"))) {
                connection.Open();
                SqlCommand cmd = new SqlCommand("Select * from Cores", connection);
                cmd.CommandType= CommandType.Text;
                
                using (var dr = await cmd.ExecuteReaderAsync())
                 {
                    while (await dr.ReadAsync()) {
                        _List.Add(new Cores()
                        {
                            IdCores = Convert.ToInt32(dr["IdCores"]),
                            Descricao = dr["Descricao"].ToString(),
                            Status = Convert.ToBoolean(dr["StatusCores"])
                        });
                    }
                }
            }
            return _List;
        }

        public async Task<List<Cores>> GetList(Cores entity)
        {
            List<Cores> _List = new List<Cores>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLConnection"))){
                connection.Open();
                SqlCommand cmd = new SqlCommand("Select * from Cores where Descricao = Descricao and StatusCores = StatusCores", connection);
                cmd.CommandType = CommandType.Text;

                using(var dr = await cmd.ExecuteReaderAsync()){
                    while(await dr.ReadAsync()){
                        _List.Add(new Cores(){
                            IdCores = Convert.ToInt32(dr["IdCores"]),
                            Descricao = dr["Descricao"].ToString(),
                            // Status = Convert.ToBoolean(dr["StatusCores"])
                        });
                    }
                }
            }
            return _List;
        }

        public async Task<bool> Save(Cores entity)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("insert into Cores values (@Descricao, @Status);", connection);
                // cmd.Parameters.AddWithValue("IdCores",entity.IdCores);
                cmd.Parameters.AddWithValue("@Descricao",entity.Descricao);
                cmd.Parameters.AddWithValue("@Status",entity.Status);
                cmd.CommandType = CommandType.Text;

                int affectedRows = await cmd.ExecuteNonQueryAsync();

                if (affectedRows > 0)
                    return true;
                else
                    return false;
            }
        }
        public async Task<bool> Edit(Cores entity)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("update Cores set Descricao = @Descricao, StatusCores = @Status where idCores = @IdCores", connection);
                cmd.Parameters.AddWithValue("@IdCores", entity.IdCores);
                cmd.Parameters.AddWithValue("@Descricao", entity.Descricao);
                cmd.Parameters.AddWithValue("@Status", entity.Status);

                cmd.CommandType = CommandType.StoredProcedure;

                int affectedRows = await cmd.ExecuteNonQueryAsync();

                if (affectedRows > 0)
                    return true;
                else
                    return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("Delete from Cores where IdCores = @IdCores", connection);
                cmd.Parameters.AddWithValue("@IdCores", id);
                cmd.CommandType = CommandType.StoredProcedure;

                int affectedRows = await cmd.ExecuteNonQueryAsync();

                if (affectedRows > 0)
                    return true;
                else
                    return false;
            }
        }
    }
}