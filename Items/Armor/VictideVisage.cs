﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items.Armor;

namespace CalamityModClassic1Point2.Items.Armor {
[AutoloadEquip(EquipType.Head)]
public class VictideVisage : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Victide Visage");
        //Tooltip.SetDefault("5% increased ranged damage");
    }

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 40000;
        Item.rare = ItemRarityID.Green;
        Item.defense = 3; //10
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("VictideBreastplate").Type && legs.type == Mod.Find<ModItem>("VictideLeggings").Type;
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Increased life regen and ranged damage while submerged in liquid";
        player.ignoreWater = true;
        if(player.wet == true || player.honeyWet == true || player.lavaWet == true)
    	{
        	player.GetDamage(DamageClass.Ranged) += 0.15f;
        	player.lifeRegen += 3;
        }
    }
    
    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Ranged) += 0.05f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "VictideBar", 3);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}