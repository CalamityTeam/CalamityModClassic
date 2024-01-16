using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
    [AutoloadEquip(EquipType.Wings)]
public class SkylineWings : ModItem
{

        public override void SetStaticDefaults()
        {
            Terraria.ID.ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new Terraria.DataStructures.WingStats(20, 8, 1);
        }

        public override void SetDefaults()
    {
        //Tooltip.SetDefault("Skyline Wings");
        Item.width = 22;
        ////Tooltip.SetDefault("Low acceleration: 1\nLow flight time: 20");
        Item.height = 20;
        Item.value = 50000;
        Item.rare = 3;
        Item.accessory = true;
    }

    public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
       ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
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
    		Dust.NewDust(player.position, player.width, player.height, 59, 0, 0, 0, Color.Blue);
    	//base.WingUpdate(player, player.controlJump && player.wingTime > 0f && player.jump == 0 && player.velocity.Y != 0f && !hideVisual);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "AerialiteBar", 5);
        recipe.AddIngredient(ItemID.Feather, 5);
        recipe.AddIngredient(ItemID.FallenStar, 5);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}