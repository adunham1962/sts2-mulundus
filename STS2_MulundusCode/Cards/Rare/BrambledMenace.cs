using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Rare;
[Pool(typeof(HeartwoodRangerCardPool))]
public class BrambledMenace() : HeartWoodRangerCard(1, CardType.Power, CardRarity.Rare, TargetType.Self)
{

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var thorns = Owner.Creature.GetPowerAmount<ThornsPower>();
        await PowerCmd.SetAmount<ThornsPower>(Owner.Creature, thorns * 2, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}