using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class BrinyBaron : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Briny Baron");
			/* Tooltip.SetDefault("Legendary Drop\n" +
				"Striking an enemy with the blade causes a briny typhoon to appear\n" +
				"Right click to fire a razorwind aqua blade\n" +
                "Revengeance drop"); */
		}

		public override void SetDefaults()
		{
			Item.width = 62;
			Item.damage = 66;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.useTurn = true;
			Item.useStyle = 1;
			Item.knockBack = 4f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 62;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
            Item.shootSpeed = 4f;
			Item.shoot = Mod.Find<ModProjectile>("NobodyKnows").Type;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 17;
		}
		
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				Item.noMelee = true;
	        	Item.noUseGraphic = true;
	    		Item.useTime = 15;
	    		Item.useAnimation = 15;
	        	Item.UseSound = SoundID.Item84;
			}
			else
			{
				Item.noMelee = false;
	        	Item.noUseGraphic = false;
	    		Item.useTime = 20;
	    		Item.useAnimation = 20;
	        	Item.UseSound = SoundID.Item1;
			}
			return base.CanUseItem(player);
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	if (player.altFunctionUse == 2)
	    	{
	        	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("Razorwind").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    		return false;
	    	}
	    	else
	    	{
	        	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("NobodyKnows").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    		return false;
	    	}
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.Next(3) == 0)
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 187, 0f, 0f, 100, new Color(53, Main.DiscoG, 255));
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
	    	Projectile.NewProjectile(Entity.GetSource_FromThis(null), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("BrinyTyphoonBubble").Type, (int)((float)Item.damage * player.GetDamage(DamageClass.Melee).Multiplicative), Item.knockBack, player.whoAmI);
		}
	}
}
