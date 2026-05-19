using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using STS2_Mulundus.STS2_MulundusCode.Cards.Curse;

namespace STS2_Mulundus.STS2_MulundusCode.Relics;
[Pool(typeof(EventRelicPool))]
public class BindosSpecialMushroom(): STS2_MulundusRelic()
{
    public override RelicRarity Rarity =>
        RelicRarity.Ancient;

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromCard<BadMushroomTrip>()
    ];

    public override decimal ModifyHandDraw(Player player, Decimal count)
    {
        if (player == Owner)
        {
            return count + 1;
        }

        return count;
    }
    
    public override async Task AfterObtained()
    {
        var cardPileAddResultList = new List<CardPileAddResult>(1);
        var card = ModelDb.Card<BadMushroomTrip>();
        var cardPileAddResult = await CardPileCmd.Add(Owner.RunState.CreateCard(card, Owner), PileType.Deck);
        cardPileAddResultList.Add(cardPileAddResult);
        CardCmd.PreviewCardPileAdd(cardPileAddResultList, 2f);
    }
}