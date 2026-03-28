using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage.RareVariants
{
	public class TheReaper : CalamityDamageItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Reaper");
			// Tooltip.SetDefault("Slice 'n dice");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 80;
			Item.damage = 350;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 22;
			Item.useTime = 22;
			Item.useStyle = 1;
			Item.knockBack = 4f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 64;
            Item.value = Item.buyPrice(1, 40, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("Valediction2").Type;
			Item.shootSpeed = 20f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 22;
		}
	}
}
