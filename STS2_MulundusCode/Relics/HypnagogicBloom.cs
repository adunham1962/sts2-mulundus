using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.RelicPools;
using STS2_Mulundus.STS2_MulundusCode.Relics;

namespace STS2_Mulundus.STS2_MulundusCode.Relics;


[Pool(typeof(EventRelicPool))]
public class HypnagogicBloom() : STS2_MulundusRelic
{
    public override RelicRarity Rarity => RelicRarity.Ancient;
    public override string PackedIconPath => "res://STS2_Mulundus/images/relics/hypnagogic_bloom.png";
    
    public override async Task AfterShuffle(PlayerChoiceContext choiceContext, Player shuffler)
    {
        if (shuffler != Owner || shuffler.Creature.CombatState is null) return;
        await PowerCmd.Apply<WeakPower>(choiceContext, shuffler.Creature.CombatState.HittableEnemies, 1, shuffler.Creature, null);
    }
}