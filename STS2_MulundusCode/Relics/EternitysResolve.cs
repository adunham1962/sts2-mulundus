using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using STS2_Mulundus.STS2_MulundusCode.Relics;

namespace STS2_Mulundus.STS2_MulundusCode.Relics;


[Pool(typeof(EventRelicPool))]
public class EternitysResolve() : STS2_MulundusRelic
{
    public override RelicRarity Rarity => RelicRarity.Ancient;
    public override string PackedIconPath => "res://STS2_Mulundus/images/relics/eternitys_resolve.png";

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromCard<Cards.Ancient.EternitysResolve>()
    ];
    
    public override async Task AfterObtained()
    {
        var cardPileAddResultList = new List<CardPileAddResult>(1);
        var card = ModelDb.Card<Cards.Ancient.EternitysResolve>();
        var cardPileAddResult = await CardPileCmd.Add(Owner.RunState.CreateCard(card, Owner), PileType.Deck);
        cardPileAddResultList.Add(cardPileAddResult);
        CardCmd.PreviewCardPileAdd(cardPileAddResultList, 2f);
    }
}