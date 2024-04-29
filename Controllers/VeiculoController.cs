using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using provaFenox.IRepository;
using provaFenox.Models;

namespace provaFenox.Controllers
{
    [Route("[controller]/Veiculos/")]
    public class VeiculoController : Controller
    {
        private readonly ILogger<VeiculoController> _logger;
        private readonly IGenericRepository<Veiculos> _veiculo;

        public VeiculoController(
            ILogger<VeiculoController> logger,
            IGenericRepository<Veiculos> veiculos)
        {
            _logger = logger;
            _veiculo = veiculos;
        }


       [HttpGet]
        public async Task<IActionResult> GetCoresList([FromBody] Veiculos veiculos)
        {
            List<Veiculos> _veiculoList = await _veiculo.GetList(veiculos);
            return StatusCode(StatusCodes.Status200OK, _veiculoList);
        }

        [HttpPost]
        public async Task<IActionResult> InsertCores([FromBody] Veiculos veiculos)
        {
            bool _result = await _veiculo.Save(veiculos);
            if(_result)
                return StatusCode(StatusCodes.Status200OK, new { value = _result, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = _result, msg = "error" });
        }

        [HttpPut]
        public async Task<IActionResult> EditCores([FromBody] Veiculos veiculos)
        {
            bool _result = await _veiculo.Edit(veiculos);
            if (_result)
                return StatusCode(StatusCodes.Status200OK, new { value = _result, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = _result, msg = "error" });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCores(int idVeiculos)
        {
            bool _result = await _veiculo.Delete(idVeiculos);
            if (_result)
                return StatusCode(StatusCodes.Status200OK, new { value = _result, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = _result, msg = "error" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}