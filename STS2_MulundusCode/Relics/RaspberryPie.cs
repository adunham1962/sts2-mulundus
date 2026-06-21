using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace STS2_Mulundus.STS2_MulundusCode.Relics;

[Pool(typeof(EventRelicPool))]
public class RaspberryPie() : STS2_MulundusRelic
{
    public override RelicRarity Rarity => RelicRarity.Ancient;
    public override string PackedIconPath => "res://STS2_Mulundus/images/relics/raspberry_pie.png";

    private int _activeAct = -1;
    public override bool HasUponPickupEffect => true;
    
    [SavedProperty]
    private int ActiveAct
    {
        get => _activeAct;
        set
        {
            AssertMutable();
            _activeAct = value;
        }
    }
    
    public override Task AfterObtained()
    {
        ActiveAct = Owner.RunState.CurrentActIndex;
        return Task.CompletedTask;
    }
    
    public override decimal ModifyMaxEnergy(Player player, Decimal amount)
    {
        return player != Owner || ActiveAct != Owner.RunState.CurrentActIndex ? amount : amount + 1;
    }
    
    public override async Task AfterRoomEntered(AbstractRoom _)
    {
        Status = ActiveAct == Owner.RunState.CurrentActIndex ? RelicStatus.Normal : RelicStatus.Disabled;
        if (Status == RelicStatus.Normal)
        {
            await PowerCmd.Apply<DexterityPower>(new ThrowingPlayerChoiceContext(), Owner.Creature, -1, null, null);
        }
    }
}