using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Accessories.Wings
{
	[AutoloadEquip(EquipType.Wings)]
	public class TarragonWings : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Tarragon Wings");
			/* Tooltip.SetDefault("Born of the jungle\n" +
				"Horizontal speed: 9.5\n" +
				"Acceleration multiplier: 2.5\n" +
				"Great vertical speed\n" +
				"Flight time: 210"); */
			ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(210, 9.5f, 2.5f);
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 20;
			Item.value = Item.buyPrice(0, 39, 99, 99);
			Item.accessory = true;
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine line2 in list)
			{
				if (line2.Mod == "Terraria" && line2.Name == "ItemName")
				{
					line2.OverrideColor = new Color(0, 255, 200);
				}
			}
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (player.controlJump && player.wingTime > 0f && !VanillaExtraJump.CloudInABottle.CanStart(player) && player.jump == 0 && player.velocity.Y != 0f && !hideVisual)
			{
				int num59 = 4;
				if (player.direction == 1)
				{
					num59 = -40;
				}
				int num60 = Dust.NewDust(new Vector2(player.position.X + (float)(player.width / 2) + (float)num59, player.position.Y + (float)(player.height / 2) - 15f), 30, 30, 75, 0f, 0f, 100, default(Color), 2.4f);
				Main.dust[num60].noGravity = true;
				Main.dust[num60].velocity *= 0.3f;
				if (Main.rand.Next(10) == 0)
				{
					Main.dust[num60].fadeIn = 2f;
				}
				Main.dust[num60].shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
			}
			player.wingTimeMax = 210;
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
			recipe.AddIngredient(null, "UeliaceBar", 5);
			recipe.AddIngredient(ItemID.SoulofFlight, 30);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}