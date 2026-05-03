using System;
using System.Collections.Generic;
using BaseLib.Abstracts;
using BaseLib.Utils.NodeFactories;
using STS2_Mulundus.STS2_MulundusCode.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using STS2_Mulundus.STS2_MulundusCode.Cards;
using STS2_Mulundus.STS2_MulundusCode.Cards.Basic;
using STS2_Mulundus.STS2_MulundusCode.Cards.Common;

namespace STS2_Mulundus.STS2_MulundusCode.Character;

public class HeartwoodRanger: PlaceholderCharacterModel
{
    public const string CharacterId = "STS2_Mulundus_Heartwood_Ranger";

    public override string PlaceholderID => "necrobinder";
    
    public static readonly Color Color = StsColors.green;

    public override Color NameColor => Color;
    public override CharacterGender Gender => CharacterGender.Feminine;
    
    protected override CharacterModel? UnlocksAfterRunAs => null;
    
    public override int StartingGold => 99;
    public override int StartingHp => 70;
    
    public override IEnumerable<CardModel> StartingDeck =>
    [
        ModelDb.Card<HeartwoodRangerStrike>(),
        ModelDb.Card<HeartwoodRangerStrike>(),
        ModelDb.Card<HeartwoodRangerStrike>(),
        ModelDb.Card<HeartwoodRangerStrike>(),
        ModelDb.Card<HeartwoodRangerDefend>(),
        ModelDb.Card<HeartwoodRangerDefend>(),
        ModelDb.Card<HeartwoodRangerDefend>(),
        ModelDb.Card<HeartwoodRangerDefend>(),
        ModelDb.Card<DreadSlash>(),
        ModelDb.Card<InvokeYourOath>()
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

    public override string CustomCharacterSelectBg =>
        "res://STS2_Mulundus/scenes/screens/char_select/char_select_bg_sts2_mulundus-heartwood_ranger.tscn";
    public override string CustomIconTexturePath => "character_icon_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectIconPath => "char_select_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "map_marker_char_name.png".CharacterUiPath();
    
    public override Color EnergyLabelOutlineColor => new Color("801212FF");

    public override Color DialogueColor => new Color("590700");

    public override VfxColor SpeechBubbleColor => VfxColor.Green;

    public override Color MapDrawingColor => new Color("CB282B");

    public override Color RemoteTargetingLineColor => new Color("E15847FF");

    public override Color RemoteTargetingLineOutline => new Color("801212FF");
}