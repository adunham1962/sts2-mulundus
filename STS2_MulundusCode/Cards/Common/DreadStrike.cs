using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using STS2_Mulundus.STS2_MulundusCode.Character;
using static MegaCrit.Sts2.Core.Entities.Cards.PileType;

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
        var damage = DynamicVars.Damage.BaseValue;
        if (play.Card.Owner.PlayerCombatState != null)
        {
            damage = DynamicVars.Damage.BaseValue + (play.Card.Owner.PlayerCombatState.ExhaustPile.Cards.Count);
        }

        await DamageCmd.Attack(damage).FromCard(this)
                             .Targeting(play.Target).Execute(choiceContext);
        if (CardPile.Get(Draw, Owner) is not { IsEmpty: true })
        {
            await CardCmd.Exhaust(choiceContext, Draw.GetPile(this.Owner).Cards[0]);
        }
        

    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Damage.UpgradeValueBy(3m);
    }
}