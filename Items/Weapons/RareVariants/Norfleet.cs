using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.RareVariants
{
	public class Norfleet : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Norfleet");
		}

	    public override void SetDefaults()
	    {
            Item.damage = 1280;
            Item.knockBack = 15f;
            Item.shootSpeed = 30f;
            Item.useStyle = 5;
            Item.useAnimation = 75;
            Item.useTime = 75;
            Item.reuseDelay = 0;
            Item.width = 140;
            Item.height = 42;
            Item.UseSound = SoundID.Item92;
            Item.shoot = Mod.Find<ModProjectile>("Norfleet").Type;
            Item.value = Item.buyPrice(1, 80, 0, 0);
            Item.rare = 10;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.Ranged;
            Item.channel = true;
            Item.useTurn = false;
            Item.useAmmo = 75;
            Item.autoReuse = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 22;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("Norfleet").Type, 0, 0f, player.whoAmI);
            return false;
		}
	}
}