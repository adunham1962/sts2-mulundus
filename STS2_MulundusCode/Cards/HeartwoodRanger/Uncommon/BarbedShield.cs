using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Uncommon;

[Pool(typeof(HeartwoodRangerCardPool))]
public class BarbedShield : HeartWoodRangerCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/barbed_shield.png";
    public BarbedShield() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<ThornsPower>(1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var thorns = Owner.Creature.Powers.ToList().Find((match) => match is ThornsPower);
        if (thorns is not null)
        {
            await CreatureCmd.GainBlock(Owner.Creature, thorns.Amount, ValueProp.Move, play);
        }

        await CommonActions.ApplySelf<ThornsPower>(choiceContext, this);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}