using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Uncommon;

[Pool(typeof(HeartwoodRangerCardPool))]
public class ToxinFueledRage : HeartWoodRangerCard
{
    
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/toxin_fueled_rage.png";
    public ToxinFueledRage() : base(4, CardType.Power, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<ToxicRagePower>(1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<ToxicRagePower>(choiceContext, this);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
    
    public override Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (power is PoisonPower && applier == Owner.Creature)
        {
            EnergyCost.AddThisCombat(-1);
        }
        return Task.CompletedTask;
    }
}