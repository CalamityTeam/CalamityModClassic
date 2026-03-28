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
	public class UrchinStinger : CalamityDamageItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Urchin Stinger");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 10;
			Item.damage = 15;
			Item.noMelee = true;
			Item.consumable = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 14;
			Item.useStyle = 1;
			Item.useTime = 14;
			Item.knockBack = 1.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 26;
			Item.maxStack = 999;
			Item.value = 200;
			Item.rare = 1;
			Item.shoot = Mod.Find<ModProjectile>("UrchinStinger").Type;
			Item.shootSpeed = 12f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}
	}
}
