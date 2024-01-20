using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Armor {
[AutoloadEquip(EquipType.Head)]
public class SilvaHelm : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Silva Helm");
        //Tooltip.SetDefault("18% increased damage and critical strike chance and +5 max minions");
    }

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 9000000;
        Item.defense = 34; //110
    }
    
    public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(108, 45, 199);
            }
        }
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("SilvaArmor").Type && legs.type == Mod.Find<ModItem>("SilvaLeggings").Type;
    }
    
    public override void ArmorSetShadows(Player player)
    {
    	player.armorEffectDrawShadow = true;
    }

    public override void UpdateArmorSet(Player player)
    {
    	CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
    	modPlayer.silvaSet = true;
    	for (int k = 0; k < player.buffImmune.Length; k++)
		{
    		if (Main.debuff[k])
    		{
				player.buffImmune[k] = true;
				player.buffImmune[BuffID.MonsterBanner] = false;
				player.buffImmune[BuffID.HeartLamp] = false;
				player.buffImmune[BuffID.Sunflower] = false;
				player.buffImmune[BuffID.PeaceCandle] = false;
				player.buffImmune[BuffID.Campfire] = false;
				player.buffImmune[BuffID.WaterCandle] = false;
				player.buffImmune[BuffID.Werewolf] = false;
				player.buffImmune[BuffID.ChaosState] = false;
				player.buffImmune[BuffID.PotionSickness] = false;
				player.buffImmune[BuffID.ManaSickness] = false;
				player.buffImmune[Mod.Find<ModBuff>("ScarfCooldown").Type] = false;
				player.buffImmune[Mod.Find<ModBuff>("VulnerabilityHex").Type] = false;
				player.buffImmune[Mod.Find<ModBuff>("AbyssalFlames").Type] = false;
				player.buffImmune[Mod.Find<ModBuff>("HeartAttack").Type] = false;
				player.buffImmune[Mod.Find<ModBuff>("Warped").Type] = false;
				player.buffImmune[Mod.Find<ModBuff>("ExtremeGrav").Type] = false;
    		}
		}
        player.setBonus = "You are immune to almost all debuffs\n" +
        	"Reduces all damage taken by 5%, this is calculated separately from damage reduction\n" +
        	"All projectiles spawn healing leaf orbs on enemy hits\n" +
        	"Max run speed and acceleration boosted by 30%";
    }
    
    public override void UpdateEquip(Player player)
    {
    	player.maxMinions += 5;
    	player.GetDamage(DamageClass.Melee) += 0.18f;
        player.GetCritChance(DamageClass.Melee) += 18;
        player.GetDamage(DamageClass.Ranged) += 0.18f;
        player.GetCritChance(DamageClass.Ranged) += 18;
        player.GetDamage(DamageClass.Magic) += 0.18f;
        player.GetCritChance(DamageClass.Magic) += 18;
        player.GetDamage(DamageClass.Throwing) += 0.18f;
        player.GetCritChance(DamageClass.Throwing) += 18;
        player.GetDamage(DamageClass.Summon) += 0.18f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "HellcasterFragment", 10);
        recipe.AddIngredient(null, "CosmiliteBar", 5);
        recipe.AddIngredient(null, "NightmareFuel", 14);
        recipe.AddIngredient(null, "EndothermicEnergy", 14);
        recipe.AddTile(null, "DraedonsForge");
        recipe.Register();
    }
}}