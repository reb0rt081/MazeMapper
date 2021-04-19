using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MazeMapper.Core;
using MazeMapper.Domain;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Unity;

namespace MazeMapper.Web.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MazeMapperController : ControllerBase
    {
        private readonly IMazeMapManager mazeMapManager;

        public MazeMapperController(IMazeMapManager _mazeMapManager)
        {
            mazeMapManager = _mazeMapManager;
        }

        [HttpGet]
        public string Get()
        {
            return "rbo";
        }
    }
}