using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Common;
[Pool(typeof(HeartwoodRangerCardPool))]
public class DreadStrike : HeartWoodRangerCard
{

    public DreadStrike() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithDamage(6);
        WithTags(CardTag.Strike);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {


        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        await DamageCmd.Attack(this.DynamicVars.Damage.BaseValue +
                             (play.Card.Owner.PlayerCombatState.ExhaustPile.Cards.Count)).FromCard(this).Targeting(play.Target).Execute(choiceContext);
        await CardCmd.Exhaust(choiceContext, PileType.Draw.GetPile(this.Owner).Cards.First());

    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Damage.UpgradeValueBy(3m);
    }
}