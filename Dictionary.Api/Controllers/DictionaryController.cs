using AutoMapper;
using Dictionary.Api.Models;
using Dictionary.Domain.Entity;
using Dictionary.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace SaintEfim.MinimalApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        private readonly IGermanRussianDictionaryRepository _repository;
        private readonly IMapper _mapper;

        public DictionaryController(IGermanRussianDictionaryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetDictionariesAsync()
        {
            var dictionaries = await _repository.GetDictionariesAsync();
            return Ok(dictionaries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDictionaryAsync(int id)
        {
            var dictionary = await _repository.GetDictionaryAsync(id);
            if (dictionary == null)
            {
                return NotFound();
            }
            return Ok(dictionary);
        }

        [HttpPost]
        public async Task<IActionResult> InsertDictionaryAsync([FromBody] CreateGermanRussianDictionaryDto dictionary)
        {
            var germanRussianDictionary = _mapper.Map<GermanRussianDictionary>(dictionary);
            await _repository.InsertDictionaryAsync(germanRussianDictionary);
            await _repository.SaveAsync();
            var result = _mapper.Map<ResultGermanRussianDictionaryDto>(germanRussianDictionary);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDictionaryAsync(int id, [FromBody] GermanRussianDictionary dictionary)
        {
            if (id != dictionary.Id)
            {
                return BadRequest();
            }
            await _repository.UpdateDictionaryAsync(dictionary);
            await _repository.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDictionaryAsync(int id)
        {
            await _repository.DeleteDictionaryAsync(id);
            await _repository.SaveAsync();
            return Ok();
        }
    }
}
