using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace STS2_Mulundus.STS2_MulundusCode.Enchantments;

public class Sticky : CustomEnchantmentModel
{
    protected override string CustomIconPath => "res://STS2_Mulundus/images/enchantments/sticky.png";

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card != Card) return;
        await CardPileCmd.Add(Card, PileType.Hand.GetPile(Card.Owner));
    }
}