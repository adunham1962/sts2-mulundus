using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Rare;
[Pool(typeof(EmeraldMonkCardPool))]
public class BalanceTheScales : EmeraldMonkCard
{

    public BalanceTheScales() : base(3, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy)
    {
        WithKeyword(CardKeyword.Ethereal);
        WithKeyword(CardKeyword.Exhaust);
    }

    protected override Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        Owner.Creature.Powers.ToList().Where(power => power.Type == PowerType.Debuff).ToList().ForEach(power => power.SetAmount(0));
        play.Target?.Powers.ToList().Where(power => power.Type == PowerType.Buff).ToList().ForEach(power => power.SetAmount(0));
        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}