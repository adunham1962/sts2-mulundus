using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Rare;

[Pool(typeof(HeartwoodRangerCardPool))]
public class PoreOverThePages : HeartWoodRangerCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/pore_over_the_pages.png";
    public PoreOverThePages() : base(1, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
        WithPower<PoreOverThePagesPower>(1);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.ApplySelf<PoreOverThePagesPower>(choiceContext, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["PoreOverThePagesPower"].UpgradeValueBy(1);
    }
}