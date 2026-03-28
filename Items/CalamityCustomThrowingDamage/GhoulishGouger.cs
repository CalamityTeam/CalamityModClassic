using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage
{
	public class GhoulishGouger : CalamityDamageItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ghoulish Gouger");
			// Tooltip.SetDefault("Throws a ghoulish scythe");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 68;
			Item.damage = 160;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 12;
			Item.useTime = 12;
			Item.useStyle = 1;
			Item.knockBack = 7.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 60;
            Item.value = Item.buyPrice(1, 40, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("GhoulishGouger").Type;
			Item.shootSpeed = 20f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}
	}
}
