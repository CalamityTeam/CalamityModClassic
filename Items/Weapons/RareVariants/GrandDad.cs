using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.RareVariants
{
	public class GrandDad : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Grand Dad");
			/* Tooltip.SetDefault("Lowers enemy defense to 0 when they are struck\n" +
						"Yeets enemies across space and time\n" +
						"7"); */
		}

		public override void SetDefaults()
		{
			Item.width = 124;
			Item.damage = 777;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 25;
			Item.useStyle = 1;
			Item.useTime = 25;
			Item.useTurn = true;
			Item.knockBack = 77f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 124;
			Item.value = Item.buyPrice(1, 0, 0, 0);
			Item.rare = 10;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 22;
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (!target.boss)
			{
				target.knockBackResist = 7f;
				target.defense = 0;
			}
		}
	}
}
