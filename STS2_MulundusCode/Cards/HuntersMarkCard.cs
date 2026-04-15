using BaseLib.Abstracts;
using BaseLib.Utils;
using HarmonyLib;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Cards;
using STS2_Mulundus.STS2_MulundusCode.Character;

[Pool(typeof(HeartwoodRangerCardPool))]
public class HuntersMark : HeartWoodRangerCard {

    public HuntersMark() : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithPower<VulnerablePower>(1);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        
        await CommonActions.Apply<VulnerablePower>(play.Target, this, DynamicVars.Vulnerable.BaseValue);
        
        //TODO: Implement logic for returning to hand when an enemy dies
    }

    protected override void OnUpgrade()
    {
        
    }
}