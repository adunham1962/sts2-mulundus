using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Uncommon;
[Pool(typeof(EmeraldMonkCardPool))]
public class ControlWater : EmeraldMonkCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/control_water.png";
    public ControlWater() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithKeyword(MulundusKeywords.Absorb);
        WithKeyword(EmeraldMonkKeywords.Flow);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        if (CombatState == null) return;
        var amount = await Consume<DrenchedPower>(CombatState.Creatures);

        await PlayerCmd.GainEnergy(amount, Owner);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(EmeraldMonkKeywords.Flow);
    }
}