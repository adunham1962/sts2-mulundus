using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk;

namespace STS2_Mulundus.STS2_MulundusCode.Enchantments;

public class Waterbending : CustomEnchantmentModel
{
    
    protected override string CustomIconPath => "res://STS2_Mulundus/images/enchantments/waterbending.png";

    private bool _isReducedByThis = false;

    public override bool CanEnchant(CardModel card)
    {
        if (card is EmeraldMonkCard && (card.HasEbb() || card.HasFlow())) return false;
        
        return base.CanEnchant(card) && !card.Keywords.Contains(CardKeyword.Unplayable) && !card.EnergyCost.CostsX;
    }
    
    public override Task AfterCardPlayedLate(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card.Type != Card.Type && cardPlay.Card.Owner == Card.Owner && !_isReducedByThis)
        {
            Card.EnergyCost.SetThisTurnOrUntilPlayed(Card.EnergyCost.GetResolved() - 1);
            _isReducedByThis = true;
        } else if (cardPlay.Card.Type == Card.Type && cardPlay.Card.Owner == Card.Owner && _isReducedByThis)
        {
            Card.EnergyCost.SetThisTurnOrUntilPlayed(Card.EnergyCost.GetResolved() + 1);
            _isReducedByThis = false;
        }
        
        return Task.CompletedTask;
    }
}