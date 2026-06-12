using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Special;

[Pool(typeof(TokenCardPool))]
public class SlipIntoShadow : EmeraldMonkCard
{
    public SlipIntoShadow() : base(3, CardType.Skill, CardRarity.Token, TargetType.Self)
    {
        WithPower<IntangiblePower>(1);
        WithKeyword(CardKeyword.Retain);
        WithKeyword(EmeraldMonkKeywords.Stance);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<IntangiblePower>(choiceContext, this);
    }

    protected override void OnUpgrade()
    {

    }

    public static IEnumerable<SlipIntoShadow> Create(Player owner, decimal amount, CombatState combatState)
    {
        var slips = new List<SlipIntoShadow>();
        for (var index = 0; index < amount; ++index)
            slips.Add(combatState.CreateCard<SlipIntoShadow>(owner));
        return slips;
    }
}