using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;
using static MegaCrit.Sts2.Core.Entities.Cards.PileType;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Rare;

[Pool(typeof(HeartwoodRangerCardPool))]
public class ToxicSpiral : HeartWoodRangerCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/toxic_spiral.png";
    
    public ToxicSpiral() : base(0, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithPower<PoisonPower>(2);
        WithCalculatedVar("CalculatedHeal", 0, 1, (c, _) => c.Owner.Creature.GetPowerAmount<PoisonPower>());
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        var card = Draw.GetPile(Owner).Cards[0];
        await CardCmd.Exhaust(choiceContext, card);
        await CommonActions.ApplySelf<PoisonPower>(choiceContext, this);
        await CreatureCmd.Heal(Owner.Creature,
            (DynamicVars["CalculatedHeal"] as CalculatedVar)!.Calculate(Owner.Creature));
    }

    protected override void OnUpgrade()
    {
        DynamicVars["CalculatedHealExtra"].UpgradeValueBy(1);
    }
}