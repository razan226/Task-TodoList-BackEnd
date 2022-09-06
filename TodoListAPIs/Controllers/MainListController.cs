using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using TodoListAPIs.DAL.Repositories;
using Prometheus;
using BenchmarkDotNet.Reports;
using TodoListAPIs.Models.Dtos;
using App.Metrics.Formatters.Prometheus;
using Swashbuckle.AspNetCore.Annotations;

namespace TodoListAPIs.Controllers
{
    [Route("api/mainList")]
    [ApiController]
    public class MainListController : ControllerBase
    {
        private readonly IMainListRepository _MainListRepository;

        public MainListController(IMainListRepository MainListRepository)
        {
            _MainListRepository = MainListRepository;

        }
        [SwaggerOperation(Summary = "Get all MainLists")]
        [HttpGet]
        public async Task<ActionResult<List<MainListData>>> GetMainLists()
        {
            try
            {
                return Ok(await _MainListRepository.GetMainLists());

            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message + "Inner Ex: " + e.InnerException.Message);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message + "Inner Ex: " + e.InnerException.Message);

            }
        }

        [SwaggerOperation(Summary = "Get all MainList")]
        [HttpGet("{id}")]
        public async Task<ActionResult<List<MainListData>>> GetMainList(Guid id)
        {
            try
            {
                return Ok(await _MainListRepository.GetMainListById(id));

            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message + "Inner Ex: " + e.InnerException.Message);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message + "Inner Ex: " + e.InnerException.Message);

            }
        }

        [SwaggerOperation(Summary = "Create new MainList")]
        [HttpPost]
        public async Task<ActionResult<MainListData>> PostMainList(MainListData MainListData)
        {
            try
            {
                MainListData createdMainList = await _MainListRepository.CreateMainList(MainListData);


                return CreatedAtAction("GetMainList", new { id = createdMainList.Id }, createdMainList);
            }

            catch (NullReferenceException e)
            {
                return NotFound(e.Message + "Inner Ex: " + e.InnerException.Message);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message + "Inner Ex: " + e.InnerException.Message);

            }
        }

        [SwaggerOperation(Summary = "Delete an MainList")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMainList(Guid id)
        {
            try
            {
                await _MainListRepository.DeleteMainList(id);
                return Ok("Deleted Sucsessfully");

            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message + "Inner Ex: " + e.InnerException.Message);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message + "Inner Ex: " + e.InnerException.Message);

            }
        }
    }
}
