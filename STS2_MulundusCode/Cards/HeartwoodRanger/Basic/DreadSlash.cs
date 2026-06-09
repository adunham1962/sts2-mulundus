using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Basic;
[Pool(typeof(HeartwoodRangerCardPool))]
public class DreadSlash : HeartWoodRangerCard
{

    protected override bool ShouldGlowGoldInternal => OwnerHasExhaustCard();
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/dread_slash.png";
    public DreadSlash() : base(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
    {
        WithDamage(8);
        WithPower<VulnerablePower>(1);
        WithPower<WeakPower>(1);
    }

    private bool OwnerHasExhaustCard()
    {
        return PileType.Exhaust.GetPile(Owner).Cards.Count > 0;
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);

        if (play.Card.Owner.PlayerCombatState is { ExhaustPile.IsEmpty: false } && play.Target != null)
        {
            await CommonActions.Apply<VulnerablePower>(play.Target, this, 1);
            await CommonActions.Apply<WeakPower>(play.Target, this, 1);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3m);
    }
}