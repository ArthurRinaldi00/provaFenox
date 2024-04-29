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
    public class CombustivelController : Controller
    {
        private readonly ILogger<CombustivelController> _logger;
        private readonly IGenericRepository<Combustivel> _Combustivel;

        public CombustivelController(
            ILogger<CombustivelController> logger,
            IGenericRepository<Combustivel> _Combustivel
            )
        {
            _logger = logger;
            _Combustivel = _Combustivel;
        }

        [HttpGet]
        public async  Task<IActionResult> GetAllCombustivelList()
        {
            List<Combustivel> _AllCombustivelList = await _Combustivel.GetAllList();
            return StatusCode(StatusCodes.Status200OK, _AllCombustivelList);
        }

       [HttpGet]
        public async Task<IActionResult> GetCoresList([FromBody] Combustivel combustivel)
        {
            List<Combustivel> _combustivelList = await _Combustivel.GetList(combustivel);
            return StatusCode(StatusCodes.Status200OK, _combustivelList);
        }

        [HttpPost]
        public async Task<IActionResult> InsertCombustivel([FromBody] Combustivel combustivel)
        {
            bool _result = await _Combustivel.Save(combustivel);
            if(_result)
                return StatusCode(StatusCodes.Status200OK, new { value = _result, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = _result, msg = "error" });
        }

        [HttpPut]
        public async Task<IActionResult> EditCombustivel([FromBody] Combustivel combustivel)
        {
            bool _result = await _Combustivel.Edit(combustivel);
            if (_result)
                return StatusCode(StatusCodes.Status200OK, new { value = _result, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = _result, msg = "error" });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCombustivel(int idCombustivel)
        {
            bool _result = await _Combustivel.Delete(idCombustivel);
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