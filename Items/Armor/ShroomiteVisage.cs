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
    [AutoloadEquip(EquipType.Head)]
    public class ShroomiteVisage : ModItem
{

    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Shroomite Visage");
        Item.width = 18;
        Item.height = 18;
        ////Tooltip.SetDefault("25% increased ranged damage for flamethrowers");
        Item.value = 375000;
        Item.rare = 8;
        Item.defense = 11; //62
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ItemID.ShroomiteBreastplate && legs.type == ItemID.ShroomiteLeggings;
    }

    public override void UpdateArmorSet(Player player)
    {
    	CalamityPlayer1Point1 modPlayer = player.GetModPlayer<CalamityPlayer1Point1>();
    	player.shroomiteStealth = true;
        player.setBonus =("Ranged stealth while standing still");
    }
    
    public override void UpdateEquip(Player player)
    {
    	CalamityPlayer1Point1 modPlayer = player.GetModPlayer<CalamityPlayer1Point1>();
    	modPlayer.flamethrowerBoost = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.ShroomiteBar, 12);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}