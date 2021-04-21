using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MazeMapper.Core;
using MazeMapper.Domain;

using Microsoft.AspNetCore.Cors;
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

            string mazeMapString = $"11111111{Environment.NewLine}10101001{Environment.NewLine}00100011{Environment.NewLine}11111110{Environment.NewLine}10010011{Environment.NewLine}11110001{Environment.NewLine}01010111{Environment.NewLine}*1011101";

            mazeMapManager.BuildMazeMapFromString(mazeMapString);
        }

        [HttpGet]
        public string Get()
        {
            mazeMapManager.SolveMazeAsync().Wait();

            return mazeMapManager.MazeMap.ToString();
        }
    }
}