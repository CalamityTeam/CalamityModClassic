using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons
{
	public class MandibleClaws : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Mandible Claws");
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.damage = 14;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 6;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 6;
			Item.useTurn = true;
			Item.knockBack = 3.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 18;
			Item.value = 10000;
			Item.rare = ItemRarityID.Blue;
		}
	}
}
