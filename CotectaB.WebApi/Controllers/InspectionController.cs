using AutoMapper;
using CotecnaB.Abstractions.Interfaces.UnitsOfWork;
using CotecnaB.Core.DTOs;
using CotecnaB.Core.Entities;
using CotecnaB.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CotectaB.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspectionController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        public InspectionController(IMapper mapper, IUnitOfWork unitOfWork, ILoggerFactory logFactory)
        {
            _logger = logFactory.CreateLogger<InspectionController>();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id:Guid}", Name = "InspectionById")]
        public async Task<IActionResult> FindAsync(Guid id)
        {
            try
            {
                Inspection result = await _unitOfWork.Inspection.FindEagerAsync(id);
                if (result == null)
                {
                    _logger.LogError($"Inspection with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned inspection with id: {id}");

                    InspectionDTO resultDTO = _mapper.Map<Inspection, InspectionDTO>(result);
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
                IEnumerable<Inspection> result = await _unitOfWork.Inspection.GetAllEagerAsync();
                _logger.LogInformation($"Returned all inspections from database.");

                IEnumerable<InspectionDTO> resultDTO = _mapper.Map<IEnumerable<Inspection>, IEnumerable<InspectionDTO>>(result);
                return Ok(resultDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllAsync action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{status}", Name = "InspectionByStatus")]
        public async Task<IActionResult> GetFilteredByStatusAsync(Status status)
        {
            try
            {
                IEnumerable<Inspection> result = await _unitOfWork.Inspection.GetFilteredEagerAsync(o => o.Status == status);

                _logger.LogInformation($"Returned all inspections from database filtered by status : {status}");

                IEnumerable<InspectionDTO> resultDTO = _mapper.Map<IEnumerable<Inspection>, IEnumerable<InspectionDTO>>(result);
                return Ok(resultDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetFilteredByNameAsync action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]InspectionDTO entity)
        {
            try
            {
                if (entity == null)
                {
                    _logger.LogError("Inspection object sent from client is null.");
                    return BadRequest("Inspection object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid inspection object sent from client.");
                    return BadRequest("Invalid model object");
                }

                Inspection found = await _unitOfWork.Inspection.GetSingleEagerAsync(o => o.Id == entity.Id
                                        && o.InspectionInspector.Any(ii => ii.InspectionDate == entity.InspectionDate)
                                        && o.InspectionInspector.Any(ii => entity.Inspectors.Any(i => i.Id == ii.InspectorId)));
                if (found != null)
                {
                    _logger.LogError("Invalid inspection object sent from client.");
                    return BadRequest("No more than 1 Inspection per day/Inspector");
                }

                Inspection inspection = _mapper.Map<InspectionDTO, Inspection>(entity);

                _unitOfWork.Inspection.Create(inspection);
                await _unitOfWork.CompleteAsync();

                InspectionDTO inspectionDTO = _mapper.Map<Inspection, InspectionDTO>(inspection);

                return CreatedAtRoute("InspectionById", new { id = inspectionDTO.Id }, inspectionDTO);
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
            _unitOfWork.Inspection.Delete(id);
            _unitOfWork.Complete();

            try
            {
                Inspection foundInspection = await _unitOfWork.Inspection.FindAsync(id);
                if (foundInspection == null)
                {
                    _logger.LogError($"Inspection with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                //if (_repository.Account.AccountsByOwner(id).Any())
                //{
                //    _logger.LogError($"Cannot delete owner with id: {id}. It has related accounts. Delete those accounts first");
                //    return BadRequest("Cannot delete owner. It has related accounts. Delete those accounts first");
                //}

                _unitOfWork.Inspection.Delete(foundInspection);
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
        public async Task<IActionResult> Update(Guid id, [FromBody]InspectionDTO entity)
        {
            try
            {
                if (entity == null)
                {
                    _logger.LogError("Inspection object sent from client is null.");
                    return BadRequest("Inspection object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid inspection object sent from client.");
                    return BadRequest("Invalid model object");
                }

                if (DateTime.Compare(entity.InspectionDate, DateTime.Today) < 0)
                {
                    _logger.LogError("Invalid inspection object sent from client.");
                    return BadRequest("Can't update an older inspection.");
                }

                Inspection inspection = _mapper.Map<InspectionDTO, Inspection>(entity);
                Inspection foundInspection = await _unitOfWork.Inspection.FindEagerAsync(inspection.Id);

                if (foundInspection == null)
                {
                    _logger.LogError($"Inspection with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                foundInspection.Update(inspection);
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
