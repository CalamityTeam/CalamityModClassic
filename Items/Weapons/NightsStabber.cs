using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class NightsStabber : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Night's Stabber");
			// Tooltip.SetDefault("Don't underestimate the power of stabby knives\nEnemies release homing dark energy on death");
		}

		public override void SetDefaults()
		{
			Item.useStyle = 3;
			Item.useTurn = false;
			Item.useAnimation = 15;
			Item.useTime = 15;
			Item.width = 30;
			Item.height = 30;
			Item.damage = 52;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.knockBack = 6.5f;
			Item.UseSound = SoundID.Item1;
			Item.useTurn = true;
			Item.autoReuse = true;
            Item.value = Item.buyPrice(0, 4, 0, 0);
            Item.rare = 3;
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "AncientShiv");
			recipe.AddIngredient(null, "SporeKnife");
			recipe.AddIngredient(null, "FlameburstShortsword");
			recipe.AddIngredient(null, "LeechingDagger");
	        recipe.AddTile(TileID.DemonAltar);	
	        recipe.Register();
	        recipe = CreateRecipe();
			recipe.AddIngredient(null, "AncientShiv");
			recipe.AddIngredient(null, "SporeKnife");
			recipe.AddIngredient(null, "FlameburstShortsword");
			recipe.AddIngredient(null, "BloodyRupture");
	        recipe.AddTile(TileID.DemonAltar);	
	        recipe.Register();
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.Next(5) == 0)
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 14);
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
	    	if (target.life <= 0)
	    	{
	    		for (int i = 0; i <= 2; i++)
	    		{
	    			Projectile.NewProjectile(Entity.GetSource_FromThis(null), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("NightStabber").Type, (int)((float)Item.damage * player.GetDamage(DamageClass.Melee).Multiplicative), Item.knockBack, Main.myPlayer);
	    		}
	    	}
		}
	}
}
