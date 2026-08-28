using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Enchantments;
using STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Special;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Uncommon;
[Pool(typeof(EmeraldMonkCardPool))]
public class OctopusStance : EmeraldMonkCard
{
    
    public OctopusStance() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithKeyword(EmeraldMonkKeywords.Sink);
        WithTips(_ => [HoverTipFactory.FromCard<TentacleLash>()]);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (CombatState is null || play.Card != this) return;
        var cards = TentacleLash.Create(Owner, 3, CombatState).ToList();
        if (IsUpgraded)
        {
            cards.ForEach(card => CardCmd.Upgrade(card));
            cards.ForEach(card => CardCmd.Enchant<Inky>(card, 1));
        }
        
        await CardPileCmd.AddGeneratedCardsToCombat(cards, PileType.Hand, Owner);
    }
}