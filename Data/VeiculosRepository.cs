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
        public async Task<List<Veiculos>> GetList()
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
        public Task<bool> Save(Veiculos entity)
        {
            throw new NotImplementedException();
        }
        public Task<bool> Edit(Veiculos entity)
        {
            throw new NotImplementedException();
        }
        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}