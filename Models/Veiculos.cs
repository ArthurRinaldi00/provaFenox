using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace provaFenox.Models
{
    public class Veiculos
    {
        public int IdVeiculo { get; set;}
        public string Placa { get; set;}
        public string Renavam { get; set;}
        public string NChassi { get; set;}
        public string NMotor { get; set;}
        public string Marca { get; set;}
        public string Modelo { get; set;}
         public Combustivel Combustivel { get; set;}
         public Cores Cor { get; set;}
        public string ano { get; set;}
        public Boolean Status { get; set;}
    }
}