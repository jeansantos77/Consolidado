using Consolidado.API.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Consolidado.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsolidadoController : ControllerBase
    {
        private readonly ILactoConsolidadoService _lactoConsolidadoService;

        public ConsolidadoController(ILactoConsolidadoService lactoConsolidadoService)
        {
            _lactoConsolidadoService = lactoConsolidadoService;
        }

        [HttpGet]
        public IActionResult GetAll(DateTime startDate, DateTime endDate)
        {
            return Ok(_lactoConsolidadoService.GetByRangeDate(startDate, endDate));
        }
    }
}
