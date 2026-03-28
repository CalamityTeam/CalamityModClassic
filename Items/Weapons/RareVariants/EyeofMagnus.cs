using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons.RareVariants
{
	public class EyeofMagnus : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Eye of Magnus");
			/* Tooltip.SetDefault("Fires powerful beams\n" +
				"Heals mana and health on enemy hits"); */
		}

		public override void SetDefaults()
		{
			Item.width = 80;
			Item.damage = 55;
			Item.rare = 5;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.useStyle = 5;
			Item.knockBack = 2f;
			Item.UseSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/Item/LaserCannon");
			Item.DamageType = DamageClass.Magic;
			Item.mana = 7;
			Item.autoReuse = true;
			Item.noMelee = true;
			Item.height = 50;
			Item.value = Item.buyPrice(0, 36, 0, 0);
			Item.shoot = Mod.Find<ModProjectile>("MagnusBeam").Type;
			Item.shootSpeed = 12f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 22;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-15, 0);
		}
	}
}
