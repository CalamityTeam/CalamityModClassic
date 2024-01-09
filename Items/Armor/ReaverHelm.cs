using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items.Armor;

namespace CalamityModClassic1Point0.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ReaverHelm : ModItem
{

    public override void SetDefaults()
    {
        //DisplayName.SetDefault("Reaver Helm");
        Item.width = 18;
        Item.height = 18;
        //AddTooltip("20% increased melee damage, 10% increased melee speed, 15% increased melee critical strike chance, and 8% increased movement speed");
        //AddTooltip2("Immune to lava, cursed inferno, and can move freely through liquids");
        Item.value = 350000;
        Item.rare = 6;
        Item.defense = 24;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("ReaverScaleMail").Type && legs.type == Mod.Find<ModItem>("ReaverCuisses").Type;
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = ("Grants increased melee damage, melee crit, and melee speed; however, take more damage as health gets lower");
        player.onHitDodge = true;
        if(player.statLife <= (player.statLifeMax2 * 0.75f))
        {
        	player.GetDamage(DamageClass.Melee) *= 1.25f;
        	player.GetCritChance(DamageClass.Melee) += 15;
        	player.endurance -= 0.05f;
        	if(player.statLife <= (player.statLifeMax2 * 0.5f))
        	{
        		player.moveSpeed += 0.12f;
        		player.AddBuff(BuffID.Thorns, 2);
        		player.endurance -= 0.05f;
        		if(player.statLife <= (player.statLifeMax2 * 0.25f))
        		{
        			player.GetAttackSpeed(DamageClass.Melee) *= 1.25f;
        			player.endurance -= 0.05f;
        			player.GetDamage(DamageClass.Melee) += 0.25f;
        		}
        	}
        }
    }
    
    public override void UpdateEquip(Player player)
    {
    	player.lavaImmune = true;
    	player.ignoreWater = true;
    	player.buffImmune[BuffID.CursedInferno] = true;
    	player.GetDamage(DamageClass.Melee) *= 1.20f;
        player.GetCritChance(DamageClass.Melee) += 15;
        player.GetAttackSpeed(DamageClass.Melee) *= 1.10f;
        player.moveSpeed += 0.08f;
    }
    
    public override void ArmorSetShadows(Player player)
    {
    	player.armorEffectDrawOutlines = true;
    	player.armorEffectDrawShadow = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "DraedonBar", 8);
        recipe.AddTile(null, "ParticleAccelerator");
        recipe.Register();
    }
}}