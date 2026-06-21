using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Special;
[Pool(typeof(TokenCardPool))]
public class ConjureFog : EmeraldMonkCard
{
    public ConjureFog() : base(1, CardType.Skill, CardRarity.Token, TargetType.Self)
    {
        WithPower<SlipperyPower>(1);
        WithKeyword(EmeraldMonkKeywords.Stance);
        WithKeyword(CardKeyword.Retain);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (CombatState != null) await CommonActions.Apply<SlipperyPower>(choiceContext, CombatState.Creatures, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["SlipperyPower"].UpgradeValueBy(1);
    }
    
    public static IEnumerable<ConjureFog> Create(Player owner, decimal amount, ICombatState combatState)
    {
        var conjureFogs = new List<ConjureFog>();
        for (var index = 0; index < amount; ++index)
            conjureFogs.Add(combatState.CreateCard<ConjureFog>(owner));
        return conjureFogs;
    }
}