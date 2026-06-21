using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Rare;
[Pool(typeof(HeartwoodRangerCardPool))]
public class BrambledMenace() : HeartWoodRangerCard(1, CardType.Power, CardRarity.Rare, TargetType.Self)
{

    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/brambled_menace.png";
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var thorns = Owner.Creature.GetPowerAmount<ThornsPower>();
        var power = Owner.Creature.GetPower<ThornsPower>();
        if (power != null) await PowerCmd.ModifyAmount(choiceContext, power, thorns, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}