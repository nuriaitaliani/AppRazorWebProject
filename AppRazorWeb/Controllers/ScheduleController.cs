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
    [Route("api/controller")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {

        #region Fields

        private readonly IScheduleBusinessService _scheduleBusinessService;

        #endregion Fields

        #region Cosntructors

        public ScheduleController(IScheduleBusinessService scheduleBusinessService)
        {
            this._scheduleBusinessService = scheduleBusinessService;
        }

        #endregion Constructors

        #region Deletes

        /// <summary>
        /// Deletes a schedule
        /// </summary>
        /// <param name="scheduleId">ScheduleHeader identifier</param>
        /// <response code = "204">ScheduleHeader deleted. There's no content to return</response>
        /// <response code = "400">Execution result with error</response>
        /// <response code = "404">The schedule doesn't exist</response>
        /// <response code = "500">Unexpected problems. Please contact support team</response>
        [HttpDelete("{scheduleId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ExecutionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSchedule(Guid scheduleId)
        {

            try
            {
                IExecutionResult result = await _scheduleBusinessService.DeleteSchedule(scheduleId);
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
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Internal server error: {exception.Message}");
            }

        }

        #endregion Deletes

        #region Gets

        /// <summary>
        /// Get a schedule by a given identifier
        /// </summary>
        /// <param name="scheduleId">ScheduleHeader identifier</param>
        /// <response code = "200">Returns the schedule</response>
        /// <response code = "400">Execution result with error</response>
        /// <response code = "404">The schedule doesn't exist</response>
        /// <response code = "500">Unexpected problems. Please contact support team</response>
        [HttpGet("{scheduleId:guid}")]
        [ProducesResponseType(typeof(ScheduleHeader), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExecutionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSchedule(Guid scheduleId)
        {
            try
            {
                IExecutionResult result = await _scheduleBusinessService.GetSchedule(scheduleId);

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
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Internal server error: {exception.Message}");
            }
        }

        /// <summary>
        /// Get a list of schedules
        /// Filters may be applied
        /// </summary>
        /// <param name="filters">ScheduleHeader filters</param>
        /// <response code = "200">Returns the list of students</response>
        /// <response code = "400">Execution result with error</response>
        /// <response code = "401">Unauthorized</response>
        /// <response code = "500">Unexpected problems. Please contact support team</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<ScheduleHeader>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IExecutionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSchedules([FromQuery] ScheduleFilters filters)
        {
            try
            {
                IExecutionResult result = await _scheduleBusinessService.GetSchedules(filters);
                if (!result.Success)
                {
                    return BadRequest(result);
                }
                return Ok(result.Result);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Internal server error: {exception.Message}");
            }
        }

        #endregion Gets

        #region Puts

        /// <summary>
        /// Creates or updates a schedule
        /// </summary>
        /// <param name="schedule">ScheduleHeader tobe created or updated</param>
        /// <response code = "201">Returns the created schedule</response>
        /// <response code = "202">Returns the updated schedule</response>
        /// <response code = "400">Execution result with error</response>
        /// <response code = "500">Unexpected problems. Please contact support team</response>
        [HttpPut]
        [ProducesResponseType(typeof(ScheduleHeader), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ScheduleHeader), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(IExecutionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOrUpdateSchedule([FromBody] ScheduleWriteModel schedule)
        {

            try
            {
                bool create = false;
                IExecutionResult result = await _scheduleBusinessService.GetSchedule(schedule.Id);
                if (!result.Success) {
                    if (result.ErrorType == Enums.ErrorType.NotFound)
                    {
                        result = await _scheduleBusinessService.CreateSchedule(schedule);
                    }
                    else
                    {
                        return BadRequest(result);
                    }
                }
                else
                {
                    result = await _scheduleBusinessService.UpdateSchedule(schedule);
                    create = false;
                }

                if (!result.Success)
                {
                    return BadRequest(result);
                }
                else
                {
                    if (create)
                    {
                        return CreatedAtAction(nameof(GetSchedule), new { scheduleId = schedule.Id }, schedule);
                    }
                    else
                    {
                        return AcceptedAtAction(nameof(GetSchedule), new {scheduleId = schedule.Id}, schedule);
                    }
                }
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Internal server error: {exception.Message}");
            }

        }

        #endregion Puts

    }
}
