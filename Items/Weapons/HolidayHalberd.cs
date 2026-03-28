using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class HolidayHalberd : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Holiday Halberd");
			// Tooltip.SetDefault("idk I'm miserable with names\n- The General");
		}

		public override void SetDefaults()
		{
			Item.width = 70;
			Item.damage = 94;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 17;
			Item.useStyle = 1;
			Item.useTime = 17;
			Item.useTurn = true;
			Item.knockBack = 7.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 72;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
			Item.shoot = Mod.Find<ModProjectile>("RedBall").Type;
			Item.shootSpeed = 12f;
		}
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
			int dustType = 0;
			switch (Main.rand.Next(4))
			{
				case 1: dustType = 107; break;
				case 2: dustType = 90; break;
			}
	        if (Main.rand.Next(5) == 0)
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, dustType);
	        }
	    }
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	switch (Main.rand.Next(6))
			{
	    		case 1: type = Mod.Find<ModProjectile>("RedBall").Type; break;
	    		case 2: type = Mod.Find<ModProjectile>("GreenBall").Type; break;
	    		default: break;
			}
	       	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, Main.myPlayer);
	    	return false;
		}
	}
}
