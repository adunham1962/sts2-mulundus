using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Basic;
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
        await PowerCmd.Apply<InvokeYourOathPower>(choiceContext, Owner.Creature, 1, Owner.Creature, this);
    }
    
    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}