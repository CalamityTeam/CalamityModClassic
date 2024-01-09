using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class XerocMask : ModItem
{
    public override void SetDefaults()
    {
        //DisplayName.SetDefault("Xeroc Mask");
        Item.width = 18;
        Item.height = 18;
        //AddTooltip("15% increased melee, ranged, and magic damage, and 15% increased melee, ranged, and magic critical strike chance; Immune to lava, cursed, fire, cursed inferno, and chilled");
        //AddTooltip2("Wrath of the cosmos");
        Item.value = 500000;
        Item.rare = 9;
        Item.defense = 28;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("XerocPlateMail").Type && legs.type == Mod.Find<ModItem>("XerocCuisses").Type;
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = ("Imbued with wrath and rage as health drops; defense up and damage down when health is critical");
        if(player.statLife <= (player.statLifeMax2 * 0.85f))
        {
        	player.GetCritChance(DamageClass.Magic) += 10;
        	player.GetCritChance(DamageClass.Melee) += 10;
        	player.GetCritChance(DamageClass.Ranged) += 10;
        	if(player.statLife <= (player.statLifeMax2 * 0.75f))
        	{
        		player.manaCost *= 0.85f;
        		player.GetDamage(DamageClass.Magic) *= 1.15f;
        		player.GetDamage(DamageClass.Ranged) *= 1.15f;
        		player.GetDamage(DamageClass.Melee) *= 1.15f;
        		if(player.statLife <= (player.statLifeMax2 * 0.5f))
        		{
        			player.AddBuff(BuffID.Wrath, 2);
        			player.AddBuff(BuffID.Rage, 2);
        			if(player.statLife <= (player.statLifeMax2 * 0.4f))
        			{
        				player.endurance += 0.25f;
        				player.manaCost *= 2f;
        				if(player.statLife <= (player.statLifeMax2 * 0.25f))
        				{
        					player.GetDamage(DamageClass.Melee) *= 0.75f;
        					player.GetDamage(DamageClass.Ranged) *= 0.75f;
        					player.GetDamage(DamageClass.Magic) *= 0.75f;
        					player.GetCritChance(DamageClass.Magic) -= 20;
        					player.GetCritChance(DamageClass.Melee) -= 20;
        					player.GetCritChance(DamageClass.Ranged) -= 20;
        					player.endurance += 0.45f;
        				}
        			}
        		}
        	}
        }
    }
    
    public override void UpdateEquip(Player player)
    {
    	player.GetDamage(DamageClass.Melee) *= 1.15f;
        player.GetCritChance(DamageClass.Melee) += 15;
        player.GetDamage(DamageClass.Ranged) *= 1.15f;
        player.GetCritChance(DamageClass.Ranged) += 15;
        player.GetDamage(DamageClass.Magic) *= 1.15f;
        player.GetCritChance(DamageClass.Magic) += 15;
    	player.lavaImmune = true;
    	player.buffImmune[BuffID.OnFire] = true;
    	player.buffImmune[BuffID.CursedInferno] = true;
    	player.buffImmune[BuffID.Cursed] = true;
    	player.buffImmune[BuffID.Chilled] = true;
    }
    
    public override void ArmorSetShadows(Player player)
    {
    	player.armorEffectDrawShadow = true;
    	player.armorEffectDrawOutlines = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "MeldiateBar", 9);
        recipe.AddTile(null, "ParticleAccelerator");
        recipe.Register();
    }
}}