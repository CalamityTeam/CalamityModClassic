using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage
{
	public class Valediction : CalamityDamageItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Valediction");
			// Tooltip.SetDefault("Throws a homing reaper scythe");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 80;
			Item.damage = 405;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 15;
			Item.useTime = 15;
			Item.useStyle = 1;
			Item.knockBack = 7f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 64;
            Item.value = Item.buyPrice(1, 40, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("Valediction").Type;
			Item.shootSpeed = 20f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}
	}
}
