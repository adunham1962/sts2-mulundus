using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Uncommon;

[Pool(typeof(HeartwoodRangerCardPool))]
public class Longstrider : HeartWoodRangerCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/longstrider.png";
    public Longstrider() : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<DexterityPower>(1);
        WithBlock(9);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<DexterityPower>(choiceContext, this);
        await CommonActions.CardBlock(this, play);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}