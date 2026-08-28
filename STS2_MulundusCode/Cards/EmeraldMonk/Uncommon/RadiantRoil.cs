using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Uncommon;

[Pool(typeof(EmeraldMonkCardPool))]
public class RadiantRoil : EmeraldMonkCard
{
    public RadiantRoil() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithTip(EmeraldMonkKeywords.Balanced);
        WithCards(4);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CardCmd.Discard(choiceContext, PileType.Hand.GetPile(Owner).Cards);
        await CommonActions.Draw(this, choiceContext);
        var balanced = PileType.Hand.GetPile(Owner).Cards
            .Count(c => c.Type == CardType.Attack) == PileType.Hand
            .GetPile(Owner).Cards.Count(c => c.Type == CardType.Skill);
        var heal = PileType.Hand.GetPile(Owner).Cards.Sum(card => card.EnergyCost.GetResolved());
        if (balanced)
        {
            await CreatureCmd.Heal(Owner.Creature, heal);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Cards.UpgradeValueBy(2);
    }
}