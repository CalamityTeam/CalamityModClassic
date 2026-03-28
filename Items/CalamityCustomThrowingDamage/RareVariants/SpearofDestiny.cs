using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage.RareVariants
{
	public class SpearofDestiny : CalamityDamageItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Spear of Destiny");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 52;
			Item.damage = 25;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 20;
			Item.useStyle = 1;
			Item.useTime = 20;
			Item.knockBack = 2f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 52;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
			Item.shoot = Mod.Find<ModProjectile>("IchorSpear").Type;
			Item.shootSpeed = 20f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 22;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			int numProj = 2;
			float rotation = MathHelper.ToRadians(3);
			for (int i = 0; i < numProj + 1; i++)
			{
				Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numProj - 1)));
				Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, (i == 1 ? type : Mod.Find<ModProjectile>("IchorSpear2").Type), damage, knockback, player.whoAmI, 0f, 0f);
			}
			return false;
		}
	}
}
