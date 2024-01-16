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
    public class VictideHelmet : ModItem
{
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Victide Helmet");
        Item.width = 18;
        Item.height = 18;
        ////Tooltip.SetDefault("5% increase to all damage");
        Item.value = 40000;
        Item.rare = 2;
        Item.defense = 2; //9
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("VictideBreastplate").Type && legs.type == Mod.Find<ModItem>("VictideLeggings").Type;
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus =("Increased life regen and damage while submerged in liquid");
        player.ignoreWater = true;
        if(player.wet == true || player.honeyWet == true || player.lavaWet == true)
    	{
    		player.GetDamage(DamageClass.Melee) *= 1.15f;
        	player.GetDamage(DamageClass.Throwing) *= 1.15f;
        	player.GetDamage(DamageClass.Ranged) *= 1.15f;
        	player.GetDamage(DamageClass.Magic) *= 1.15f;
        	player.GetDamage(DamageClass.Summon) *= 1.15f;
        	player.lifeRegen += 3;
        }
    }
    
    public override void UpdateEquip(Player player)
    {
    	player.GetDamage(DamageClass.Melee) *= 1.05f;
        player.GetDamage(DamageClass.Throwing) *= 1.05f;
        player.GetDamage(DamageClass.Ranged) *= 1.05f;
        player.GetDamage(DamageClass.Magic) *= 1.05f;
        player.GetDamage(DamageClass.Summon) *= 1.05f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "VictideBar", 3);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}