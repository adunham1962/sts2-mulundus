using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using STS2_Mulundus.STS2_MulundusCode.Cards.Ancient;

namespace STS2_Mulundus.STS2_MulundusCode.Relics;
[Pool(typeof(EventRelicPool))]
public class PairOfBears() : STS2_MulundusRelic()
{
    
    public override RelicRarity Rarity => RelicRarity.Ancient;

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromCard<Jake>(),
        HoverTipFactory.FromCard<Aljmor>()
    ];
    
    public override async Task AfterObtained()
    {
        var cardPileAddResultList = new List<CardPileAddResult>(1);
        var jake = ModelDb.Card<Jake>();
        var aljmor = ModelDb.Card<Aljmor>();

        var cardPileAddResult1 = await CardPileCmd.Add(Owner.RunState.CreateCard(jake, Owner), PileType.Deck);
        var cardPileAddResult2 = await CardPileCmd.Add(Owner.RunState.CreateCard(aljmor, Owner), PileType.Deck);
        
        cardPileAddResultList.Add(cardPileAddResult1);
        cardPileAddResultList.Add(cardPileAddResult2);
        CardCmd.PreviewCardPileAdd(cardPileAddResultList, 2f);
    }
}