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
	public class HellfireFlamberge : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Hellfire Flamberge");
		}

		public override void SetDefaults()
		{
			Item.width = 50;
			Item.damage = 102;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 20;
			Item.useTurn = true;
			Item.knockBack = 7.75f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 50;
			Item.value = 415000;
			Item.rare = ItemRarityID.Yellow;
			Item.shoot = Mod.Find<ModProjectile>("ChaosFlameSmall").Type;
			Item.shootSpeed = 20f;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	float SpeedA = velocity.X;
	   		float SpeedB = velocity.Y;
	        int num6 = Main.rand.Next(3, 5);
	        for (int index = 0; index < num6; ++index)
	        {
	      	 	float num7 = velocity.X;
	            float num8 = velocity.Y;
	            float SpeedX = velocity.X + (float) Main.rand.Next(-40, 41) * 0.05f;
	            float SpeedY = velocity.Y + (float) Main.rand.Next(-40, 41) * 0.05f;
	    		switch (Main.rand.Next(3))
				{
	    			case 0: type = Mod.Find<ModProjectile>("ChaosFlameSmall").Type; break;
	    			case 1: type = Mod.Find<ModProjectile>("ChaosFlameMedium").Type; break;
	    			case 2: type = Mod.Find<ModProjectile>("ChaosFlameLarge").Type; break;
	    			default: break;
				}
	            Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, (int)((double)damage * 0.75), knockback, player.whoAmI, 0.0f, 0.0f);
	    	}
	    	return false;
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "CruptixBar", 15);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.NextBool(3))
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.InfernoFork);
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
			target.AddBuff(BuffID.OnFire, 300);
		}
	}
}
