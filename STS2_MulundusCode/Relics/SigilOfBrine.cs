using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.ValueProps;

namespace STS2_Mulundus.STS2_MulundusCode.Relics;

[Pool(typeof(EventRelicPool))]
public class SigilOfBrine() : STS2_MulundusRelic
{
    
    public override string PackedIconPath => "res://STS2_Mulundus/images/relics/sigil_of_brine.png";
    public override RelicRarity Rarity => RelicRarity.Ancient;

    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier,
        CardModel? cardSource)
    {
        if (power.Owner == Owner.Creature && power.Type == PowerType.Debuff && power.Amount != 0 || applier == Owner.Creature && power.Type == PowerType.Debuff && power.Amount != 0)
        {
            await CreatureCmd.Damage(choiceContext, power.CombatState.HittableEnemies, 1, ValueProp.Unblockable, Owner.Creature, null);
        }
    }
    
}