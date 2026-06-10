using System.Runtime.InteropServices;
using BaseLib.Abstracts;
using BaseLib.Utils.NodeFactories;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Basic;
using STS2_Mulundus.STS2_MulundusCode.Extensions;
using STS2_Mulundus.STS2_MulundusCode.Relics;

namespace STS2_Mulundus.STS2_MulundusCode.Character;

public class EmeraldMonk : PlaceholderCharacterModel
{
    public const string CharacterId = "STS2_Mulundus_Emerald_Monk";

    public override string PlaceholderID => "silent";
    
    public static readonly Color Color = StsColors.aqua;

    public override Color NameColor => Color;
    public override CharacterGender Gender => CharacterGender.Masculine;
    
    protected override CharacterModel? UnlocksAfterRunAs => null;
    
    public override int StartingGold => 99;
    public override int StartingHp => 55;
    
    public override IEnumerable<CardModel> StartingDeck =>
    [
        ModelDb.Card<EmeraldMonkStrike>(),
        ModelDb.Card<EmeraldMonkStrike>(),
        ModelDb.Card<EmeraldMonkStrike>(),
        ModelDb.Card<EmeraldMonkStrike>(),
        ModelDb.Card<EmeraldMonkDefend>(),
        ModelDb.Card<EmeraldMonkDefend>(),
        ModelDb.Card<EmeraldMonkDefend>(),
        ModelDb.Card<EmeraldMonkDefend>(),
        ModelDb.Card<WaterWhip>(),
        ModelDb.Card<EmeraldSerpentStance>()
    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<EmeraldSerpentSash>()
    ];

    public override List<string> GetArchitectAttackVfx()
    {
        const int num = 5;
        var list = new List<string>(num);
        CollectionsMarshal.SetCount(list, num);
        var span = CollectionsMarshal.AsSpan(list);
        const int index1 = 0;
        span[index1] = "vfx/vfx_attack_blunt";
        const int index2 = index1 + 1;
        span[index2] = "vfx/vfx_heavy_blunt";
        const int index3 = index2 + 1;
        span[index3] = "vfx/vfx_attack_slash";
        const int index4 = index3 + 1;
        span[index4] = "vfx/vfx_bloody_impact";
        const int index5 = index4 + 1;
        span[index5] = "vfx/vfx_rock_shatter";
        return list;
    }

    public override CardPoolModel CardPool => ModelDb.CardPool<EmeraldMonkCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<EmeraldMonkRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<EmeraldMonkPotionPool>();

    /*  PlaceholderCharacterModel will utilize placeholder basegame assets for most of your character assets until you
        override all the other methods that define those assets.
        These are just some of the simplest assets, given some placeholders to differentiate your character with.
        You don't have to, but you're suggested to rename these images. */
    public override Control CustomIcon
    {
        get
        {
            var icon = NodeFactory<Control>.CreateFromResource(CustomIconTexturePath);
            icon.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
            return icon;
        }
    }

    public override string CustomCharacterSelectBg => "res://STS2_Mulundus/scenes/screens/char_select/lloyd_select.tscn";
    public override string CustomIconTexturePath => "character_icon_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectIconPath => "res://STS2_Mulundus/images/charui/Lloyd_char_select_button.png";
    public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "map_marker_char_name.png".CharacterUiPath();
    
    public override Color EnergyLabelOutlineColor => new("801212FF");

    public override Color DialogueColor => new("590700");

    public override VfxColor SpeechBubbleColor => VfxColor.Blue;

    public override Color MapDrawingColor => new("CB282B");

    public override Color RemoteTargetingLineColor => new("E15847FF");

    public override Color RemoteTargetingLineOutline => new("801212FF");
}