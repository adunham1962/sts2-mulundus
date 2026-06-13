using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Special;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Uncommon;
[Pool(typeof(EmeraldMonkCardPool))]
public class DragonStance : EmeraldMonkCard
{

    public DragonStance() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithKeyword(EmeraldMonkKeywords.EnterStance);
        WithTips(_ => [HoverTipFactory.FromCard<DragonsTorrent>()]);
    }

    public override async Task AfterCardPlayedLate(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (CombatState is null || cardPlay.Card != this) return;
        var card = DragonsTorrent.Create(Owner, 1, CombatState).ToList()[0];
        if (IsUpgraded)
        {
            CardCmd.Upgrade(card);
        }

        await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, true);
    }
    
}