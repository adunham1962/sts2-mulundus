using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using STS2_Mulundus.STS2_MulundusCode.Enchantments;

namespace STS2_Mulundus.STS2_MulundusCode.Relics;

[Pool(typeof(EventRelicPool))]
public class DreadnoughtHide : STS2_MulundusRelic
{
    public override RelicRarity Rarity => RelicRarity.Ancient;
    
    public override string PackedIconPath => "res://STS2_Mulundus/images/relics/dreadnought_hide.png";

    protected override IEnumerable<IHoverTip> ExtraHoverTips => HoverTipFactory.FromEnchantment<AstralReinforcement>();

    public override Task AfterObtained()
    {
        var astral = ModelDb.Enchantment<AstralReinforcement>();
        var list = PileType.Deck.GetPile(Owner).Cards.Where(astral.CanEnchant).ToList();
        if (list.Count > 0)
        {
            list.ForEach(c =>
            {
                CardCmd.Enchant<AstralReinforcement>(c, 1M);
                if (!c.Keywords.Contains(CardKeyword.Ethereal))
                    c.AddKeyword(CardKeyword.Ethereal);
                var child = NCardEnchantVfx.Create(c);
                if (child is null) return;
            
                var instance = NRun.Instance;
                instance?.GlobalUi.CardPreviewContainer.AddChildSafely(child);
            });
        }
        return Task.CompletedTask;
    }
}