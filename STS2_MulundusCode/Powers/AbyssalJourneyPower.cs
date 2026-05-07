using BaseLib.Abstracts;
using STS2_Mulundus.STS2_MulundusCode.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Powers;

public class AbyssalJourneyPower() : CustomPowerModel()
{
    
    //Loads from STS2_Mulundus/images/powers/your_power.png
    public override string CustomPackedIconPath
    {
        get
        {
            var path = "power.png".PowerImagePath();
            return ResourceLoader.Exists(path) ? path : "power.png".PowerImagePath();
        }
    }

    public override string CustomBigIconPath
    {
        get
        {
            var path = "power.png".BigPowerImagePath();
            return ResourceLoader.Exists(path) ? path : "power.png".BigPowerImagePath();
        }
    }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (!this.Owner.HasPower<PoisonPower>()) 
          await CreatureCmd.LoseMaxHp(choiceContext, this.Owner, 1, false);
    }

    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
}