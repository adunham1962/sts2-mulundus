using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using STS2_Mulundus.STS2_MulundusCode.Enchantments;
using STS2_Mulundus.STS2_MulundusCode.Relics;

namespace STS2_Mulundus.STS2_MulundusCode.Relics;

[Pool(typeof(EventRelicPool))]
public class RaspberryWine() : STS2_MulundusRelic
{
    public override string PackedIconPath => "res://STS2_Mulundus/images/relics/raspberry_wine.png";
    public override RelicRarity Rarity => RelicRarity.Ancient;

    public override async Task AfterObtained()
    {
        var prefs = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 2);
        var randoms = new List<CardPileAddResult>();
        foreach (var original in (await CardSelectCmd.FromDeckForTransformation(Owner, prefs)).ToList())
        {
            var random = await CardCmd.TransformToRandom(original, Owner.RunState.Rng.Niche);
            randoms.Add(random);
        }

        foreach (var card in randoms.Select(cardPileAddResult => cardPileAddResult.cardAdded))
        {
            CardCmd.Enchant<Tipsy>(card, 1M);
            var child = NCardEnchantVfx.Create(card);
            if (child == null) continue;
            var instance = NRun.Instance;
            instance?.GlobalUi.CardPreviewContainer.AddChildSafely((Godot.Node)child);
        }
    }
}