﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class AerospecLeggings : ModItem
{
    
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Aerospec Leggings");
        Item.width = 18;
        Item.height = 18;
        ////Tooltip.SetDefault("12% increased movement speed");
        Item.value = 57500;
        Item.rare = 3;
        Item.defense = 5; //15
    }

    public override void UpdateEquip(Player player)
    {
        player.moveSpeed += 0.12f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "AerialiteBar", 7);
        recipe.AddIngredient(ItemID.Cloud, 6);
        recipe.AddIngredient(ItemID.RainCloud, 3);
        recipe.AddIngredient(ItemID.Feather, 2);
        recipe.AddTile(TileID.SkyMill);
        recipe.Register();
    }
}}