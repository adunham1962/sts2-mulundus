using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Uncommon;

[Pool(typeof(EmeraldMonkCardPool))]
public class BreakingWave : EmeraldMonkCard
{

    public BreakingWave() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies)
    {
        WithDamage(10);
        WithPower<DrenchedPower>(3);
        WithKeyword(EmeraldMonkKeywords.Flow);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        if (CombatState != null) await CommonActions.Apply<DrenchedPower>(choiceContext, CombatState.HittableEnemies, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["DrenchedPower"].UpgradeValueBy(3);
    }
}