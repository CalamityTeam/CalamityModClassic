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
    public class StatigelHelm : ModItem
{

    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Statigel Helm");
        Item.width = 18;
        Item.height = 18;
        ////Tooltip.SetDefault("10% increased throwing damage, 15% increased throwing velocity, and 5% increased throwing crit\nIncreased maximum minions and 8% increased minion damage");
        Item.value = 200000;
        Item.rare = 5;
        Item.defense = 7; //25
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("StatigelArmor").Type && legs.type == Mod.Find<ModItem>("StatigelGreaves").Type;
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus =("Grants minion damage, throwing crit, and speed boosts as health gets lower");
        if(player.statLife <= (player.statLifeMax2 * 0.75f))
        {
        	player.GetCritChance(DamageClass.Throwing) += 5;
        	player.GetDamage(DamageClass.Summon) += 0.05f;
        	if(player.statLife <= (player.statLifeMax2 * 0.5f))
        	{
        		player.GetDamage(DamageClass.Summon) += 0.10f;
        		if(player.statLife <= (player.statLifeMax2 * 0.25f))
        		{
        			player.GetCritChance(DamageClass.Throwing) += 10;
        			player.moveSpeed += 0.10f;
        			player.GetDamage(DamageClass.Summon) += 0.15f;
        		}
        	}
        }
    }
    
    public override void UpdateEquip(Player player)
    {
    	player.GetDamage(DamageClass.Throwing) += 0.10f;
        player.GetCritChance(DamageClass.Throwing) += 5;
        player.ThrownVelocity += 0.15f;
    	player.maxMinions++;
    	player.GetDamage(DamageClass.Summon) += 0.08f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "PurifiedGel", 5);
        recipe.AddIngredient(ItemID.HellstoneBar, 9);
		recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}