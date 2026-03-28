using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.Ravager
{
	public class UltimusCleaver : ModItem
	{
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ultimus Cleaver");
			// Tooltip.SetDefault("Launches damaging sparks when the player walks on the ground with this weapon out");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 300;
	        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.rare = 8;
	        Item.width = 82;
	        Item.height = 102;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = 5;
	        Item.knockBack = 5f;
            Item.value = Item.buyPrice(0, 80, 0, 0);
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("UltimusCleaverDust").Type;
			Item.shootSpeed = 10f;
	    }
	    
		public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, -10);
        }
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
			if(Collision.SolidCollision(position, player.width, player.height) && player.velocity.X != 0)
			{
				for(int i = 0; i < 5; i++)
				{
					float posX;
					float velocityX;
					if(player.direction == 1)
					{
						posX = (float)Main.rand.Next(10, 60);
						velocityX = (float)Main.rand.Next(2, 10);
					}
					else
					{
						posX = (float)Main.rand.Next(-60, -10);
						velocityX = (float)Main.rand.Next(-10, -2);
					}
					Projectile.NewProjectile(Entity.GetSource_FromThis(null), (player.Center.X + posX), (player.Center.Y + 20), velocityX, (float)Main.rand.Next(-7, -3), Mod.Find<ModProjectile>("UltimusCleaverDust").Type, (int)((float)Item.damage * 0.4), 0f, Main.myPlayer);
				}
			}
			return false;
		}
	}
}