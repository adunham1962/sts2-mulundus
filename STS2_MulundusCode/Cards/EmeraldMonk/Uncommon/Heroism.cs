using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Uncommon;
[Pool(typeof(EmeraldMonkCardPool))]
public class Heroism : EmeraldMonkCard
{
    protected override bool ShouldGlowGoldInternal => ShouldGlowGoldFromBalance;

    public Heroism() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<WisdomPower>(2);
        WithHeal(4);
        WithKeyword(CardKeyword.Exhaust);
        WithTip(EmeraldMonkKeywords.Balanced);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        
        var statuses = PileType.Draw.GetPile(Owner).Cards.Where(card => card.Type == CardType.Status).ToList();
        statuses = statuses.Concat(PileType.Hand.GetPile(Owner).Cards.Where(card => card.Type == CardType.Status))
            .ToList();
        statuses = statuses.Concat(PileType.Discard.GetPile(Owner).Cards.Where(card => card.Type == CardType.Status))
            .ToList();

        foreach (var cardModel in statuses)
        {
            await CardCmd.Exhaust(choiceContext, cardModel);
        }

        if (TreatAsBalancedWhilePlaying)
        {
            await CommonActions.ApplySelf<WisdomPower>(choiceContext, this);
        }
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}