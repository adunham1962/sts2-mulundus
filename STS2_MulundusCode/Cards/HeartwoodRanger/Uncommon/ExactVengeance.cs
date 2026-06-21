using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Uncommon;
[Pool(typeof(HeartwoodRangerCardPool))]
public class ExactVengeance : HeartWoodRangerCard
{
    public ExactVengeance() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithCalculatedDamage(7, 3, (card, creature) => CombatManager.Instance.History.Entries.OfType<DamageReceivedEntry>().Count(e => e.Receiver == card.Owner.Creature && e.Dealer is { IsMonster: true }));
        
        //WithCalculatedDamage(7, 3, (card, _) => 2, ValueProp.Move, 0, 1);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (play.Target is null) return;
        await DamageCmd.Attack(DynamicVars.CalculatedDamage).FromCard(this).Targeting(play.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.ExtraDamage.UpgradeValueBy(1);
    }
}