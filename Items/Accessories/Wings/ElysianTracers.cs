using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Accessories.Wings
{
	[AutoloadEquip(EquipType.Wings)]
	public class ElysianTracers : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Elysian Tracers");
			/* Tooltip.SetDefault("Counts as wings\n" +
				"Horizontal speed: 10.5\n" +
				"Acceleration multiplier: 2.75\n" +
				"Great vertical speed\n" +
				"Flight time: 160\n" +
				"Ludicrous speed!\n" +
				"Greater mobility on ice\n" +
				"Water and lava walking\n" +
				"Temporary immunity to lava"); */
			ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(160, 10.5f, 2.75f);
		}

		public override void SetDefaults()
		{
			Item.width = 36;
			Item.height = 32;
			Item.value = Item.buyPrice(0, 90, 0, 0);
			Item.accessory = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 14;
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
				int num60 = Dust.NewDust(new Vector2(player.position.X + (float)(player.width / 2) + (float)num59, player.position.Y + (float)(player.height / 2) - 15f), 30, 30, (Main.rand.Next(2) == 0 ? 206 : 173), 0f, 0f, 100, default(Color), 2.4f);
				Main.dust[num60].noGravity = true;
				Main.dust[num60].velocity *= 0.3f;
				if (Main.rand.Next(10) == 0)
				{
					Main.dust[num60].fadeIn = 2f;
				}
				Main.dust[num60].shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
			}
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			player.accRunSpeed = 10.5f;
			player.rocketBoots = 3;
			player.moveSpeed += 0.36f;
			player.iceSkate = true;
			player.waterWalk = true;
			player.fireWalk = true;
			player.lavaMax += 240;
			modPlayer.IBoots = !hideVisual;
			modPlayer.elysianFire = !hideVisual;
			modPlayer.eTracers = true;
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 0.95f; //0.85
			ascentWhenRising = 0.15f;
			maxCanAscendMultiplier = 1.1f; //1
			maxAscentMultiplier = 3.15f; //3
			constantAscend = 0.135f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "InfinityBoots");
			recipe.AddIngredient(null, "ElysianWings");
			recipe.AddIngredient(null, "CosmiliteBar", 5);
			recipe.AddIngredient(null, "Phantoplasm", 5);
			recipe.AddTile(null, "DraedonsForge");
			recipe.Register();
		}
	}
}