using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResultCommunication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppRazorWeb.Framework.BusinessService;
using AppRazorWeb.Framework.BusinessService.Models;
using AppRazorWeb.Framework.Dataservices.Filters;

namespace AppRazorWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        #region Fields

        private readonly IUserBusinessService _userBusinessService;

        #endregion Fields

        #region Constructor

        public UserController(IUserBusinessService userBusinessService)
        {
            _userBusinessService = userBusinessService;
        }

        #endregion Constructor

        #region Deletes

        /// <summary>
        /// Deletes a UserHeader
        /// </summary>
        /// <param name="userId">UserHeader identifier</param>
        /// <reponse code = "204">UserHeader deleted. There's no content to return</reponse>
        /// <response code = "400">Execution result with error</response>
        /// <response code = "404">The user doesn't exist</response>
        /// <response code = "500">Unexpected problems. Please contact suport team</response>
        [HttpDelete("{userId:guid}")]                                        //Esto se pone porque la eliminación se hace por el Id
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IExecutionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            try
            {
                IExecutionResult result = await _userBusinessService.DeleteUser(userId);
                if (!result.Success)
                {
                    if (result.ErrorType == Enums.ErrorType.NotFound)
                    {
                        return NotFound();
                    }
                    return BadRequest(result);
                }
                return NoContent();
            }
            catch (Exception exception)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError, $"Internal server error: {exception.Message}");
            }
        }

        #endregion Deletes

        #region Puts

        /// <summary>
        /// Creates or updates a user
        /// </summary>
        /// <param name="user"></param>
        /// <response code = "201">Returns the created user</response>
        /// <response code = "202">Returns the updated user</response>
        /// <response code = "400">Execution result with error</response>
        /// <response code = "500">Unexpected problems. Please contact support team</response>
        [HttpPut]                 //Esto se pone así por que la inserción o actualización es por el user completo
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(User), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(IExecutionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //Se pone FromBody para hacer referencia a todas las propiedades de una clase y así crearlas o actualizarlas
        public async Task<IActionResult> CreateOrUpdateUser([FromBody] UserWriteModel user)
        {
            try
            {
                bool create = true;
                IExecutionResult result = await _userBusinessService.GetUser(user.Id);
                if (!result.Success)
                {
                    //Busca el user para ver si ya existe
                    if (result.ErrorType == Enums.ErrorType.NotFound)
                    {
                        result = await _userBusinessService.CreateUser(user);
                    }
                    else
                    {
                        //Si existe no se puede volver a crear
                        return BadRequest(result);
                    }
                }
                else
                {
                    result = await _userBusinessService.UpdateUser(user);
                    create = false;
                }

                if (!result.Success)
                {
                    //Devuelve el tipo de error en el result
                    return BadRequest(result);
                }
                else
                {
                    if (create)
                    {
                        //Accedemos al método Get del UsersController
                        //Le metemos los valores al user para crearlo
                        return CreatedAtAction(nameof(GetUser), new { userId = user.Id }, user);
                    }
                    else
                    {
                        //Le metemos valores al user para ser actualizado
                        return AcceptedAtAction(nameof(GetUser), new { userId = user.Id }, user);
                    }
                }

            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {exception.Message}");
            }
        }

        #endregion Puts

        #region Gets

        /// <summary>
        /// Get a user by a given identifier
        /// </summary>
        /// <param name="userId">UserHeader identifier</param>
        /// <response code ="200">Returns the user</response>
        /// <response code="400">Execution resul with error</response>
        /// <response code="404">The user doesn't exist</response>
        /// <response code="500">Unexpected problems. Please contact support team</response>
        [HttpGet("{userId:guid}")]                      //Esto se pone así porque la devolución se hace por Id específico
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(User), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            try
            {
                IExecutionResult result = await _userBusinessService.GetUser(userId);
                if (!result.Success)
                {
                    if (result.ErrorType == Enums.ErrorType.NotFound)
                    {
                        return NotFound();
                    }
                    return BadRequest(result);
                }
                return Ok(result.Result);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {exception.Message}");
            }
        }

        /// <summary>
        /// Get a list of users
        /// Filters may be applied
        /// </summary>
        /// <param name="filters">UserHeader filters</param>
        /// <response code = "200">Returns the list of users</response>
        /// <response code = "400">Execution result with error</response>
        /// <response code = "500">Unexpected problems. Please contact support team</response>
        [HttpGet]                                          //Esto se pone así porque devuelve todos los datos del user
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IExecutionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //Se pone el FromQuery dado que hace referencia a los filtros
        public async Task<IActionResult> GetUsers([FromQuery] UserFilters filters)
        {
            try
            {
                IExecutionResult result = await _userBusinessService.GetUsers(filters);

                if (!result.Success)
                {
                    return BadRequest(result);
                }
                return Ok(result.Result);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {exception.Message}");
            }
        }

        #endregion Gets

    }
}
