using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
[AutoloadEquip(EquipType.Wings)]
public class InfinityBoots : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Seraph Tracers");
		//Tooltip.SetDefault("Counts as wings\nExcellent acceleration: 3\nExcellent flight time: 180\nLudicrous speed!\nGreater mobility on ice\nWater and lava walking\nTemporary immunity to lava");
	}
	
	public override void SetDefaults()
	{
		Item.width = 36;
		Item.height = 32;
		Item.value = 5000000;
		Item.rare = ItemRarityID.Red;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if (player.controlJump && player.wingTime > 0f && player.jump == 0 && player.velocity.Y != 0f)
		{
			int num59 = 4;
			if (player.direction == 1)
			{
				num59 = -40;
			}
			int num60 = Dust.NewDust(new Vector2(player.position.X + (float)(player.width / 2) + (float)num59, player.position.Y + (float)(player.height / 2) - 15f), 30, 30, DustID.TerraBlade, 0f, 0f, 100, default(Color), 2.4f);
			Main.dust[num60].noGravity = true;
			Main.dust[num60].velocity *= 0.3f;
			if (Main.rand.NextBool(10))
			{
				Main.dust[num60].fadeIn = 2f;
			}
			Main.dust[num60].shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
		}
		CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
		player.accRunSpeed = 18f;
		player.rocketBoots = 3;
        player.moveSpeed = 1.6f;
		player.iceSkate = true;
		player.waterWalk = true;
		player.fireWalk = true;
		player.lavaMax += 920;
		player.wingTimeMax = 180;
		player.runAcceleration *= 1.25f;
		player.maxRunSpeed *= 1.25f;
		modPlayer.IBoots = true;
		if (hideVisual)
		{
			modPlayer.IBoots = false;
		}
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
        speed = 15f;
        acceleration *= 3.2f;
    }
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddRecipeGroup("WingsGroup");
		recipe.AddIngredient(null, "AngelTreads");
		recipe.AddIngredient(null, "CoreofCalamity", 3);
		recipe.AddIngredient(null, "BarofLife", 5);
		recipe.AddIngredient(null, "MeldiateBar", 5);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
	}
}}