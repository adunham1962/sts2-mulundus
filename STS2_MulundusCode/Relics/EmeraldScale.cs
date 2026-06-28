using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using STS2_Mulundus.STS2_MulundusCode.Enchantments;

namespace STS2_Mulundus.STS2_MulundusCode.Relics;

[Pool(typeof(EventRelicPool))]
public class EmeraldScale : STS2_MulundusRelic
{
    public override RelicRarity Rarity => RelicRarity.Starter;
    
    public override string PackedIconPath => "res://STS2_Mulundus/images/relics/emerald_scale.png";

    public decimal Energy = 1;

    public override async Task AfterObtained()
    {
        var waterbending = ModelDb.Enchantment<Waterbending>();
        var list = PileType.Deck.GetPile(Owner).Cards.Where(waterbending.CanEnchant).ToList();
        var prefs = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 2);
        var cards = await CardSelectCmd.FromDeckForEnchantment(list, waterbending, 1, prefs);
        var cardModels = cards.ToList();
        if (cardModels.Count == 0) return;
        cardModels.ToList().ForEach(c =>
        {
            CardCmd.Enchant<Waterbending>(c, 1M);
            var child = NCardEnchantVfx.Create(c);
            if (child is null) return;
            
            var instance = NRun.Instance;
            instance?.GlobalUi.CardPreviewContainer.AddChildSafely(child);
        });
    }
}