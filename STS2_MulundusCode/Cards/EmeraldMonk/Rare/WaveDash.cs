using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Rare;

[Pool(typeof(EmeraldMonkCardPool))]
public class WaveDash : EmeraldMonkCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/wave_dash.png";
    public WaveDash() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithPower<SlipperyPower>(1);
        WithKeyword(EmeraldMonkKeywords.Ebb);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<SlipperyPower>(choiceContext, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["SlipperyPower"].UpgradeValueBy(1);
    }
}