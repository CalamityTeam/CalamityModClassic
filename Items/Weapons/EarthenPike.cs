using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class EarthenPike : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Earthen Pike");
			// Tooltip.SetDefault("Crushes enemy defenses");
		}

		public override void SetDefaults()
		{
			Item.width = 60;
			Item.damage = 50;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.noMelee = true;
			Item.useTurn = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 25;
			Item.useStyle = 5;
			Item.useTime = 25;
			Item.knockBack = 7f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.height = 60;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
			Item.shoot = Mod.Find<ModProjectile>("EarthenPike").Type;
			Item.shootSpeed = 6f;
		}
	}
}
