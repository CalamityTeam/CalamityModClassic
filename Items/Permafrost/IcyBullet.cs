using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Permafrost
{
	public class IcyBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Icy Bullet");
			// Tooltip.SetDefault("Can hit up to three times\nBreaks into ice shards on last impact");
		}
		public override void SetDefaults()
		{
			Item.damage = 10;
			Item.DamageType = DamageClass.Ranged;
            Item.consumable = true;
			Item.width = 18;
			Item.height = 16;
			Item.knockBack = 2f;
			Item.value = Item.buyPrice(0, 0, 0, 15);
            Item.rare = 3;
			Item.shoot = Mod.Find<ModProjectile>("IcyBullet").Type;
            Item.shootSpeed = 5f;
            Item.ammo = AmmoID.Bullet;
            Item.maxStack = 999;
		}
    }
}
