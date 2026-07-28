using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Cards;

namespace STS2_Mulundus.STS2_MulundusCode.Powers;

public class DrunkenMasteryPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Resources.EnergySpent < cardPlay.Card.EnergyCost.Canonical && cardPlay.Card.HasEbb() || cardPlay.Card.HasFlow())
        {
            if (Owner.Player is null) return Task.CompletedTask;
            var cards = CardPile.GetCards(Owner.Player, PileType.Hand);

            foreach (var card in cards)
            {
                card.EnergyCost.SetThisCombat(NextEnergyCost(card));
                NCard.FindOnTable(card)?.PlayRandomizeCostAnim();
            }
        } 
        return Task.CompletedTask;
    }
    
    private int NextEnergyCost(CardModel card)
    {
        return card.Owner.RunState.Rng.CombatEnergyCosts.NextInt(4);
    }
}