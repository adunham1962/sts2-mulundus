using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using STS2_Mulundus.STS2_MulundusCode.Relics;

namespace STS2_Mulundus.STS2_MulundusCode.Relics;

[Pool(typeof(EventRelicPool))]
public class TheFirstFlame() : STS2_MulundusRelic
{
    public override RelicRarity Rarity => RelicRarity.Ancient;

    public override async Task AfterObtained()
    {
        var prefs = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 2);
        foreach (var card in await CardSelectCmd.FromDeckForEnchantment(Owner, ModelDb.Enchantment<Vigorous>(), 1, prefs))
        {
            CardCmd.Enchant<Vigorous>(card, 8);
            var child = NCardEnchantVfx.Create(card);
            if (child is null) return;
            
            var instance = NRun.Instance;
            if (instance is null) return; 
            
            instance.GlobalUi.CardPreviewContainer.AddChildSafely(child);
        }
    }

    public override async Task AfterRoomEntered(AbstractRoom room)
    {
        await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), Owner.Creature, new DamageVar(1, ValueProp.Move), null, null);
    }
}