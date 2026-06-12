using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Rooms;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Relics;

namespace STS2_Mulundus.STS2_MulundusCode.Relics;

[Pool(typeof(EmeraldMonkRelicPool))]
public class EmeraldSerpentSash() : STS2_MulundusRelic
{
    public override RelicRarity Rarity => RelicRarity.Starter;
    
    public override string PackedIconPath => "res://STS2_Mulundus/images/relics/emerald_serpent_sash.png";

    private bool _powerPlayed = false;
    private bool _attackPlayed = false;
    private bool _skillPlayed = false;
    private bool _cursePlayed = false;
    private bool _statusPlayed = false;
    private bool _questPlayed = false;

    public override Task AfterCombatEnd(CombatRoom room)
    {
        _powerPlayed = false;
        _attackPlayed = false;
        _skillPlayed = false;
        _cursePlayed = false;
        _statusPlayed = false;
        _questPlayed = false;
        return Task.CompletedTask;
    }

    public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        var card = cardPlay.Card;
        var shouldDraw = false;
        switch (card.Type)
        {
            case CardType.Attack when !_attackPlayed:
                shouldDraw = true;
                _attackPlayed = true;
                break;
            case CardType.Curse when !_cursePlayed:
                shouldDraw = true;
                _cursePlayed = true;
                break;
            case CardType.Power when !_powerPlayed:
                shouldDraw = true;
                _powerPlayed = true;
                break;
            case CardType.Skill when !_skillPlayed:
                shouldDraw = true;
                _skillPlayed = true;
                break;
            case CardType.Status when !_statusPlayed:
                shouldDraw = true;
                _statusPlayed = true;
                break;
            case CardType.Quest when !_questPlayed:
                shouldDraw = true;
                _questPlayed = true;
                break;
            case CardType.None:
            default:
                break;
        }

        if (shouldDraw)
        {
            CardPileCmd.Draw(context, Owner);
        }
        
        return Task.CompletedTask;
    }
}