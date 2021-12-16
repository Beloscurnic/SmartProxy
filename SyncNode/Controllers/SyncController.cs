using SyncNode.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyncNode.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncNode.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        private  readonly SyncWorkService _workService;
        public SyncController(SyncWorkService workService)
        {
            _workService = workService;
        }
        [HttpPost]
        public IActionResult Sync(SyncEntity entity)
        {
            _workService.AddItem(entity);

            return Ok();
        }
    }
}
