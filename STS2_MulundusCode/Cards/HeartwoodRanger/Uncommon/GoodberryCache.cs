using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2_Mulundus.STS2_MulundusCode.Cards.Token;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Uncommon;
[Pool(typeof(HeartwoodRangerCardPool))]
public class GoodberryCache : HeartWoodRangerCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/goodberry_cache.png";

    public GoodberryCache() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithTips(_ => [HoverTipFactory.FromCard<Goodberry>()]);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var berries = Goodberry.Create(Owner, 10, CombatState!).ToList();
        if (IsUpgraded)
        {
            berries.ForEach(b => CardCmd.Upgrade(b));
        }
        var card = this;
        var pendingResults = berries.Select(b => CardPileCmd.AddGeneratedCardToCombat(b, PileType.Discard, true)).ToList();
        var results = await Task.WhenAll(pendingResults);
        CardCmd.PreviewCardPileAdd(results);
    }

    protected override void OnUpgrade()
    {
        
    }
}