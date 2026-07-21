using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Powers;

public class LifeOnTheEdgePower() : CustomPowerModel()
{

    public override decimal ModifyHandDraw(Player player, decimal count)
    {
        if (player.Creature != Owner || !player.HasPower<PoisonPower>()) return count;
        var increase = Amount;
        return count + increase;
    }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player == Owner.Player)
        {
            await PowerCmd.Apply<PoisonPower>(choiceContext, Owner, Amount, null, null);
        }
    }

    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
}