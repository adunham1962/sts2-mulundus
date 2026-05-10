using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Token;
[Pool(typeof(TokenCardPool))]
public class Goodberry : HeartWoodRangerCard
{
    public Goodberry() : base(0, CardType.Skill, CardRarity.Token, TargetType.Self)
    {
        WithHeal(1);
        WithKeyword(CardKeyword.Exhaust);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.Heal(Owner.Creature, DynamicVars.Heal.BaseValue);
    }

    public static IEnumerable<Goodberry> Create(Player owner, decimal amount, CombatState combatState)
    {
        var goodBerryList = new List<Goodberry>();
        for (var index = 0; index < amount; ++index)
            goodBerryList.Add(combatState.CreateCard<Goodberry>(owner));
        return goodBerryList;
    }
    
    protected override void OnUpgrade()
    {
        DynamicVars.Heal.UpgradeValueBy(1);
    }
}