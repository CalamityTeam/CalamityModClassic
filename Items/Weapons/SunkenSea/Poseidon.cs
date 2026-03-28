using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.SunkenSea
{
	public class Poseidon : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Poseidon");
			// Tooltip.SetDefault("Casts a poseidon typhoon");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 58;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 15;
	        Item.width = 28;
	        Item.height = 32;
	        Item.useTime = 22;
	        Item.useAnimation = 22;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 6f;
			Item.value = Item.buyPrice(0, 36, 0, 0);
			Item.UseSound = SoundID.Item84;
			Item.rare = 5;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("PoseidonTyphoon").Type;
	        Item.shootSpeed = 10f;
	    }
	}
}