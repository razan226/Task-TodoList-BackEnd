using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using TodoListAPIs.DAL.Repositories;
using TodoListAPIs.Models.Dtos;

namespace TodoListAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubListController : ControllerBase
    {

        private readonly ISubListRepository _SubListRepository;

        public SubListController(ISubListRepository SubListRepository)
        {
            _SubListRepository = SubListRepository;

        }

        [SwaggerOperation(Summary = "Get all SubLists")]
        [HttpGet]
        public async Task<ActionResult<List<SubListData>>> GetSubLists()
        {
            try
            {
                return Ok(await _SubListRepository.GetSubLists());

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

        [SwaggerOperation(Summary = "Get SubList")]
        [HttpGet]
        public async Task<ActionResult<List<SubListData>>> GetSubList(Guid id)
        {
            try
            {
                return Ok(await _SubListRepository.GetSubListById(id));

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

        [SwaggerOperation(Summary = "Create new SubList")]
        [HttpPost]
        public async Task<ActionResult<SubListData>> PostItem(SubListData SubListData)
        {
            try
            {
                SubListData createdSubList = await _SubListRepository.CreateSubList(SubListData);


                return CreatedAtAction("GetSubList", new { id = createdSubList.Id }, createdSubList);
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

        [SwaggerOperation(Summary = "Delete an SubList")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubList(Guid id)
        {
            try
            {
                await _SubListRepository.DeleteSublist(id);
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
