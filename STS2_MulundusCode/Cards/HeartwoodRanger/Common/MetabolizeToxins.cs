using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Common;
[Pool(typeof(HeartwoodRangerCardPool))]
public class MetabolizeToxins() : HeartWoodRangerCard(0, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/metabolize.png";

    protected override bool HasEnergyCostX => true;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var xValue = ResolveEnergyXValue();
        var powers = Owner.Creature.Powers.ToList();
        var debuffs = powers.FindAll(p => p.Type == PowerType.Debuff).Count;
        await CreatureCmd.Heal(Owner.Creature, xValue * debuffs);
        var poison = powers.Find(p => p is PoisonPower);
        if (poison is not null)
        {
            await CommonActions.ApplySelf<PoisonPower>(choiceContext, this, poison.Amount * -1);
        }
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}