using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Ancient;
[Pool(typeof(EventCardPool))]
public class AcornBuddy : ConstructedCardModel
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/acorn_buddy.png";
    
    public AcornBuddy() : base(1, CardType.Power, CardRarity.Ancient, TargetType.Self)
    {
        WithVar("GainHp", 1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.GainMaxHp(Owner.Creature, DynamicVars["GainHp"].BaseValue);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}