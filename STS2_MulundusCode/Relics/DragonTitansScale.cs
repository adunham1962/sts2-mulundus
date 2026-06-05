using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.ValueProps;

namespace STS2_Mulundus.STS2_MulundusCode.Relics;

[Pool(typeof(EventRelicPool))]
public class DragonTitansScale() : STS2_MulundusRelic
{
    public override string PackedIconPath => "res://STS2_Mulundus/images/relics/dragon_titans_scale.png";
    public override RelicRarity Rarity => RelicRarity.Ancient;

    private int _amount = 0;

    public override int DisplayAmount => _amount;

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner) return;
        _amount++;

        await CreatureCmd.GainBlock(Owner.Creature, new BlockVar(_amount, ValueProp.Move), null);
        if (player.Creature.CombatState is null) return;
        await CreatureCmd.Damage(choiceContext, player.Creature.CombatState.HittableEnemies, new DamageVar(_amount, ValueProp.Move), Owner.Creature);
    }
}