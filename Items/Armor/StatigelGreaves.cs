﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items.Armor;

namespace CalamityModClassic1Point2.Items.Armor {
[AutoloadEquip(EquipType.Legs)]
public class StatigelGreaves : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Statigel Greaves");
        //Tooltip.SetDefault("5% increased damage and movement speed");
    }

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 122500;
        Item.rare = ItemRarityID.Pink;
        Item.defense = 8;
    }

    public override void UpdateEquip(Player player)
    {
		player.GetDamage(DamageClass.Melee) += 0.05f;
		player.GetDamage(DamageClass.Magic) += 0.05f;
		player.GetDamage(DamageClass.Ranged) += 0.05f;
		player.GetDamage(DamageClass.Throwing) += 0.05f;
		player.GetDamage(DamageClass.Summon) += 0.05f;
    	player.moveSpeed += 0.05f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "PurifiedGel", 6);
        recipe.AddIngredient(ItemID.HellstoneBar, 11);
		recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}