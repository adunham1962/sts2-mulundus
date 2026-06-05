using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.RelicPools;
using STS2_Mulundus.STS2_MulundusCode.Relics;

namespace STS2_Mulundus.STS2_MulundusCode.Relics;

[Pool(typeof(EventRelicPool))]
public class SilvanTotem() : STS2_MulundusRelic
{
    public override string PackedIconPath => "res://STS2_Mulundus/images/relics/silvan_totem.png";
    public override RelicRarity Rarity => RelicRarity.Ancient;

    public override async Task AfterObtained()
    {
        await CreatureCmd.LoseMaxHp(new ThrowingPlayerChoiceContext(), Owner.Creature, 15, false);
        var deck = PileType.Deck.GetPile(Owner).Cards.ToList();
        var strikes = deck.FindAll(c => c.Tags.ToList().Contains(CardTag.Strike));
        
        var transformations = new List<CardTransformation>();
        strikes.ForEach(c => transformations.Add(new CardTransformation(c)));
        await CardCmd.Transform(transformations, Owner.PlayerRng.Transformations);
    }
}