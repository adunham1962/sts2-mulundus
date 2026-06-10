using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Special;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Basic;
[Pool(typeof(EmeraldMonkCardPool))]
public class EmeraldSerpentStance : EmeraldMonkCard
{
    
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/emerald_serpent_stance.png";
    
    public EmeraldSerpentStance() : base(1, CardType.Skill, CardRarity.Basic, TargetType.Self)
    {
        WithKeyword(EmeraldMonkKeywords.EnterStance);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (CombatState is null) return;
        var card = SerpentStrike.Create(Owner, 1, CombatState).ToList()[0];
        if (IsUpgraded)
        {
            CardCmd.Upgrade(card);
        }

        await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, true);
    }
}