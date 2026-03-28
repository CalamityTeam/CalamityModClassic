using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.Astral
{
	public class TitanArm : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Titan Arm");
			// Tooltip.SetDefault("Slap Hand but better");
		}

		public override void SetDefaults()
		{
			Item.width = 46;
			Item.height = 58;
			Item.damage = 60;
			Item.crit += 96; //more knockback huehue
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useTurn = true;
			Item.useAnimation = 12;
			Item.useStyle = 1;
			Item.useTime = 12;
			Item.knockBack = 9001f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
        }
	}
}
