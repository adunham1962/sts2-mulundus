using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Common;

[Pool(typeof(EmeraldMonkCardPool))]
public class UnarmedStrike : EmeraldMonkCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/unarmed_strike.png";
    public UnarmedStrike() : base(0, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithTags(CardTag.Strike);
        WithCalculatedDamage(4,
            (card, _) => CombatManager.Instance.History.Entries.OfType<CardPlayFinishedEntry>().Count(e =>
                e.CardPlay.Card.Type == CardType.Attack && e.HappenedThisTurn(card.CombatState)) > 0
                ? 0
                : 4);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationBase.UpgradeValueBy(3);
    }
}