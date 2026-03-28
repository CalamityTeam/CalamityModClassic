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
	public class ScourgeoftheDesert : CalamityDamageItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Scourge of the Desert");
        }

		public override void SafeSetDefaults()
		{
			Item.width = 44;
			Item.damage = 16;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 20;
			Item.useStyle = 1;
			Item.useTime = 20;
			Item.knockBack = 3.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 44;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.rare = 2;
			Item.shoot = Mod.Find<ModProjectile>("ScourgeoftheDesert").Type;
			Item.shootSpeed = 12f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}
	}
}
