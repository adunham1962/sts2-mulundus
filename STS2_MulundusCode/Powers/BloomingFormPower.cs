using BaseLib.Abstracts;
using STS2_Mulundus.STS2_MulundusCode.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace STS2_Mulundus.STS2_MulundusCode.Powers;

public class BloomingFormPower() : CustomPowerModel()
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

    public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool causedByEthereal)
    {
        if (CombatState.CurrentSide != Owner.Side)
            return;
        Flash();
        await Cmd.CustomScaledWait(0.2f, 0.4f);
        foreach (var hittableEnemy in CombatState.HittableEnemies)
        {
            var creatureNode = NCombatRoom.Instance?.GetCreatureNode(hittableEnemy);
            if (creatureNode != null)
                NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NGaseousImpactVfx.Create(creatureNode.VfxSpawnPosition, new Color("83eb85")));
        }
        await PowerCmd.Apply<PoisonPower>(CombatState.HittableEnemies, Amount, Owner, null);
    }

    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
}