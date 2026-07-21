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
    
    public SliceOfLife() : base(2, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies)
    {
        WithCalculatedDamage(8, 8, (card, _) => CardsExhaustedThisTurn(card));
        WithCalculatedVar("CalculatedHeal", 1, 1, (card, _) => CardsExhaustedThisTurn(card));
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

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationBase.UpgradeValueBy(2);
        DynamicVars.ExtraDamage.UpgradeValueBy(2);
    }
}