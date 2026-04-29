using System.Threading.Tasks;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Common;

[Pool(typeof(HeartwoodRangerCardPool))]
public class SpikeGrowth : HeartWoodRangerCard
{
    public SpikeGrowth() : base(1, CardType.Power, CardRarity.Uncommon, MegaCrit.Sts2.Core.Entities.Cards.TargetType.Self)
    {
        WithPower<ThornsPower>(3);
    }

    protected new IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<ThornsPower>(3)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<ThornsPower>(this.Owner.Creature, 3m, this.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars["ThornsPower"].UpgradeValueBy(1m);
    }
}