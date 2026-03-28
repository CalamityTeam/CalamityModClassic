using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Permafrost
{
	public class IcicleArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Icicle Arrow");
			// Tooltip.SetDefault("Shatters into shards on impact");
		}

		public override void SetDefaults()
		{
			Item.damage = 14;
			Item.DamageType = DamageClass.Ranged;
            Item.consumable = true;
            Item.width = 14;
			Item.height = 50;
			Item.knockBack = 2.5f;
			Item.value = Item.buyPrice(0, 0, 0, 15);
            Item.rare = 3;
			Item.shoot = Mod.Find<ModProjectile>("IcicleArrow").Type;
            Item.shootSpeed = 1.0f;
			Item.ammo = AmmoID.Arrow;
            Item.maxStack = 999;
		}
    }
}
