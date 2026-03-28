using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Patreon
{
	public class Keelhaul : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Keelhaul");
			/* Tooltip.SetDefault("Summons a geyser upon hitting an enemy\n"+
							   "Crumple 'em like paper"); */
		}

		public override void SetDefaults()
		{
			Item.width = 42;
			Item.height = 42;
			Item.damage = 55;
			Item.mana = 50;
			Item.DamageType = DamageClass.Magic;
			Item.noMelee = true;
			Item.useTime = 30;
            Item.useAnimation = 30;
			Item.useTurn = true;
			Item.useStyle = 1;
			Item.knockBack = 2f;
			Item.UseSound = SoundID.Item102;
			Item.autoReuse = true;
			Item.rare = 8;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 21;
			Item.value = Item.buyPrice(0, 80, 0, 0);
			Item.shoot = Mod.Find<ModProjectile>("KeelhaulBubble").Type;
			Item.shootSpeed = 15f;
		}
	}
}
