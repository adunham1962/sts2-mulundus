using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Special;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Common;
[Pool(typeof(EmeraldMonkCardPool))]
public class TurtleStance : EmeraldMonkCard
{
    public TurtleStance() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithKeyword(EmeraldMonkKeywords.Sink);
        WithTips(_ => [HoverTipFactory.FromCard<Withdraw>()]);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (CombatState is null || play.Card != this) return;
        var cards = RainBarrage.Create(Owner, 1, CombatState).ToList();
        cards.ForEach(card =>
        {
            if (IsUpgraded) CardCmd.Upgrade(card);
        });

        await CardPileCmd.AddGeneratedCardsToCombat(cards, PileType.Hand, Owner);
    }

    protected override void OnUpgrade()
    {

    }
}