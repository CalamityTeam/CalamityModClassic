using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class BiomeBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Biome Blade");
			// Tooltip.SetDefault("Fires different projectiles based on what biome you're in");
		}

		public override void SetDefaults()
		{
			Item.width = 36;
			Item.damage = 42;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 24;
			Item.useTime = 24;
			Item.useTurn = true;
			Item.useStyle = 1;
			Item.knockBack = 6;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 36;
            Item.value = Item.buyPrice(0, 12, 0, 0);
            Item.rare = 4;
			Item.shoot = Mod.Find<ModProjectile>("BiomeOrb").Type;
			Item.shootSpeed = 12f;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.WoodenSword);
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddIngredient(ItemID.SandBlock, 10);
			recipe.AddIngredient(ItemID.IceBlock, 10);
			recipe.AddIngredient(ItemID.EbonstoneBlock, 10);
			recipe.AddIngredient(ItemID.GlowingMushroom, 10);
			recipe.AddIngredient(ItemID.Marble, 10);
			recipe.AddIngredient(ItemID.Granite, 10);
			recipe.AddIngredient(ItemID.Hellstone, 10);
			recipe.AddIngredient(ItemID.Coral, 5);
			recipe.AddIngredient(ItemID.PearlstoneBlock, 10);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	        recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.WoodenSword);
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddIngredient(ItemID.SandBlock, 10);
			recipe.AddIngredient(ItemID.IceBlock, 10);
			recipe.AddIngredient(ItemID.CrimstoneBlock, 10);
			recipe.AddIngredient(ItemID.GlowingMushroom, 10);
			recipe.AddIngredient(ItemID.Marble, 10);
			recipe.AddIngredient(ItemID.Granite, 10);
			recipe.AddIngredient(ItemID.Hellstone, 10);
			recipe.AddIngredient(ItemID.Coral, 5);
			recipe.AddIngredient(ItemID.PearlstoneBlock, 10);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.Next(5) == 0)
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 0);
	        }
	    }
	}
}
