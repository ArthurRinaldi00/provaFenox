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
    public class VeiculosRepository : IGenericRepository<Veiculos>
    {
        private readonly IConfiguration _configuration;
        public VeiculosRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Veiculos>> GetAllList()
        {
            List<Veiculos> _List = new List<Veiculos>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLConnection"))) {
                connection.Open();
                SqlCommand cmd = new SqlCommand("Select * from Veiculos", connection);
                cmd.CommandType= CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync()) {
                    while (await dr.ReadAsync()) {
                        _List.Add(new Veiculos()
                        {
                            IdVeiculo = Convert.ToInt32(dr["IdVeiculo"]),
                            Placa = dr["Placa"].ToString(),
                            Renavam = dr["Renavam"].ToString(),
                            NChassi = dr["NChassi"].ToString(),
                            NMotor = dr["NMotor"].ToString(),
                            Marca = dr["Marca"].ToString(),
                            Modelo = dr["Modelo"].ToString(),
                            Combustivel = new Combustivel(){
                                IdCombustivel = Convert.ToInt32(dr["IdCombustivel"]),
                                Descricao = dr["Descricao"].ToString(),
                                Status = Convert.ToBoolean(dr["StatusCombustivel"])
                            },
                            Cor = new Cores(){
                                IdCores = Convert.ToInt32(dr["IdCores"]),
                                Descricao = dr["Descricao"].ToString(),
                                Status = Convert.ToBoolean(dr["StatusCores"])
                            },
                            ano = dr["ano"].ToString(),
                            Status = Convert.ToBoolean(dr["StatusVeiculo"])
                        });
                    }
                }
            }
            return _List;
        }

        public async Task<List<Veiculos>> GetList(Veiculos entiry)
        {
            List<Veiculos> _List = new List<Veiculos>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLConnection"))){
                connection.Open();
                SqlCommand cmd = new SqlCommand("Select * from Veiculos", connection);
                cmd.CommandType = CommandType.Text;

                using(var dr = await cmd.ExecuteReaderAsync()){
                    while(await dr.ReadAsync()){
                        _List.Add(new Veiculos(){
                            IdVeiculo = Convert.ToInt32(dr["IdVeiculo"]),
                            Placa = dr["Placa"].ToString(),
                            Renavam = dr["Renavam"].ToString(),
                            NChassi = dr["NChassi"].ToString(),
                            NMotor = dr["NMotor"].ToString(),
                            Marca = dr["Marca"].ToString(),
                            Modelo = dr["Modelo"].ToString(),
                            Combustivel = new Combustivel(){
                                IdCombustivel = Convert.ToInt32(dr["IdCombustivel"]),
                                Descricao = dr["Descricao"].ToString(),
                                Status = Convert.ToBoolean(dr["StatusCombustivel"])
                            },
                            Cor = new Cores(){
                                IdCores = Convert.ToInt32(dr["IdCores"]),
                                Descricao = dr["Descricao"].ToString(),
                                Status = Convert.ToBoolean(dr["StatusCores"])
                            },
                            ano = dr["ano"].ToString(),
                            Status = Convert.ToBoolean(dr["StatusVeiculo"])
                        });
                    }
                }
            }
            return _List;
        }
        public async Task<bool> Save(Veiculos entity)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("ProcInsertVeiculo", connection);
                cmd.Parameters.AddWithValue("Placa",entity.Placa);
                cmd.Parameters.AddWithValue("Renavam", entity.Renavam);
                cmd.Parameters.AddWithValue("NChassi", entity.NChassi);
                cmd.Parameters.AddWithValue("NMotor", entity.NMotor);
                cmd.Parameters.AddWithValue("Marca", entity.Marca);
                cmd.Parameters.AddWithValue("Modelo", entity.Modelo);
                cmd.Parameters.AddWithValue("IdCombustivel", entity.Combustivel.IdCombustivel);
                cmd.Parameters.AddWithValue("IdCores", entity.Cor.IdCores);
                cmd.Parameters.AddWithValue("Ano", entity.ano);
                cmd.Parameters.AddWithValue("StatusVeiculo", entity.Status);
                cmd.CommandType = CommandType.StoredProcedure;

                int affectedRows = await cmd.ExecuteNonQueryAsync();

                if (affectedRows > 0)
                    return true;
                else
                    return false;
            }
        }
        public async Task<bool> Edit(Veiculos entity)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("ProcUpdateVeiculo", connection);
                cmd.Parameters.AddWithValue("IdVeiculo",entity.IdVeiculo);
                cmd.Parameters.AddWithValue("Placa",entity.Placa);
                cmd.Parameters.AddWithValue("Renavam", entity.Renavam);
                cmd.Parameters.AddWithValue("NChassi", entity.NChassi);
                cmd.Parameters.AddWithValue("NMotor", entity.NMotor);
                cmd.Parameters.AddWithValue("Marca", entity.Marca);
                cmd.Parameters.AddWithValue("Modelo", entity.Modelo);
                cmd.Parameters.AddWithValue("IdCombustivel", entity.Combustivel.IdCombustivel);
                cmd.Parameters.AddWithValue("IdCores", entity.Cor.IdCores);
                cmd.Parameters.AddWithValue("Ano", entity.ano);
                cmd.Parameters.AddWithValue("StatusVeiculo", entity.Status);
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
                SqlCommand cmd = new SqlCommand("Delete from Veiculo where IdVeiculo = @IdVeiculo", connection);
                cmd.Parameters.AddWithValue("@IdVeiculo", id);
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