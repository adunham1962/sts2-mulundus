using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Uncommon;
[Pool(typeof(HeartwoodRangerCardPool))]
public class LifeOnTheEdge : HeartWoodRangerCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/life_on_the_edge.png";
    public LifeOnTheEdge() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<LifeOnTheEdgePower>(1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<LifeOnTheEdgePower>(choiceContext, this);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Innate);
    }
}