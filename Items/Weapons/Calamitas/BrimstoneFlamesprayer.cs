using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.Calamitas
{
	public class BrimstoneFlamesprayer : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Havoc's Breath");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 59;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 50;
			Item.height = 18;
			Item.useTime = 9;
			Item.useAnimation = 30;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 1.5f;
			Item.UseSound = SoundID.Item34;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("BrimstoneFireFriendly").Type;
			Item.shootSpeed = 8.5f;
			Item.useAmmo = 23;
		}
	}
}