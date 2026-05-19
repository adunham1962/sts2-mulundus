using BaseLib.Abstracts;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Powers;

public class LifeOnTheEdgePower() : CustomPowerModel()
{

    public override decimal ModifyHandDraw(Player player, decimal count)
    {
        if (player.Creature != Owner || !player.HasPower<PoisonPower>()) return count;
        var increase = 2 * Amount;
        return count + increase;
    }

    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
}