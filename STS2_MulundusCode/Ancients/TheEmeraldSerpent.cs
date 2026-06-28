using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Models;
using STS2_Mulundus.STS2_MulundusCode.Relics;

namespace STS2_Mulundus.STS2_MulundusCode.Ancients;


public class TheEmeraldSerpent : CustomAncientModel
{
    protected override OptionPools MakeOptionPools => new([
        AncientOption<TideglassPendant>()
    ], [
        AncientOption<SigilOfBrine>()
    ], [
        AncientOption<EmeraldScale>()
    ]);
    
    public override bool IsValidForAct(ActModel act) => act.ActNumber() == 2;

    public override bool ShouldForceSpawn(ActModel act, AncientEventModel? rngChosenAncient) => false;
    
    public override string? CustomScenePath => "res://STS2_Mulundus/scenes/events/background_scenes/sts2_mulundus-the_emerald_serpent.tscn";

    public override string? CustomMapIconPath => "res://STS2_Mulundus/images/ui/run_history/sts2_mulundus-the_emerald_serpent.png";

    public override string? CustomMapIconOutlinePath => "res://STS2_Mulundus/images/ui/run_history/sts2_mulundus-the_emerald_serpent.png";
    
    public override string? CustomRunHistoryIconPath => "res://STS2_Mulundus/images/ui/run_history/sts2_mulundus-the_emerald_serpent.png";

    public override string? CustomRunHistoryIconOutlinePath => "res://STS2_Mulundus/images/ui/run_history/sts2_mulundus-the_emerald_serpent.png";
}