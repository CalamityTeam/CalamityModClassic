using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
[AutoloadEquip(EquipType.Wings)]
public class MOAB : ModItem
{
	
	public override void SetDefaults()
	{
		Item.width = 28;
		Item.height = 32;
		Item.value = 5000000;
		Item.rare = ItemRarityID.Yellow;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if (player.controlJump && player.wingTime > 0f && player.jump == 0 && player.velocity.Y != 0f)
		{
			player.rocketDelay2--;
			if (player.rocketDelay2 <= 0) 
			{
				SoundEngine.PlaySound(SoundID.Item13, player.position);
				player.rocketDelay2 = 60;
			}
			int num66 = 2;
			if (player.controlUp) 
			{
				num66 = 4;
			}
			for (int num67 = 0; num67 < num66; num67++) 
			{
				int type = 6;
				if (player.head == 41) 
				{
					int arg_58FD_0 = player.body;
				}
				float scale = 1.75f;
				int alpha = 100;
				float x = player.position.X + (float)(player.width / 2) + 16f;
				if (player.direction > 0) 
				{
					x = player.position.X + (float)(player.width / 2) - 26f;
				}
				float num68 = player.position.Y + (float)player.height - 18f;
				if (num67 == 1 || num67 == 3) 
				{
					x = player.position.X + (float)(player.width / 2) + 8f;
					if (player.direction > 0) 
					{
						x = player.position.X + (float)(player.width / 2) - 20f;
					}
					num68 += 6f;
				}
				if (num67 > 1) 
				{
					num68 += player.velocity.Y;
				}
				int num69 = Dust.NewDust(new Vector2(x, num68), 8, 8, type, 0f, 0f, alpha, default(Color), scale);
				Dust expr_5A11_cp_0_cp_0 = Main.dust[num69];
				expr_5A11_cp_0_cp_0.velocity.X = expr_5A11_cp_0_cp_0.velocity.X * 0.1f;
				Main.dust[num69].velocity.Y = Main.dust[num69].velocity.Y * 1f + 2f * player.gravDir - player.velocity.Y * 0.3f;
				Main.dust[num69].noGravity = true;
				Main.dust[num69].shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
				if (num66 == 4) 
				{
					Dust expr_5AA6_cp_0_cp_0 = Main.dust[num69];
					expr_5AA6_cp_0_cp_0.velocity.Y = expr_5AA6_cp_0_cp_0.velocity.Y + 6f;
				}
			}
		}
		player.GetJumpState(ExtraJump.CloudInABottle).Enable()/* tModPorter Suggestion: Call Enable() if setting this to true, otherwise call Disable(). */;
		player.GetJumpState(ExtraJump.SandstormInABottle).Enable()/* tModPorter Suggestion: Call Enable() if setting this to true, otherwise call Disable(). */;
		player.GetJumpState(ExtraJump.BlizzardInABottle).Enable()/* tModPorter Suggestion: Call Enable() if setting this to true, otherwise call Disable(). */;
		player.jumpBoost = true;
		player.autoJump = true;
		player.noFallDmg = true;
		player.jumpSpeedBoost += 4f;
		player.wingTimeMax = 80;
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
        speed = 10.5f;
        acceleration *= 2f;
    }
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.FrogLeg);
		recipe.AddIngredient(ItemID.BundleofBalloons);
		recipe.AddIngredient(ItemID.LuckyHorseshoe);
		recipe.AddIngredient(ItemID.Jetpack);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}