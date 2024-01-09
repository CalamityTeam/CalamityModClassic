using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items.Armor;

namespace CalamityModClassic1Point2.Items.Armor {
[AutoloadEquip(EquipType.Body)]
public class XerocPlateMail : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 425000;
        Item.rare = ItemRarityID.Cyan;
        Item.defense = 27;
    }

    public override void UpdateEquip(Player player)
    {
    	player.statLifeMax2 += 150;
        player.statManaMax2 += 80;
        player.moveSpeed += 0.12f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "MeldiateBar", 15);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}}