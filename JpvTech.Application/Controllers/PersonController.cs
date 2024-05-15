using JpvTech.Application.DTOs.PersonDTOs;
using JPVTech.Commons.Interfaces;
using JPVTech.Domain.Entities;
using JPVTech.Domain.Interfaces.Services;
using JPVTech.Domain.Models;
using JPVTech.Service.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JpvTech.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IResponseCommon _responseCommon;
        private readonly IBaseService<PersonEntity> _baseService;
        private readonly IUriService _uriService;

        public PersonController(IResponseCommon responseCommon, IBaseService<PersonEntity> baseService, IUriService uriService)
        {
            _baseService = baseService;
            _responseCommon = responseCommon;
            _uriService = uriService;
        }

        [HttpGet]
        public async Task<ActionResult<Dictionary<string, object>>> GetAll([FromQuery] PaginationModel paginationModel)
        {
            try
            {
                (List<PersonResponseDTO>, int) tupleResponse = await _baseService.SelectPaginated<PersonResponseDTO>(paginationModel);
                string route = Request.Path.Value;
                var pagedPerson = _responseCommon.CreatePagedReponse(tupleResponse.Item1, paginationModel, tupleResponse.Item2, _uriService, route);


                Dictionary<string, object> response = _responseCommon.GenerateHttpResponse("Pesoas listadas com sucesso!", 200, pagedPerson);

                return Ok(response);
            }
            catch (Exception ex)
            {
                Dictionary<string, object> badRequestResponse = _responseCommon.GenerateHttpResponse(ex.Message, 400, null);
                return BadRequest(badRequestResponse);
            }
        }

        [HttpGet("{idPerson}")]
        public async Task<ActionResult<Dictionary<string, object>>> FindOne(int idPerson)
        {
            try
            {
                PersonResponseDTO person = await _baseService.GetById<PersonResponseDTO>(idPerson);
                Dictionary<string, object> response = _responseCommon.GenerateHttpResponse("Usuário listado com sucesso!", 200, person);
                return Ok(response);
            }
            catch (InvalidOperationException)
            {
                Dictionary<string, object> notFoundResponse = _responseCommon.GenerateHttpResponse($"Usuário com id {idPerson} não encontrado. Tente novamente!", 404, null);
                return NotFound(notFoundResponse);
            }
            catch (Exception ex)
            {
                Dictionary<string, object> badRequestResponse = _responseCommon.GenerateHttpResponse(ex.Message, 400, null);
                return BadRequest(badRequestResponse);
            }
        }

        [HttpDelete("{idPerson}")]
        public async Task<ActionResult> Delete(int idPerson)
        {
            try
            {
                await _baseService.Delete(idPerson);
                return NoContent();
            }
            catch (InvalidOperationException)
            {
                Dictionary<string, object> notFoundResponse = _responseCommon.GenerateHttpResponse($"Usuário com id {idPerson} não encontrado. Tente novamente!", 404, null);
                return NotFound(notFoundResponse);
            }
            catch (Exception ex)
            {
                Dictionary<string, object> badRequestResponse = _responseCommon.GenerateHttpResponse(ex.Message, 400, null);
                return BadRequest(badRequestResponse);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Dictionary<string, object>>> CreatePerson([FromBody] PersonRequestDTO person)
        {
            try
            {
                PersonResponseDTO personResponse = await _baseService.Add<PersonRequestDTO, PersonResponseDTO, PersonValidator>(person);
                Dictionary<string, object> response = _responseCommon.GenerateHttpResponse("Pessoa criada com sucesso!", 201, personResponse);

                return Created("", response);
            }
            catch (Exception ex)
            {


                Dictionary<string, object> violationKeyResponse = _responseCommon.GenerateHttpResponse($"CPF {person.CPF} já cadastrado. Tente novamente!", 422, null);
                return UnprocessableEntity(violationKeyResponse);
            }
        }

        [HttpPut("{idPerson}")]
        public async Task<ActionResult<Dictionary<string, object>>> UpdatePerson([FromBody] PersonRequestDTO person, int idPerson)
        {
            try
            {
                PersonResponseDTO personResponse = await _baseService.Update<PersonRequestDTO, PersonResponseDTO, PersonValidator>(person, idPerson);
                Dictionary<string, object> response = _responseCommon.GenerateHttpResponse("Pessoa criada com sucesso!", 200, personResponse);

                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                Dictionary<string, object> notFoundResponse = _responseCommon.GenerateHttpResponse($"Usuário com id {idPerson} não encontrado. Tente novamente!", 404, null);
                return NotFound(notFoundResponse);
            }
            catch (Exception ex)
            {
                Dictionary<string, object> badRequestResponse = _responseCommon.GenerateHttpResponse(ex.Message, 400, null);
                return BadRequest(badRequestResponse);
            }
        }
    }
}
