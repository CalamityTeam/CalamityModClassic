using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage
{
	public class CosmicKunai : CalamityDamageItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cosmic Kunai");
			// Tooltip.SetDefault("Fires a stream of short-range kunai");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 26;
			Item.damage = 75;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useTime = 1;
			Item.useAnimation = 5;
			Item.useStyle = 1;
			Item.knockBack = 5f;
			Item.UseSound = SoundID.Item109;
			Item.autoReuse = true;
			Item.height = 48;
            Item.value = Item.buyPrice(1, 40, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("CosmicKunai").Type;
			Item.shootSpeed = 28f;
			Item.rare = 9;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}
	}
}
