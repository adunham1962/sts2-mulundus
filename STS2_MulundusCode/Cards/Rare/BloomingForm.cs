using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Rare;
[Pool(typeof(HeartwoodRangerCardPool))]
public class BloomingForm : HeartWoodRangerCard
{

    public BloomingForm() : base(3, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
        WithPower<PoisonPower>(2);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<BloomingFormPower>(Owner.Creature, DynamicVars["PoisonPower"].BaseValue, Owner.Creature,
            this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["PoisonPower"].UpgradeValueBy(2);
    }
}