using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Rare;
[Pool(typeof(HeartwoodRangerCardPool))]
public class TheDowagersDeal : HeartWoodRangerCard
{
    public TheDowagersDeal() : base(0, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithCards(2);
        WithPower<StrengthPower>(2);
        WithKeyword(HeartwoodRangerKeywords.Grim);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.LoseMaxHp(choiceContext, Owner.Creature, 1, true);
        await CommonActions.Draw(this, choiceContext);
        await CommonActions.ApplySelf<StrengthPower>(this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Cards.UpgradeValueBy(1);
    }
}