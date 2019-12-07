using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiskFirst.Hateoas;
using USSC.DeckSorter.Requests;
using USSC.DeckSorter.Responses;

namespace USSC.DeckSorter.Controllers
{
    /// <summary>
    /// Контроллер для работы с колодой.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DeckController : ControllerBase
    {
        /// <summary>
        /// Список доступных колод. todo: заменить на нормальный источник данных.
        /// </summary>
        private static List<DeckResponse> decks = new List<DeckResponse>();

        /// <summary>
        /// Сервис для построения ссылок HATEOAS.
        /// </summary>
        private readonly ILinksService linksService;

        /// <summary>
        /// Конструктор для объекта <see cref="DeckController"/>.
        /// </summary>
        /// <param name="linksService">Сервис для построения ссылок HATEOAS.</param>
        public DeckController(ILinksService linksService)
        {
            this.linksService = linksService;
        }

        /// <summary>
        /// Просмотр информации по колоде.
        /// </summary>
        /// <param name="id">Уникальный идентификатор колоды.</param>
        /// <returns>Информация по колоде.</returns>
        [HttpGet("{id}", Name = nameof(GetDeck))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetDeck([Required] [FromRoute] Guid id)
        {
            var deckResponse = decks.FirstOrDefault(x => x.Id == id);

            if (deckResponse == null)
            {
                return NotFound(id);
            }
            
            deckResponse.Links.Clear();
            await linksService.AddLinksAsync(deckResponse);
            return Ok(deckResponse);
        }

        /// <summary>
        /// Функциональность для получения списока всех доступных колод.
        /// </summary>
        /// <returns>Список всех доступных колод.</returns>
        [HttpGet(Name = nameof(GetAllDecks))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllDecks()
        {
            return Ok(decks.Select(x => new {x.Id, x.Name}));
        }

        /// <summary>
        /// Создание новой колоды.
        /// </summary>
        /// <param name="deckRequest">Необходимоя информация для создания колоды.</param>
        /// <returns>Информация о созданной колоде.</returns>
        [HttpPost(Name = nameof(CreateDeck))]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateDeck([Required] [FromBody] DeckCreateRequest deckRequest)
        {
            var deckResponse = new DeckResponse
            {
                Id = Guid.NewGuid(),
                Name = deckRequest.Name
            };

            decks.Add(deckResponse);
            await linksService.AddLinksAsync(deckResponse);
            return CreatedAtAction(nameof(GetDeck), new {id = deckResponse.Id}, deckResponse);
        }

        /// <summary>
        /// Перемешивание колоды.
        /// </summary>
        /// <param name="id">Уникальный идентификатор колоды.</param>
        [HttpPatch("{id}", Name = nameof(SorterDeck))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SorterDeck([Required] [FromRoute] Guid id)
        {
            if (!decks.Any(x => x.Id == id))
            {
                return NotFound();
            }

            return Ok();
        }

        /// <summary>
        /// Удаление колоды.
        /// </summary>
        /// <param name="id">Уникальный идентификатор колоды.</param>
        [HttpDelete("{id}", Name = nameof(DeleteDeck))]
        public async Task<IActionResult> DeleteDeck([Required] [FromRoute] Guid id)
        {
            if (!decks.Any(x => x.Id == id))
            {
                return NotFound();
            }
            
            decks.Remove(decks.First(x => x.Id == id));
            
            return Ok();
        }
    }
}