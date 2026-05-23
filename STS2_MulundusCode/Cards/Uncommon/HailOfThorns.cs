using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Uncommon;
[Pool(typeof(HeartwoodRangerCardPool))]
public class HailOfThorns : HeartWoodRangerCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/hail_of_thorns.png";
    public HailOfThorns() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.RandomEnemy)
    {
        WithDamage(2);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var thorns = Owner.Creature.Powers.ToList().Find(p => p is ThornsPower);
        var hits = thorns?.Amount ?? 0;
        await CommonActions.CardAttack(this, play, hits).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2);
    }
}