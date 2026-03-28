using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons.HiveMind
{
	public class LeechingDagger : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Leeching Dagger");
			// Tooltip.SetDefault("Enemies release homing leech orbs on death");
		}

		public override void SetDefaults()
		{
			Item.useStyle = 3;
			Item.useTurn = false;
			Item.useAnimation = 15;
			Item.useTime = 15;
			Item.width = 26;
			Item.height = 26;
			Item.damage = 26;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.knockBack = 5.25f;
			Item.UseSound = SoundID.Item1;
			Item.useTurn = true;
			Item.autoReuse = true;
            Item.value = Item.buyPrice(0, 4, 0, 0);
            Item.rare = 3;
		}
		
		public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.RottenChunk, 2);
	        recipe.AddIngredient(ItemID.DemoniteBar, 5);
	        recipe.AddIngredient(null, "TrueShadowScale", 4);
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
	    		Projectile.NewProjectile(Entity.GetSource_FromThis(null),target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("Leech").Type, (int)((float)Item.damage * player.GetDamage(DamageClass.Melee).Multiplicative), Item.knockBack, Main.myPlayer);
	    	}
		}
	}
}
