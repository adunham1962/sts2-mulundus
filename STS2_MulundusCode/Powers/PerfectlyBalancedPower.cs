using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace STS2_Mulundus.STS2_MulundusCode.Powers;

public class PerfectlyBalancedPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterCardChangedPiles(CardModel card, PileType oldPileType, AbstractModel? clonedBy)
    {
        if (card.Owner == Owner.Player && card.Pile != null &&
            (oldPileType == PileType.Hand || card.Pile.Type == PileType.Hand))
        {
            var hand = PileType.Hand.GetPile(Owner.Player).Cards;
            if (hand.Count(c => c.Type == CardType.Attack) == hand.Count(c => c.Type == CardType.Skill))
                await PlayerCmd.GainEnergy(Amount, Owner.Player);
        }
    }
}