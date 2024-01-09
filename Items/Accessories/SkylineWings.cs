using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
[AutoloadEquip(EquipType.Wings)]
public class SkylineWings : ModItem
{
    public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Skyline Wings");
		//Tooltip.SetDefault("Low acceleration: 1\nLow flight time: 20");
	}

    public override void SetDefaults()
    {
        Item.width = 22;
        Item.height = 20;
        Item.value = 50000;
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.wingTimeMax = 30;
    }

    public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
    {
        ascentWhenFalling = 0.85f;
        ascentWhenRising = 0.15f;
        maxCanAscendMultiplier = 1f;
        maxAscentMultiplier = 3f;
        constantAscend = 0.135f;
    }

    public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
    {
        speed = 8f;
        acceleration *= 1f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "AerialiteBar", 5);
        recipe.AddIngredient(ItemID.Feather, 5);
        recipe.AddIngredient(ItemID.FallenStar, 5);
        recipe.AddIngredient(ItemID.Bone, 15);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}