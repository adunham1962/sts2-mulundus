using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Uncommon;

[Pool(typeof(HeartwoodRangerCardPool))]
public class DreadArmament : HeartWoodRangerCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/dread_armament.png";
    private decimal _extraDamage;
    private decimal _extraBlock;
    
    public DreadArmament() : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithDamage(7);
        WithBlock(6);
        WithVar("ExhaustAmount", 1);
    }
    

    private decimal ExtraDamage
    {
        get => _extraDamage;
        set
        {
            AssertMutable();
            _extraDamage = value;
        }
    }
    
    private decimal ExtraBlock
    {
        get => _extraBlock;
        set
        {
            AssertMutable();
            _extraBlock = value;
        }
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {

        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).WithHitCount(1).FromCard(this).Targeting(play.Target).WithHitFx("vfx/vfx_thrash").Execute(choiceContext);
        await CommonActions.CardBlock(this, play);

        for (var i = 0; i < DynamicVars["ExhaustAmount"].BaseValue; i++)
        {
            var pile = PileType.Draw.GetPile(Owner);
            var cardModel = Owner.RunState.Rng.CombatCardSelection.NextItem(pile.Cards);
            if (cardModel is null) return;
            var damage1 = 0M;
            if (cardModel.DynamicVars.ContainsKey("CalculatedDamage"))
                damage1 = cardModel.DynamicVars.CalculatedDamage.Calculate( null);
            else if (cardModel.DynamicVars.ContainsKey("Damage"))
                damage1 = cardModel.DynamicVars.Damage.BaseValue;
            else if (cardModel.DynamicVars.ContainsKey("OstyDamage"))
                damage1 = cardModel.DynamicVars.OstyDamage.BaseValue;
            else
                Log.Warn($"{Id.Entry} exhausted attack card {cardModel.Id.Entry} that did not have an appropriate damage var!");
            var num = Hook.ModifyDamage(Owner.RunState, Owner.Creature.CombatState, null, Owner.Creature, damage1, ValueProp.Move, cardModel, ModifyDamageHookType.All, CardPreviewMode.None, out IEnumerable<AbstractModel> _);
            var damage2 = DynamicVars.Damage;
            damage2.BaseValue += num;
            ExtraDamage += num;

            var block1 = 0M;
            if (cardModel.DynamicVars.ContainsKey("CalculatedBlock"))
                block1 = cardModel.DynamicVars.CalculatedBlock.Calculate(null);
            else if (cardModel.DynamicVars.ContainsKey("Block"))
                block1 = cardModel.DynamicVars.Block.BaseValue;
            else
                Log.Warn($"{Id.Entry} exhausted attack card {cardModel.Id.Entry} that did not have an appropriate block var!");
            
            if (CombatState is null) return;
            var numBlock = Hook.ModifyBlock(CombatState, Owner.Creature, block1, ValueProp.Move, cardModel, play, out IEnumerable<AbstractModel> _);
            var block2 = DynamicVars.Block;
            block2.BaseValue += numBlock;
            ExtraBlock += numBlock;
        
            await CardCmd.Exhaust(choiceContext, cardModel);
        }

    }

    protected override void OnUpgrade()
    {
        DynamicVars["ExhaustAmount"].UpgradeValueBy(1);
    }
}