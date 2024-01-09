using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class Mariana : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Mariana");
			//Tooltip.SetDefault("Tropical and deadly");
		}

		public override void SetDefaults()
		{
			Item.damage = 55;
			Item.width = 46;
			Item.height = 56;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 24;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 24;
			Item.useTurn = true;
			Item.knockBack = 6.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.value = 500000;
			Item.rare = ItemRarityID.Lime;
			Item.shoot = Mod.Find<ModProjectile>("MarianaProjectile").Type;
			Item.shootSpeed = 16f;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
			float SpeedA = velocity.X;
	   		float SpeedB = velocity.Y;
	        int num6 = Main.rand.Next(4, 6);
	        for (int index = 0; index < num6; ++index)
	        {
	      	 	float num7 = velocity.X;
	            float num8 = velocity.Y;
	            float SpeedX = velocity.X + (float) Main.rand.Next(-40, 41) * 0.05f;
	            float SpeedY = velocity.Y + (float) Main.rand.Next(-40, 41) * 0.05f;
	            Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, (int)((double)damage * 0.5f), knockback, player.whoAmI, 0.0f, 0.0f);
	        }
	        return false;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.ChlorophyteClaymore, 1);
	        recipe.AddIngredient(ItemID.Coral, 5);
	        recipe.AddIngredient(ItemID.Starfish, 3);
	        recipe.AddIngredient(ItemID.Seashell, 3);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if(Main.rand.NextBool(3))
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.BlueTorch);
	        }
	    }
	}
}
