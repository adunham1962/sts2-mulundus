using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Rooms;

namespace STS2_Mulundus.STS2_MulundusCode.Relics;

[Pool(typeof(EventRelicPool))]
public class PoisonousFlower() : STS2_MulundusRelic()
{
    public override string PackedIconPath => "res://STS2_Mulundus/images/relics/poisonous_flower.png";
    public override RelicRarity Rarity => RelicRarity.Ancient;

    public override async Task AfterObtained()
    {
        var sown = ModelDb.Enchantment<Sown>();
        var list = PileType.Deck.GetPile(Owner).Cards.Where(sown.CanEnchant).ToList();
        var prefs = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 2);
        var cards = await CardSelectCmd.FromDeckForEnchantment(list, sown, 1, prefs);
        var cardModels = cards.ToList();
        if (cardModels.Count == 0) return;
        cardModels.ToList().ForEach(c =>
        {
            CardCmd.Enchant<Sown>(c, 1M);
            var child = NCardEnchantVfx.Create(c);
            if (child is null) return;
            
            var instance = NRun.Instance;
            instance?.GlobalUi.CardPreviewContainer.AddChildSafely(child);
        });
    }

    public override async Task AfterRoomEntered(AbstractRoom room)
    {
        if (room is CombatRoom)
        {
            await PowerCmd.Apply<PoisonPower>(new ThrowingPlayerChoiceContext(),Owner.Creature, 2, null, null);
        }
    }
}