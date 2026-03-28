using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using Terraria.Audio;

namespace CalamityModClassicPreTrailer.Items.Weapons.RareVariants
{
	public class Thunderstorm : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Thunderstorm");
			// Tooltip.SetDefault("Make it rain");
		}

		public override void SetDefaults()
		{
			Item.damage = 360;
			Item.mana = 50;
			Item.DamageType = DamageClass.Magic;
			Item.width = 48;
			Item.height = 22;
			Item.useTime = 24;
			Item.useAnimation = 24;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 2f;
			Item.value = Item.buyPrice(1, 20, 0, 0);
			Item.rare = 10;
			Item.UseSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/Item/PlasmaBlast");
			Item.autoReuse = true;
			Item.shootSpeed = 6f;
			Item.shoot = Mod.Find<ModProjectile>("ThunderstormShot").Type;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 22;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
	}
}