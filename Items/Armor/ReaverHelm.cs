using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items.Armor;

namespace CalamityModClassic1Point1.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ReaverHelm : ModItem
{

    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Reaver Helm");
        Item.width = 18;
        Item.height = 18;
        ////Tooltip.SetDefault("15% increased melee damage, 8% increased melee speed, 9% increased melee critical strike chance\n8% increased movement speed\nImmune to lava, cursed inferno, and can move freely through liquids");
        Item.value = 350000;
        Item.rare = 6;
        Item.defense = 24; //51
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("ReaverScaleMail").Type && legs.type == Mod.Find<ModItem>("ReaverCuisses").Type;
    }
    
    public override void ArmorSetShadows(Player player)
    {
    	player.armorEffectDrawShadowSubtle = true;
    	player.armorEffectDrawOutlines = true;
    }

    public override void UpdateArmorSet(Player player)
    {
		player.thorns = 0.5f;
    	CalamityPlayer1Point1.reaverBlast = true;
        player.setBonus =("Grants increased melee damage, melee crit, and melee speed as health decreases\nYou take more damage as health decreases\nMelee projectiles explode on hit\nReaver thorns\nRage activates when you are damaged");
        if (player.immune)
		{
        	player.AddBuff(Mod.Find<ModBuff>("ReaverRage").Type, 180);
        }
        if(player.statLife <= (player.statLifeMax2 * 0.75f))
        {
        	player.GetDamage(DamageClass.Melee) += 0.2f;
        	player.GetCritChance(DamageClass.Melee) += 15;
        	player.endurance -= 0.05f;
        	if(player.statLife <= (player.statLifeMax2 * 0.5f))
        	{
        		player.moveSpeed += 0.12f;
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
    	player.GetDamage(DamageClass.Melee) *= 1.15f;
        player.GetCritChance(DamageClass.Melee) += 9;
        player.GetAttackSpeed(DamageClass.Melee) *= 1.08f;
        player.moveSpeed += 0.08f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "DraedonBar", 8);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}