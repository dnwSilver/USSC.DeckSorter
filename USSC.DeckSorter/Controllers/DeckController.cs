using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiskFirst.Hateoas;
using USSC.DeckSorter.BusinessLogic;
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
        /// Сервис для работы с колодами.
        /// </summary>
        private readonly IDeckService _deckService;

        /// <summary>
        /// Маппер для колод.
        /// </summary>
        private readonly DeckMapper _deckMapper;
        
        /// <summary>
        /// Сервис для построения ссылок HATEOAS.
        /// </summary>
        private readonly ILinksService _linksService;

        /// <summary>
        /// Конструктор для объекта <see cref="DeckController"/>.
        /// </summary>
        /// <param name="linksService">Сервис для построения ссылок HATEOAS.</param>
        /// <param name="deckService">Сервис для работы с колодами.</param>
        /// <param name="deckMapper">Маппер для колод.</param>
        public DeckController(ILinksService linksService, IDeckService deckService, DeckMapper deckMapper)
        {
            this._deckMapper = deckMapper;
            this._deckService = deckService;
            this._linksService = linksService;
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
            var deck = await _deckService.Find(id);
            if (deck == null)
            {
                return NotFound();
            }

            var deckResponse = _deckMapper.Map(deck);
            await _linksService.AddLinksAsync(deckResponse);
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
            var decks = await _deckService.Find();
            var deckResponses = decks.Select(_deckMapper.Map);
            
            foreach (var deckResponse in deckResponses)
            {
                await _linksService.AddLinksAsync(deckResponse);
            }
            
            return Ok(deckResponses);
        }

        /// <summary>
        /// Создание новой колоды.
        /// </summary>
        /// <param name="deckRequest">Необходимоя информация для создания колоды.</param>
        /// <returns>Информация о созданной колоде.</returns>
        [HttpPost(Name = nameof(CreateDeck))]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateDeck([Required] [FromBody] DeckCreateRequest deckRequest)
        {  
            var deck = await _deckService.Find(deckRequest.Name);
            if (deck != null)
            {
                return Conflict(deckRequest.Name);
            }
            
            await _deckService.CreateNewDeck(deckRequest.Name);
            deck = await _deckService.Find(deckRequest.Name);
            var deckResponse = _deckMapper.Map(deck);

            await _linksService.AddLinksAsync(deckResponse);
            return CreatedAtAction(nameof(GetDeck), new {id = deckResponse.Id}, deckResponse);
        }

        /// <summary>
        /// Перемешивание колоды.
        /// </summary>
        /// <param name="id">Уникальный идентификатор колоды.</param>
        [HttpPatch("{id}", Name = nameof(ShuffleDeck))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ShuffleDeck([Required] [FromRoute] Guid id)
        {
            if (await _deckService.IsDeckNotExists(id))
            {
                return NotFound();
            }

            await _deckService.SnuffleDeck(id);
            
            return Ok();
        }

        /// <summary>
        /// Удаление колоды.
        /// </summary>
        /// <param name="id">Уникальный идентификатор колоды.</param>
        [HttpDelete("{id}", Name = nameof(DeleteDeck))]
        public async Task<IActionResult> DeleteDeck([Required] [FromRoute] Guid id)
        {
            if (await _deckService.IsDeckNotExists(id))
            {
                return NotFound();
            }
            await _deckService.Remove(id);
            return Ok();
        }
    }
}