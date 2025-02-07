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
        public override void SetStaticDefaults() => ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(20, 8f, 1f);

        public override void SetDefaults()
    {
        Item.width = 22;
        Item.height = 20;
        Item.value = 50000;
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;
    }

    public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
    {
        ascentWhenFalling = 0.85f;
        ascentWhenRising = 0.15f;
        maxCanAscendMultiplier = 1f;
        maxAscentMultiplier = 3f;
        constantAscend = 0.135f;
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