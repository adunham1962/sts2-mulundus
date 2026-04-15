using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards;

[Pool(typeof(HeartwoodRangerCardPool))]
public class Barkskin : HeartWoodRangerCard
{

    public Barkskin() : base(2, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithBlock(12);
        WithPower<PlatingPower>(4);
        WithKeyword(CardKeyword.Exhaust);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
        await CommonActions.ApplySelf<PlatingPower>(this, 4);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(4);
    }
}