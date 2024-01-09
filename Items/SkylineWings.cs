using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items {
    [AutoloadEquip(EquipType.Wings)]
public class SkylineWings : ModItem
{
        public override void SetStaticDefaults()
        {
            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(70, 9f, 1.25f);
        }

    public override void SetDefaults()
    {
        //DisplayName.SetDefault("Skyline Wings");
        Item.width = 22;
        Item.height = 20;
        Item.value = 50000;
        Item.rare = 3;
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

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.controlJump && player.wingTime > 0f && player.jump == 0 && player.velocity.Y != 0f && !hideVisual)
            {
    		Dust.NewDust(player.position, player.width, player.height, 59, 0, 0, 0, Color.Red);
            }
            player.noFallDmg = true;
        }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "AerialiteBar", 5);
        recipe.AddIngredient(ItemID.Feather, 15);
        recipe.AddIngredient(ItemID.FallenStar, 5);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}