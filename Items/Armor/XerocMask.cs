using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class XerocMask : ModItem
{

    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Xeroc Mask");
        Item.width = 18;
        Item.height = 18;
        ////Tooltip.SetDefault("10% increased damage and critical strike chance\nImmune to lava, cursed, fire, cursed inferno, and chilled\nWrath of the cosmos");
        Item.value = 500000;
        Item.rare = 9;
        Item.defense = 23; //62
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("XerocPlateMail").Type && legs.type == Mod.Find<ModItem>("XerocCuisses").Type;
    }
    
    public override void ArmorSetShadows(Player player)
    {
    	player.armorEffectDrawShadow = true;
    	player.armorEffectDrawOutlines = true;
    }

    public override void UpdateArmorSet(Player player)
    {
    	CalamityPlayer.xerocBlast = true;
    	CalamityPlayer.xerocHurt = true;
    	CalamityPlayer.xerocHeal = true;
    	CalamityPlayer.xerocSpike = true;
    	CalamityPlayer.xerocTear = true;
    	CalamityPlayer.xerocSummon = true;
    	if (player.immune)
		{
        	player.AddBuff(Mod.Find<ModBuff>("XerocRage").Type, 240);
        	player.AddBuff(Mod.Find<ModBuff>("XerocWrath").Type, 240);
        }
        player.setBonus =("Imbued with wrath and rage as health drops\nDefense up and damage down when health is critical\nAll projectile types have special effects on enemy hits\nImbued with cosmic wrath and rage when you are damaged");
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
        				player.endurance += 0.15f;
        				player.manaCost *= 2f;
        				if(player.statLife <= (player.statLifeMax2 * 0.25f))
        				{
        					player.GetDamage(DamageClass.Melee) *= 0.75f;
        					player.GetDamage(DamageClass.Ranged) *= 0.75f;
        					player.GetDamage(DamageClass.Magic) *= 0.75f;
        					player.GetCritChance(DamageClass.Magic) -= 20;
        					player.GetCritChance(DamageClass.Melee) -= 20;
        					player.GetCritChance(DamageClass.Ranged) -= 20;
        					player.endurance += 0.2f;
        				}
        			}
        		}
        	}
        }
    }
    
    public override void UpdateEquip(Player player)
    {
    	player.GetDamage(DamageClass.Melee) *= 1.1f;
        player.GetCritChance(DamageClass.Melee) += 10;
        player.GetDamage(DamageClass.Ranged) *= 1.1f;
        player.GetCritChance(DamageClass.Ranged) += 10;
        player.GetDamage(DamageClass.Magic) *= 1.1f;
        player.GetCritChance(DamageClass.Magic) += 10;
        player.GetDamage(DamageClass.Throwing) *= 1.1f;
        player.GetCritChance(DamageClass.Throwing) += 10;
        player.GetDamage(DamageClass.Summon) *= 1.1f;
    	player.lavaImmune = true;
    	player.buffImmune[BuffID.OnFire] = true;
    	player.buffImmune[BuffID.CursedInferno] = true;
    	player.buffImmune[BuffID.Cursed] = true;
    	player.buffImmune[BuffID.Chilled] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "MeldiateBar", 9);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}