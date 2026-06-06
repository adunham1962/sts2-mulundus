using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using STS2_Mulundus.STS2_MulundusCode.Enchantments;

namespace STS2_Mulundus.STS2_MulundusCode.Relics;

[Pool(typeof(EventRelicPool))]
public class RaspberryJam() : STS2_MulundusRelic
{
    public override RelicRarity Rarity => RelicRarity.Ancient;
    public override string PackedIconPath => "res://STS2_Mulundus/images/relics/raspberry_jam.png";

    public override bool HasUponPickupEffect => true;

    public override Task AfterObtained()
    {
        var defends = PileType.Deck.GetPile(Owner).Cards.Where(c => c.Tags.Contains(CardTag.Defend));
        foreach (var card in defends)
        {
            CardCmd.Enchant<Sticky>(card, 1M);
            var child = NCardEnchantVfx.Create(card);
            if (child == null) continue;
            var instance = NRun.Instance;
            instance?.GlobalUi.CardPreviewContainer.AddChildSafely(child);
        }
        return Task.CompletedTask;
    }
}