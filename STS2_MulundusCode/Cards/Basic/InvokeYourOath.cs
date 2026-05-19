using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Basic;
[Pool(typeof(HeartwoodRangerCardPool))]
public class InvokeYourOath : HeartWoodRangerCard
{

    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/invoke_your_oath.png";
    public InvokeYourOath() : base(1, CardType.Skill, CardRarity.Basic, TargetType.Self)
    {
        WithKeyword(CardKeyword.Exhaust);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<InvokeYourOathPower>(this.Owner.Creature, 1, this.Owner.Creature, this);
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        await PowerCmd.Remove<InvokeYourOathPower>(this.Owner.Creature);
    }
    
    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}