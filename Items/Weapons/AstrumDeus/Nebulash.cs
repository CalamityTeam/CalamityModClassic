using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons.AstrumDeus
{
	public class Nebulash : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Nebulash");
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.damage = 54;
            Item.rare = 7;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.channel = true;
			Item.autoReuse = true;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 18;
			Item.useTime = 18;
			Item.useStyle = 5;
			Item.knockBack = 2f;
			Item.UseSound = SoundID.Item117;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.shootSpeed = 24f;
			Item.shoot = Mod.Find<ModProjectile>("Nebulash").Type;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	float ai3 = (Main.rand.NextFloat() - 0.5f) * 0.7853982f; //0.5
	    	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 0.0f, ai3);
	    	return false;
		}
	}
}
