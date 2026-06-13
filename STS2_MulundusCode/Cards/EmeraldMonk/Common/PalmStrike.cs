using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Common;
[Pool(typeof(EmeraldMonkCardPool))]
public class PalmStrike : EmeraldMonkCard
{
    public PalmStrike() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithDamage(6);
        WithPower<VulnerablePower>(2);
        WithTags(CardTag.Strike);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        var lastCard = CombatManager.Instance.History.Entries.OfType<CardPlayFinishedEntry>().LastOrDefault(e => e.Actor == Owner.Creature)?.CardPlay.Card;
        if (lastCard is not null && lastCard.Owner == Owner && lastCard.Type == CardType.Skill && play.Target is not null)
        {
            await CommonActions.Apply<VulnerablePower>(choiceContext, play.Target, this);
        }
    }

    protected override void OnUpgrade()
    {
        AddKeyword(EmeraldMonkKeywords.Flow);
    }
}