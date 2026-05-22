using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Ancient;

[Pool(typeof(EventCardPool))]
public class BloodWarp : ConstructedCardModel
{
    
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/blood_warp.png";
    
    public BloodWarp() : base(1, CardType.Skill, CardRarity.Ancient, TargetType.Self)
    {
        WithKeyword(CardKeyword.Exhaust);
        WithPower<IntangiblePower>(1);
        WithVar("LoseHp", 2);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.LoseMaxHp(choiceContext, Owner.Creature, DynamicVars["LoseHp"].BaseValue, true);
        await CommonActions.ApplySelf<IntangiblePower>(this);
    }

    protected override void OnUpgrade()
    {
        RemoveKeyword(CardKeyword.Exhaust);
    }
}