using BaseLib.Utils;
using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using STS2_Mulundus.STS2_MulundusCode.Cards.Status;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Uncommon;

[Pool(typeof(HeartwoodRangerCardPool))]
public class NecroticWave : HeartWoodRangerCard
{
    
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/necrotic_wave.png";
    public NecroticWave() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies)
    {
        WithTips(_ => [HoverTipFactory.FromCard<Necrosis>()]);
        WithDamage(10);
        //WithVar("Hits", 1);
        WithCalculatedVar("CalculatedHits", 0, (card, _) =>
        { 
            var ePile = CardPile.Get(PileType.Exhaust, card.Owner); 
            if (ePile is null) return 0; 
            var hits = ePile.Cards.Count / 10; 
            return hits;
        });
      //  var calc = new CalculatedVar("CalculatedHits").WithMultiplier((card, creature) =>
       // {
       //     var ePile = CardPile.Get(PileType.Exhaust, card.Owner);
       //     if (ePile is null) return 0;
       //     var hits = ePile.Cards.Count / 10;
        //    return hits;
        //});
        //DynamicVars.AddItem(new KeyValuePair<string, DynamicVar>("CalculatedHits", calc));
        //CanonicalVars.AddItem(new CalculationBaseVar(0));
        //CanonicalVars.AddItem(new CalculationExtraVar(1));
       // CanonicalVars.AddItem(new CalculatedVar("CalculatedHits").WithMultiplier((card, creature) =>
        //{ 
       //     var ePile = CardPile.Get(PileType.Exhaust, card.Owner);
       //     if (ePile is null) return 0;
       //     var hits = ePile.Cards.Count / 10;
       //     return hits;
        //}));
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var hits = DynamicVars["CalculatedHits"].IntValue;
        await CommonActions.CardAttack(this, play, hits).Execute(choiceContext);
        
        if (CombatState is null) return;
        var statuses = Necrosis.Create(Owner, hits, CombatState).ToList();

        List<CardPileAddResult> added = [];
        foreach (var status in statuses)
        {
            var addResult = await CardPileCmd.AddGeneratedCardToCombat(status, PileType.Discard, true);
            added.Add(addResult);
        }
        
        CardCmd.PreviewCardPileAdd(added);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}