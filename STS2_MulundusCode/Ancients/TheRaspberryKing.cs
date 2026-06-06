using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Events;
using STS2_Mulundus.STS2_MulundusCode.Relics;

namespace STS2_Mulundus.STS2_MulundusCode.Ancients;

public class TheRaspberryKing : CustomAncientModel
{
    protected override OptionPools MakeOptionPools => new([
        AncientOption<PoisonousFlower>(weight: 35),
        AncientOption<BindosSpecialMushroom>(weight: 30),
        AncientOption<RaspberryJam>(25),
        AncientOption<SilvanTotem>(weight: 10)
    ], [
        AncientOption<WhimsicalGift>(weight: 35),
        AncientOption<SilvanSprig>(weight:30),
        AncientOption<RaspberryKnightsHelm>(weight: 25),
        AncientOption<TheGoldenRaspberry>(weight: 10)
    ], [
        AncientOption<PairOfBears>(weight: 35),
        AncientOption<RaspberryWine>(weight: 30),
        AncientOption<RaspberryPie>(weight: 25),
        AncientOption<ReforgedSoul>(weight: 10)
    ]);

    public override bool ShouldForceSpawn(ActModel act, AncientEventModel? rngChosenAncient) => rngChosenAncient is Neow;

    public override string? CustomScenePath => "res://STS2_Mulundus/scenes/events/background_scenes/sts2_mulundus-the_raspberry_king.tscn";

    public override string? CustomMapIconPath => "res://STS2_Mulundus/images/ui/run_history/sts2_mulundus-the_raspberry_king.png";

    public override string? CustomMapIconOutlinePath => "res://STS2_Mulundus/images/packed/map/ancients/ancient_node_sts2_mulundus-the_raspberry_king_outline.png";
    
    public override string? CustomRunHistoryIconPath => "res://STS2_Mulundus/images/ui/run_history/sts2_mulundus-the_raspberry_king.png";

    public override string? CustomRunHistoryIconOutlinePath => "res://STS2_Mulundus/images/packed/map/ancients/ancient_node_sts2_mulundus-the_raspberry_king_outline.png";

    public override bool IsValidForAct(ActModel act) => false;
}