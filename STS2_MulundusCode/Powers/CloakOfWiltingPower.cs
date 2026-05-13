using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Powers;

public class CloakOfWiltingPower() : CustomPowerModel()
{
    public override decimal ModifyMaxEnergy(Player player, decimal amount)
    {
        return player != Owner.Player ? amount : amount + Amount;
    }

    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
}