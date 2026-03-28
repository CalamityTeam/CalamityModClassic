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
	public class PearlGod : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Pearl God");
			// Tooltip.SetDefault("Your life is mine...");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 40;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 50;
	        Item.height = 32;
	        Item.useTime = 12;
	        Item.useAnimation = 12;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 3f;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
	        Item.UseSound = SoundID.Item41;
	        Item.autoReuse = true;
	        Item.shootSpeed = 24f;
	        Item.shoot = Mod.Find<ModProjectile>("ShockblastRound").Type;
	        Item.useAmmo = 97;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 22;
		}
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			for (int index = 0; index < 2; ++index)
			{
				float speedMult = (float)(index + 1) * 0.15f;
				Projectile.NewProjectile(Entity.GetSource_FromThis(null),position.X, position.Y, velocity.X * speedMult, velocity.Y * speedMult, Mod.Find<ModProjectile>("FrostsparkBullet").Type, (int)((double)damage * 0.5), knockback, player.whoAmI, 0.0f, 0.0f);
			}
	    	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("ShockblastRound").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	}
}