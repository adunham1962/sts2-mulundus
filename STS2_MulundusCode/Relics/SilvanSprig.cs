using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Rooms;
using STS2_Mulundus.STS2_MulundusCode.Relics;

namespace STS2_Mulundus.STS2_MulundusCode.Relics;

[Pool(typeof(EventRelicPool))]
public class SilvanSprig() : STS2_MulundusRelic
{
    public override string PackedIconPath => "res://STS2_Mulundus/images/relics/silvan_sprig.png";
    public override async Task AfterCombatEnd(CombatRoom room)
    {
        Flash();
        var amount = CardPile.GetCards(Owner, PileType.Deck).Count() / 10;
        await CreatureCmd.Heal(Owner.Creature, 2 * amount);
    }

    public override RelicRarity Rarity => RelicRarity.Ancient;

    
}