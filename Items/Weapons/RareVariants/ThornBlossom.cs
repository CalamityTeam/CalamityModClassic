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
	public class ThornBlossom : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Thorn Blossom");
			// Tooltip.SetDefault("Every rose has its thorn");
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 45;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 10;
	        Item.width = 66;
	        Item.height = 68;
	        Item.useTime = 23;
	        Item.useAnimation = 23;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 2f;
            Item.value = Item.buyPrice(0, 95, 0, 0);
            Item.rare = 9;
	        Item.UseSound = SoundID.Item109;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("BeamingBolt").Type;
	        Item.shootSpeed = 20f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 22;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			player.statLife -= 5;
			if (player.statLife <= 0)
			{
				player.KillMe(PlayerDeathReason.ByOther(10), 1000.0, 0, false);
			}
			for (int index = 0; index < 3; ++index)
			{
				float SpeedX = velocity.X + (float)Main.rand.Next(-120, 121) * 0.05f;
				float SpeedY = velocity.Y + (float)Main.rand.Next(-120, 121) * 0.05f;
				Projectile.NewProjectile(Entity.GetSource_FromThis(null),position.X, position.Y, SpeedX * 1.5f, SpeedY * 1.5f, 150, (int)((double)damage * 1.5), knockback, player.whoAmI, 0f, 0f);
			}
			Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X * 0.66f, velocity.Y * 0.66f, type, damage, knockback, player.whoAmI, 0f, 0f);
			return false;
		}
	}
}