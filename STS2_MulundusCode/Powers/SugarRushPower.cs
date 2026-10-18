using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Cards.Token;

namespace STS2_Mulundus.STS2_MulundusCode.Powers;

public class SugarRushPower() : CustomPowerModel()
{
    public override string CustomPackedIconPath => "res://STS2_Mulundus/images/powers/sugar_rush_power.png";

    public override string CustomBigIconPath => "res://STS2_Mulundus/images/powers/sugar_rush_power.png";

    public override Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner.Player) return Task.CompletedTask;
        
        try
        {
            var cardsInHand = CardPile.GetCards(player, PileType.Hand);
            foreach (var card in cardsInHand)
            {
                if (!card.Keywords.Contains(CardKeyword.Ethereal))
                {
                    card.AddKeyword(CardKeyword.Ethereal);
                }
            }

            return Task.CompletedTask;
        }
        catch (Exception exception)
        {
            return Task.FromException(exception);
        }
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card is Goodberry)
        {
            await CardPileCmd.Draw(choiceContext, 1, cardPlay.Card.Owner);
        }
    }

    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
}