using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Uncommon;

[Pool(typeof(EmeraldMonkCardPool))]
public class Drench : EmeraldMonkCard
{

    public Drench() : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithPower<DrenchedPower>(6);
        WithKeyword(CardKeyword.Exhaust);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        if (play.Target != null)
        {
            await CommonActions.Apply<DrenchedPower>(choiceContext, play.Target, this);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["DrenchedPower"].UpgradeValueBy(3);
    }
}