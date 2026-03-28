using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;

namespace CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage
{
	public class FrostyFlare : CalamityDamageItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Frosty Flare");
            /* Tooltip.SetDefault("Sticks to enemies\n" +
				"Generates a localized hailstorm\n'Do not insert in flare gun'"); */
        }

		public override void SafeSetDefaults()
		{
			Item.damage = 32;
            Item.noUseGraphic = true;
            Item.noMelee = true;
			Item.width = 10;
			Item.height = 22;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = 1;
            Item.useTurn = false;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.knockBack = 2f;
			Item.value = Item.buyPrice(0, 0, 5, 0);
            Item.rare = 5;
			Item.shoot = Mod.Find<ModProjectile>("FrostyFlare").Type;
            Item.shootSpeed = 22f;
            Item.maxStack = 999;
            Item.consumable = true;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}
    }
}
