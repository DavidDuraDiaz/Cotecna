using AutoMapper;
using CotecnaB.Abstractions.Interfaces.UnitsOfWork;
using CotecnaB.Core.DTOs;
using CotecnaB.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CotectaB.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspectorController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        public InspectorController(IMapper mapper, IUnitOfWork unitOfWork, ILoggerFactory logFactory)
        {
            _logger = logFactory.CreateLogger<InspectorController>();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id:Guid}", Name = "InspectorById")]
        public async Task<IActionResult> FindAsync(Guid id)
        {
            try
            {
                Inspector result = await _unitOfWork.Inspector.FindAsync(id);
                if (result == null)
                {
                    _logger.LogError($"Inspector with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned inspector with id: {id}");

                    InspectorDTO resultDTO = _mapper.Map<Inspector, InspectorDTO>(result);
                    return Ok(resultDTO);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside FindAsync action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                IEnumerable<Inspector> result = await _unitOfWork.Inspector.GetAllAsync();
                _logger.LogInformation($"Returned all inspectors from database.");

                IEnumerable<InspectorDTO> resultDTO= _mapper.Map<IEnumerable<Inspector>, IEnumerable<InspectorDTO>>(result);
                return Ok(resultDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllAsync action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{name}", Name = "InspectorByName")]
        public async Task<IActionResult> GetFilteredByNameAsync(string name)
        {
            try
            {
                IEnumerable<Inspector> result = await _unitOfWork.Inspector.GetFilteredAsync(o => o.Name == name);

                _logger.LogInformation($"Returned all inspectors from database filtered by name : {name}");

                IEnumerable<InspectorDTO> resultDTO = _mapper.Map<IEnumerable<Inspector>, IEnumerable<InspectorDTO>>(result);
                return Ok(resultDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetFilteredByNameAsync action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]InspectorDTO entity)
        {
            try
            {
                if (entity == null)
                {
                    _logger.LogError("Inspector object sent from client is null.");
                    return BadRequest("Inspector object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid inspector object sent from client.");
                    return BadRequest("Invalid model object");
                }

                Inspector inspector = _mapper.Map<InspectorDTO, Inspector>(entity);

                _unitOfWork.Inspector.Create(inspector);
                await _unitOfWork.CompleteAsync();

                InspectorDTO inspectorDTO = _mapper.Map<Inspector, InspectorDTO>(inspector);

                return CreatedAtRoute("InspectorById", new { id = inspectorDTO.Id }, inspectorDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Create action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _unitOfWork.Inspector.Delete(id);
            _unitOfWork.Complete();

            try
            {
                Inspector foundInspector = await _unitOfWork.Inspector.FindAsync(id);
                if (foundInspector == null)
                {
                    _logger.LogError($"Inspector with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                //if (_repository.Account.AccountsByOwner(id).Any())
                //{
                //    _logger.LogError($"Cannot delete owner with id: {id}. It has related accounts. Delete those accounts first");
                //    return BadRequest("Cannot delete owner. It has related accounts. Delete those accounts first");
                //}

                _unitOfWork.Inspector.Delete(foundInspector);
                await _unitOfWork.CompleteAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Delete action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody]InspectorDTO entity)
        {
            try
            {
                if (entity == null)
                {
                    _logger.LogError("Inspector object sent from client is null.");
                    return BadRequest("Inspector object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid inspector object sent from client.");
                    return BadRequest("Invalid model object");
                }

                Inspector inspector = _mapper.Map<InspectorDTO, Inspector>(entity);
                Inspector foundInspector = await _unitOfWork.Inspector.FindAsync(inspector.Id);

                if (foundInspector == null)
                {
                    _logger.LogError($"Inspector with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                foundInspector.Update(inspector);
                await _unitOfWork.CompleteAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Update action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
