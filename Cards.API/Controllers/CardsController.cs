using Cards.API.Data;
using Cards.API.Models;
//using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Cards.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : Controller
    {
        private readonly CardsDbContext cardsDbContext;

        public CardsController(CardsDbContext cardsDbContext)
        {
            this.cardsDbContext = cardsDbContext;
        }
        //Get All Cards
        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
            var cards = await cardsDbContext.Cards.ToListAsync();
            return Ok(cards);

        }

        //Get single card
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetCard")]
        public async Task<IActionResult> GetCard([FromRoute] Guid id)
        {
            var cards = await cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (cards == null)
            {
                return Ok(cards);
            }
            return NotFound("Card not found");

        }
        // Add card
        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] Card card)
        {
            card.Id = Guid.NewGuid();
            await cardsDbContext.Cards.AddAsync(card);
            await cardsDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(AddCard), card.Id, card);
        }

        //Update A Card
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCard([FromRoute] Guid id, [FromBody] Card card)
        {
            var existingCard = await cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCard != null)
            {
                existingCard.CardholderName = card.CardholderName;
                existingCard.CardNumber = card.CardNumber;
                existingCard.ExpiryMonth = card.ExpiryMonth;
                existingCard.ExpiryYear = card.ExpiryYear;
                existingCard.CVC = card.CVC;
                await cardsDbContext.SaveChangesAsync();
                return Ok(existingCard);
            }
            return NotFound("Card not found");
        }

        //Delete A Card
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCard([FromRoute] Guid id)
        {
            var existingCard = await cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCard != null)
            {
                cardsDbContext.Remove(existingCard);
                await cardsDbContext.SaveChangesAsync();
                return Ok(existingCard);
            }
            return NotFound("Card not found");
        }




    }

}
