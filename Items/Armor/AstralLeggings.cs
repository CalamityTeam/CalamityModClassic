using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Armor {
[AutoloadEquip(EquipType.Legs)]
public class AstralLeggings : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 450000;
        Item.rare = ItemRarityID.Lime;
        Item.defense = 17;
    }

    public override void UpdateEquip(Player player)
    {
        player.moveSpeed += 0.1f;
        player.findTreasure = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "AstralBar", 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}