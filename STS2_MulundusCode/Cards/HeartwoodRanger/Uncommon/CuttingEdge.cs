using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Uncommon;
[Pool(typeof(HeartwoodRangerCardPool))]
public class CuttingEdge : HeartWoodRangerCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/cutting_edge.png";
    public CuttingEdge() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<CuttingEdgePower>(2);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<CuttingEdgePower>(this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["CuttingEdgePower"].UpgradeValueBy(1);
    }
}