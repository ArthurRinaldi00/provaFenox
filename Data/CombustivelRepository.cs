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
    public class CombustivelRepository : IGenericRepository<Combustivel>
    {
        private readonly IConfiguration _configuration;
        public CombustivelRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<Combustivel>> GetList()
        {
            List<Combustivel> _List = new List<Combustivel>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLConnection"))){
                connection.Open();
                SqlCommand cmd = new SqlCommand("Select * from Combustivel", connection);
                cmd.CommandType = CommandType.Text;

                using(var dr = await cmd.ExecuteReaderAsync()){
                    while(await dr.ReadAsync()){
                        _List.Add(new Combustivel(){
                            IdCombustivel = Convert.ToInt32(dr["IdCombustivel"]),
                            Descricao = dr["Descricao"].ToString(),
                            Status = Convert.ToBoolean(dr["StatusCombustivel"])
                        });
                    }
                }
            }
            return _List;
        }
        public async Task<bool> Save(Combustivel entity)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("insert into Combustivel values (Descricao, Status);", connection);
                // cmd.Parameters.AddWithValue("IdCores",entity.IdCores);
                cmd.Parameters.AddWithValue("Descricao",entity.Descricao);
                cmd.Parameters.AddWithValue("StatusCombustivel",entity.Status);
                cmd.CommandType = CommandType.StoredProcedure;

                int affectedRows = await cmd.ExecuteNonQueryAsync();

                if (affectedRows > 0)
                    return true;
                else
                    return false;
            }
        }
        public async Task<bool> Edit(Combustivel entity)
        {
             using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("update Combustivel set Descricao = @Descricao, StatusCombustivel = @Status where idCombustivel = @IdCombustivel", connection);
                cmd.Parameters.AddWithValue("@IdCombustivel", entity.IdCombustivel);
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
                SqlCommand cmd = new SqlCommand("Delete from Combustivel where IdCombustivel = @IdCombustivel", connection);
                cmd.Parameters.AddWithValue("@IdCombustivel", id);
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