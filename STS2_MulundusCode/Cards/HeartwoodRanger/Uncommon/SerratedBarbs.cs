using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Uncommon;
[Pool(typeof(HeartwoodRangerCardPool))]
public class SerratedBarbs : HeartWoodRangerCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/serrated_barbs.png";
    public SerratedBarbs() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<SerratedBarbsPower>(1);
        WithKeyword(HeartwoodRangerKeywords.Grim);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<ThornsPower>(choiceContext, this, Owner.Creature.GetPowerAmount<ThornsPower>() * 2);
        await CommonActions.ApplySelf<SerratedBarbsPower>(choiceContext, this);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}