using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Ancient;
[Pool(typeof(EventCardPool))]
public class TheGoldenRaspberry : ConstructedCardModel
{
    
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/the_golden_raspberry.png";
    
    
    public TheGoldenRaspberry() : base(0, CardType.Skill, CardRarity.Ancient, TargetType.Self)
    {
        WithCards(1);
        WithKeyword(CardKeyword.Exhaust);
        WithHeal(1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.Draw(this, choiceContext);
        await CreatureCmd.Heal(Owner.Creature, DynamicVars.Heal.BaseValue);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Heal.UpgradeValueBy(1);
    }
}