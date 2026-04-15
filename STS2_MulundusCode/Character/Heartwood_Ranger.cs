using BaseLib.Abstracts;
using BaseLib.Utils.NodeFactories;
using STS2_Mulundus.STS2_MulundusCode.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;

namespace STS2_Mulundus.STS2_MulundusCode.Character;

public class HeartwoodRanger: CustomCharacterModel
{
    public const string CharacterId = "STS2_Mulundus_Heartwood_Ranger";

    public static readonly Color Color = new("124212");

    public override Color NameColor => Color;
    public override CharacterGender Gender => CharacterGender.Neutral;
    public override int StartingHp => 80;
    
    public override IEnumerable<CardModel> StartingDeck =>
    [
        ModelDb.Card<StrikeIronclad>(),
        ModelDb.Card<StrikeIronclad>(),
        ModelDb.Card<StrikeIronclad>(),
        ModelDb.Card<StrikeIronclad>(),
        ModelDb.Card<StrikeIronclad>(),
        ModelDb.Card<DefendIronclad>(),
        ModelDb.Card<DefendIronclad>(),
        ModelDb.Card<DefendIronclad>(),
        ModelDb.Card<DefendIronclad>(),
        ModelDb.Card<DefendIronclad>()
    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<BurningBlood>()
    ];

    public override List<string> GetArchitectAttackVfx()
    {
        throw new NotImplementedException();
    }

    public override CardPoolModel CardPool => ModelDb.CardPool<HeartwoodRangerCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<HeartwoodRangerRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<Heartwood_Ranger_PotionPool>();

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

    public override string CustomIconTexturePath => "character_icon_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectIconPath => "char_select_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "map_marker_char_name.png".CharacterUiPath();
}