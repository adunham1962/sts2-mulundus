using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.Models.RelicPools;

namespace STS2_Mulundus.STS2_MulundusCode.Relics;


[Pool(typeof(EventRelicPool))]
public class ReforgedArmory() : STS2_MulundusRelic
{
    public override RelicRarity Rarity => RelicRarity.Ancient;
    public override string PackedIconPath => "res://STS2_Mulundus/images/relics/reforged_armory.png";

    public override async Task AfterObtained()
    {
        var attacks = PileType.Deck.GetPile(Owner).Cards.Where(c => c is { Type: CardType.Attack, IsUpgraded: false });
        var randoms = attacks.TakeRandom(5, Owner.RunState.Rng.Shuffle);
        foreach (var cardModel in randoms)
        {
            CardCmd.Upgrade(cardModel);
        }
    }
}