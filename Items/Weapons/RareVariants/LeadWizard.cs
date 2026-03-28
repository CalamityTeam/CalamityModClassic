using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.RareVariants
{
	public class LeadWizard : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Lead Wizard");
			/* Tooltip.SetDefault("Something's not right...\n" +
				"33% chance to not consume ammo"); */
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 88;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 66;
	        Item.height = 34;
	        Item.useTime = 2;
	        Item.reuseDelay = 10;
	        Item.useAnimation = 6;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 5f;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
	        Item.UseSound = SoundID.Item31;
	        Item.autoReuse = true;
	        Item.shoot = 10;
	        Item.shootSpeed = 20f;
	        Item.useAmmo = 97;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 22;
		}
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float SpeedX = velocity.X + (float)Main.rand.Next(-15, 16) * 0.05f;
			float SpeedY = velocity.Y + (float)Main.rand.Next(-15, 16) * 0.05f;
			SpeedX += velocity.Y + (float)Main.rand.Next(-85, 86) * 0.05f;
			SpeedY += velocity.X + (float)Main.rand.Next(-85, 86) * 0.05f;
			Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, 242, damage, knockback, player.whoAmI, 0.0f, 0.0f);
			return false;
		}
	    
	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) < 33)
	    		return false;
	    	return true;
	    }
	}
}