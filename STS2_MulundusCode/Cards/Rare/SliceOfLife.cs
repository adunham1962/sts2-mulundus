using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Rare;

[Pool(typeof(HeartwoodRangerCardPool))]
public class SliceOfLife : HeartWoodRangerCard
{

    private int _exhaustCount;
    
    public SliceOfLife() : base(2, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies)
    {
        WithDamage(8);
        WithHeal(1);
        WithKeyword(CardKeyword.Exhaust);
        _exhaustCount = 0;
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        CommonActions.CardAttack(this, play, _exhaustCount + 1);
        await CreatureCmd.Heal(Owner.Creature, DynamicVars.Heal.BaseValue +  _exhaustCount);
    }

    public override Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool causedByEthereal)
    {
        _exhaustCount++;
        return Task.CompletedTask;
    }

    public override Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        _exhaustCount = 0;
        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2);
    }
}