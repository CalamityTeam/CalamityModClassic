using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
	[AutoloadEquip(EquipType.Wings)]
public class InfinityBoots : ModItem
{

        public override void SetStaticDefaults()
        {
            Terraria.ID.ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new Terraria.DataStructures.WingStats(180, 15, 3);
        }

        public override void SetDefaults()
	{
		//Tooltip.SetDefault("Seraph Tracers");
		////Tooltip.SetDefault("Counts as wings\nExcellent acceleration: 3\nExcellent flight time: 180\nLudicrous speed!\nGreater mobility on ice\nWater and lava walking\nTemporary immunity to lava\nIncreased wing flight time");
		Item.width = 36;
		Item.height = 32;
		Item.value = 5000000;
		Item.rare = 10;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
		player.accRunSpeed = 18f;
		player.rocketBoots = 3;
        player.moveSpeed = 1.6f;
        player.iceSkate = true;
		player.waterWalk = true;
		player.fireWalk = true;
		player.lavaMax += 920;
		modPlayer.IBoots = true;
		if (hideVisual)
		{
			modPlayer.IBoots = false;
            }
            if (player.controlJump && player.wingTime > 0f && player.jump == 0 && player.velocity.Y != 0f && !hideVisual)
                Dust.NewDust(player.position, player.width, player.height, 107, 0, 0, 0, Color.Green);
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
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddRecipeGroup("WingsGroup");
		recipe.AddIngredient(null, "AngelTreads");
		recipe.AddIngredient(null, "CoreofCalamity", 3);
		recipe.AddIngredient(null, "BarofLife", 5);
		recipe.AddIngredient(null, "MeldiateBar", 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}