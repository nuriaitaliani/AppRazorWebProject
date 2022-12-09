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
    public class ActivityController : ControllerBase
    {

        #region Fields

        private readonly IActivityBusinessService _activityBusinessService;

        #endregion Fields

        #region Constructors

        public ActivityController(IActivityBusinessService activityBusinessService)
        {
            _activityBusinessService = activityBusinessService;
        }

        #endregion Constructors

        #region Deletes

        /// <summary>
        /// Deletes an ActivityHeader
        /// </summary>
        /// <param name="activityId">ActivityHeader identifier</param>
        /// <reponse code = "204">ActivityHeader deleted. There's no content to return</reponse>
        /// <response code = "400">Execution result with error</response>
        /// <response code = "404">The activity doesn't exist</response>
        /// <response code = "500">Unexpected problems. Please contact suport team</response>
        [HttpDelete("{activityId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IExecutionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteActivity(Guid activityId)
        {
            try
            {
                IExecutionResult result = await _activityBusinessService.DeleteActivity(activityId);
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
                    StatusCodes.Status500InternalServerError,
                    $"Internal server error: {exception.Message}");
            }
        }

        #endregion Deletes

        #region Puts

        /// <summary>
        /// Creates or updates an activity
        /// </summary>
        /// <param name="activity"></param>
        /// <response code = "201">Returns the created activity</response>
        /// <response code = "202">Returns the updated activity</response>
        /// <response code = "400">Execution result with error</response>
        /// <response code = "500">Unexpected problems. Please contact support team</response>
        [HttpPut]                 //Esto se pone así por que la inserción o actualización es por el activity completo
        [ProducesResponseType(typeof(Activity), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Activity), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(IExecutionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //Se pone FromBody para hacer referencia a todas las propiedades de una clase y así crearlas o actualizarlas
        public async Task<IActionResult> CreateOrUpdateActivity([FromBody] ActivityClassWriteModel activity)
        {
            try
            {
                bool create = true;
                IExecutionResult result = await _activityBusinessService.GetActivity(activity.Id);
                if (!result.Success)
                {
                    //Busca la disciplina para ver si ya existe
                    if (result.ErrorType == Enums.ErrorType.NotFound)
                    {
                        //En caso de no existir la crea
                        result = await _activityBusinessService.CreateActivity(activity);
                    }
                    else
                    {
                        //Si existe no se puede volver a crear
                        return BadRequest(result);
                    }
                }
                else
                {
                    result = await _activityBusinessService.UpdateActivity(activity);
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
                        //Accedemos al método Get del ActivityController
                        //Le metemos los valores al activity para ser creado
                        return CreatedAtAction(nameof(GetActivity), new { activityId = activity.Id }, activity);
                    }
                    else
                    {
                        //Le metemos valores al activity para ser actualizado
                        return AcceptedAtAction(nameof(GetActivity), new { activityId = activity.Id }, activity);
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
        /// Get an activity by a given identifier
        /// </summary>
        /// <param name="activityId">ActivityHeader identifier</param>
        /// <response code ="200">Returns the activity</response>
        /// <response code="400">Execution resul with error</response>
        /// <response code="404">The activity doesn't exist</response>
        /// <response code="500">Unexpected problems. Please contact support team</response>
        [HttpGet("{activityId:guid}")]                      //Esto se pone así porque la devolución se hace por Id específico
        [ProducesResponseType(typeof(Activity), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Activity), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetActivity(Guid activityId)
        {
            try
            {
                IExecutionResult result = await _activityBusinessService.GetActivity(activityId);
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
        /// Get a list of activities
        /// Filters may be applied
        /// </summary>
        /// <param name="filters">ActivityHeader filters</param>
        /// <response code = "200">Returns the list of activities</response>
        /// <response code = "400">Execution result with error</response>
        /// <response code = "500">Unexpected problems. Please contact support team</response>
        [HttpGet]                                          //Esto se pone así porque devuelve todos los datos del activity
        [ProducesResponseType(typeof(List<Activity>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IExecutionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //Se pone el FromQuery dado que hace referencia a los filtros
        public async Task<IActionResult> GetActivities([FromQuery] ActivityFilters filters)
        {
            try
            {
                IExecutionResult result = await _activityBusinessService.GetActivities(filters);

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
