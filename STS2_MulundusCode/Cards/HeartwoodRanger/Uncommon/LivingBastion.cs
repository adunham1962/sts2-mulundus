using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Uncommon;
[Pool(typeof(HeartwoodRangerCardPool))]
public class LivingBastion : HeartWoodRangerCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/living_bastion.png";
    public LivingBastion() : base(2, CardType.Power, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<LivingBastionPower>(1);
        WithKeyword(HeartwoodRangerKeywords.Grim);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<LivingBastionPower>(choiceContext, this);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Innate);
    }
}