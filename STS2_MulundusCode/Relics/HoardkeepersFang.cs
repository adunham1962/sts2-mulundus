using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models.RelicPools;
using STS2_Mulundus.STS2_MulundusCode.Relics;

namespace STS2_Mulundus.STS2_MulundusCode.Relics;

[Pool(typeof(EventRelicPool))]
public class HoardkeepersFang() : STS2_MulundusRelic
{
    public override RelicRarity Rarity => RelicRarity.Ancient;

    public override async Task AfterGoldGained(Player player)
    {
        if (player != Owner) return;
        await CreatureCmd.Heal(player.Creature, 3);
    }
}