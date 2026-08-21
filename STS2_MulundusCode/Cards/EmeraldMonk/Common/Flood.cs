using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Common;

[Pool(typeof(EmeraldMonkCardPool))]
public class Flood : EmeraldMonkCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/flood.png";
    public Flood() : base(1, CardType.Skill, CardRarity.Common, TargetType.AllEnemies)
    {
        WithPower<DrenchedPower>(3);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (CombatState != null)
        {
            await CommonActions.Apply<DrenchedPower>(choiceContext, CombatState.HittableEnemies, this);
        }
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}