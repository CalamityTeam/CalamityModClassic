﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items.Armor;

namespace CalamityModClassic1Point1.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class AtaxiaSubligar : ModItem
{

    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Ataxia Subligar");
        Item.width = 18;
        Item.height = 18;
        ////Tooltip.SetDefault("7% increased magic critical strike chance\n12% increased movement speed");
        Item.value = 412500;
        Item.rare = 7;
        Item.defense = 13; //42
    }

    public override void UpdateEquip(Player player)
    {
    	player.GetCritChance(DamageClass.Magic) += 7;
    	player.moveSpeed += 0.12f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "CruptixBar", 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}