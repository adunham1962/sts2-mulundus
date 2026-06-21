using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Uncommon;
[Pool(typeof(EmeraldMonkCardPool))]
public class TorrentialAssault : EmeraldMonkCard
{

    public TorrentialAssault() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithDamage(2);
        WithCalculatedVar("CalculatedHits", 2, (card, _) => CombatManager.Instance.History.Entries.OfType<CardPlayFinishedEntry>().Count(e =>
            e.HappenedThisTurn(card.CombatState) && e.Actor == card.Owner.Creature && e.CardPlay.Card.Type == CardType.Attack), 2);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var hits = (DynamicVars["CalculatedHits"] as CalculatedVar)!.Calculate(null);
        await CommonActions.CardAttack(this, play, (int)hits).Execute(choiceContext);
    }
}