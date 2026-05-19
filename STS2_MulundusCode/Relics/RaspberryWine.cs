using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Nodes.Cards;
using STS2_Mulundus.STS2_MulundusCode.Relics;

namespace STS2_Mulundus.STS2_MulundusCode.Relics;

[Pool(typeof(EventRelicPool))]
public class RaspberryWine() : STS2_MulundusRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;

    public override Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner) return Task.CompletedTask;
        
        var hand = PileType.Hand.GetPile(player);
        var random = Random.Shared.Next(hand.Cards.Count);
        var card = hand.Cards.ToList()[random];
        
        if (card.EnergyCost.GetWithModifiers(CostModifiers.None) < 0) return Task.CompletedTask;
        
        card.EnergyCost.SetThisTurnOrUntilPlayed(NextEnergyCost());
        NCard.FindOnTable(card)?.PlayRandomizeCostAnim();
        return Task.CompletedTask;
    }
    
    private int NextEnergyCost()
    {
        return Owner.RunState.Rng.CombatEnergyCosts.NextInt(4);
    }
}