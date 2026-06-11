using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Rare;

[Pool(typeof(HeartwoodRangerCardPool))]
public class SliceOfLife : HeartWoodRangerCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/slice_of_life.png";
    //private int _exhaustCount;
    
    public SliceOfLife() : base(2, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies)
    {
        WithCalculatedDamage(8, 8, (card, _) => CardsExhaustedThisTurn(card));
        //WithDamage(8);
        //WithHeal(1);
        WithCalculatedVar("CalculatedHeal", 1, 1, (card, _) => CardsExhaustedThisTurn(card));
        WithKeyword(CardKeyword.Exhaust);
        //_exhaustCount = 0;
    }

    private static decimal CardsExhaustedThisTurn(CardModel card)
    {
        return CombatManager.Instance.History.Entries.OfType<CardExhaustedEntry>().Count(e =>
            e.HappenedThisTurn(card.CombatState) && e.Actor == card.Owner.Creature);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        await CreatureCmd.Heal(Owner.Creature, (DynamicVars["CalculatedHeal"] as CalculatedVar)!.Calculate(null));
    }

   // public override Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool causedByEthereal)
   // {
     //   _exhaustCount++;
      //  return Task.CompletedTask;
    //}

    //public override Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    //{
    //    _exhaustCount = 0;
    //    return Task.CompletedTask;
    //}

    protected override void OnUpgrade()
    {
        //DynamicVars.Damage.UpgradeValueBy(2);
        DynamicVars.CalculationBase.UpgradeValueBy(2);
        DynamicVars.ExtraDamage.UpgradeValueBy(2);
    }
}