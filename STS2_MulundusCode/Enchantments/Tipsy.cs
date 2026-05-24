using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Cards;

namespace STS2_Mulundus.STS2_MulundusCode.Enchantments;

public class Tipsy : CustomEnchantmentModel
{
    
    protected override string CustomIconPath => "res://STS2_Mulundus/images/enchantments/tipsy.png";
    
    public override bool CanEnchant(CardModel card)
    {
        return base.CanEnchant(card) && !card.Keywords.Contains(CardKeyword.Unplayable) && !card.EnergyCost.CostsX;
    }

    public override Task AfterCardDrawn(
        PlayerChoiceContext choiceContext,
        CardModel card,
        bool fromHandDraw)
    {
        if (card != Card || Card.Pile?.Type != PileType.Hand)
            return Task.CompletedTask;
        Card.EnergyCost.SetThisCombat(NextEnergyCost());
        NCard.FindOnTable(card)?.PlayRandomizeCostAnim();
        return Task.CompletedTask;
    }

    private int NextEnergyCost()
    {
        return Card.Owner.RunState.Rng.CombatEnergyCosts.NextInt(5);
    }
}