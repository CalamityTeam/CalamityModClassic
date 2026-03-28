using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items
{
	public class SkyfringePickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Skyfringe Pickaxe");
			// Tooltip.SetDefault("Able to mine Hellstone");
		}

		public override void SetDefaults()
		{
			Item.damage = 12;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 44;
			Item.height = 44;
			Item.useTime = 16;
			Item.useAnimation = 16;
			Item.useTurn = true;
			Item.pick = 95;
			Item.useStyle = 1;
			Item.knockBack = 2.5f;
			Item.value = Item.buyPrice(0, 4, 0, 0);
			Item.rare = 3;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "AerialiteBar", 7);
			recipe.AddIngredient(ItemID.SunplateBlock, 3);
			recipe.AddTile(TileID.SkyMill);
			recipe.Register();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(3) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 59);
			}
		}
	}
}