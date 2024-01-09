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
public class WulfrumLeggings : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Wulfrum Leggings");
        //Tooltip.SetDefault("Movement speed increased by 5%");
    }

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 17500;
        Item.rare = ItemRarityID.Blue;
        Item.defense = 1;
    }

    public override void UpdateEquip(Player player)
    {
    	player.moveSpeed += 0.05f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "WulfrumShard", 15);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}