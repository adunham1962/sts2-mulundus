using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Special;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Common;

[Pool(typeof(EmeraldMonkCardPool))]
public class RainStance : EmeraldMonkCard
{
    public RainStance() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithKeyword(MulundusKeywords.Consume);
        WithKeyword(EmeraldMonkKeywords.Sink);
        WithTips(_ => [HoverTipFactory.FromCard<RainBarrage>()]);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (CombatState is null || play.Card != this) return;
        var amount = await Consume<SlipperyPower>(CombatState.HittableEnemies);
        amount += await Consume<DownpourPower>(CombatState.HittableEnemies);
        var cards = RainBarrage.Create(Owner, amount, CombatState).ToList();
        cards.ForEach(card =>
        {
            if (IsUpgraded) CardCmd.Upgrade(card);
        });

        LatestCardsCreated = cards;

        await CardPileCmd.AddGeneratedCardsToCombat(cards, PileType.Hand, Owner);
    }
}