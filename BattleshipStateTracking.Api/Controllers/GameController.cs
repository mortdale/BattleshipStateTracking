using System;
using BattleshipStateTracking.Models;
using BattleshipStateTracking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BattleshipStateTracking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private readonly IBattlefieldService _battlefieldService;
        public GameController(ILogger<GameController> logger, IBattlefieldService battlefieldService)
        {
            _logger = logger;
            _battlefieldService = battlefieldService;
        }

        /// <summary>
        /// Create a new gaming board    
        /// </summary>
        /// <returns>HTTP OK</returns>
        [HttpPost]
        public IActionResult CreateBoard()
        {
            try
            {
                _battlefieldService.CreateBoard();
            }
            catch (ArgumentException ae)
            {
                return new BadRequestObjectResult(ae.Message);
            }
            catch (InvalidOperationException ie)
            {
                return new BadRequestObjectResult(ie.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }

            return Ok();
        }

        /// <summary>
        /// Add a battleship on to the current board    
        /// </summary>
        /// <param name="startX">Row of the start position of the battleship</param>
        /// <param name="startY">Column of the start position of the battleship</param>
        /// <param name="orientation">Orientation of the battleship</param>
        /// <param name="length">Length of the battleship</param>
        /// <returns>HTTP OK</returns>
        [HttpPost]
        public IActionResult AddBattleship(int startX, int startY, EnumOrientation orientation, int length)
        {
            try
            {
                _battlefieldService.AddBattleship(startX, startY, orientation, length);
            }
            catch (ArgumentException ae)
            {
                return new BadRequestObjectResult(ae.Message);
            }
            catch (InvalidOperationException ie)
            {
                return new BadRequestObjectResult(ie.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }

            return Ok();
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="x">Row of the attack position</param>
        /// <param name="y">Column of the attack position</param>
        /// <returns>Hit or Missing</returns>
        [HttpPost]
        public ActionResult<string> Attack(int x, int y)
        {
            try
            {
                var result = _battlefieldService.Attack(x, y);
                return result;
            }
            catch (ArgumentException ae)
            {
                return new BadRequestObjectResult(ae.Message);
            }
            catch (InvalidOperationException ie)
            {
                return new BadRequestObjectResult(ie.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
    }
}
