using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Uncommon;
[Pool(typeof(EmeraldMonkCardPool))]
public class Maelstrom : EmeraldMonkCard
{
    public Maelstrom() : base(3, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies)
    {
        WithDamage(3);
        WithKeyword(EmeraldMonkKeywords.Flow);
        WithPower<DrenchedPower>(3);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play, 3).Execute(choiceContext);
        if (CombatState != null)
            await CommonActions.Apply<DrenchedPower>(choiceContext, CombatState.HittableEnemies, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2);
    }
}