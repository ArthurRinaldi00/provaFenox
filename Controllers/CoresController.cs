using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using provaFenox.IRepository;
using provaFenox.Models;

namespace provaFenox.Controllers
{
    public class CoresController : Controller
    {
        
        private readonly ILogger<CoresController> _logger;
        private readonly IGenericRepository<Cores> _Cores;

        public CoresController(
            ILogger<CoresController> logger,
            IGenericRepository<Cores> cores
            )
        {
            _logger = logger;
            _Cores = cores;
        }

        [HttpGet]
        public async  Task<IActionResult> GetAllCoresList()
        {
            List<Cores> _AllCoresList = await _Cores.GetAllList();
            return StatusCode(StatusCodes.Status200OK, _AllCoresList);
        }

        [HttpGet]
        public async Task<IActionResult> GetCoresList([FromBody] Cores cores)
        {
            List<Cores> _coresList = await _Cores.GetList(cores);
            return StatusCode(StatusCodes.Status200OK, _coresList);
        }

        [HttpPost]
        public async Task<IActionResult> InsertCores(Cores cores)
        {
            bool _result = await _Cores.Save(cores);
            if(_result)
                return StatusCode(StatusCodes.Status200OK, new { value = _result, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = _result, msg = "error" });
        }

        [HttpPut]
        public async Task<IActionResult> EditCores([FromBody] Cores cores)
        {
            bool _result = await _Cores.Edit(cores);
            if (_result)
                return StatusCode(StatusCodes.Status200OK, new { value = _result, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = _result, msg = "error" });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCores(int idCores)
        {
            bool _result = await _Cores.Delete(idCores);
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