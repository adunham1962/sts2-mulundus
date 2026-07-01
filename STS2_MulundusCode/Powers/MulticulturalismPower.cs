using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace STS2_Mulundus.STS2_MulundusCode.Powers;

public class MulticulturalismPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override Task AfterCardGeneratedForCombat(CardModel card, Player? creator)
    {
        if (card.Owner == Owner.Player)
        {
            card.EnergyCost.SetCustomBaseCost(card.EnergyCost.Canonical - 1);
        }
        return Task.CompletedTask;
    }
}