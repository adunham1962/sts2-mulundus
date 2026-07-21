using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Rare;
[Pool(typeof(HeartwoodRangerCardPool))]
public class SurvivalOfTheFittest : HeartWoodRangerCard
{

    public SurvivalOfTheFittest() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithPower<ConstitutionPower>(5);
        WithBlock(10);
        WithKeyword(CardKeyword.Exhaust);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
        await CommonActions.ApplySelf<ConstitutionPower>(choiceContext, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["ConstitutionPower"].UpgradeValueBy(5);
    }
}