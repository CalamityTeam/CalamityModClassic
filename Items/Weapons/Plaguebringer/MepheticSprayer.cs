using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.Plaguebringer
{
	public class MepheticSprayer : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Blight Spewer");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 99;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 76;
			Item.height = 36;
			Item.useTime = 10;
			Item.useAnimation = 30;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 2f;
			Item.UseSound = SoundID.Item34;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("CorossiveFlames").Type;
			Item.shootSpeed = 7.5f;
			Item.useAmmo = 23;
		}
	}
}