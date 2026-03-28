using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class ShroomiteVisage : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shroomite Visage");
			// Tooltip.SetDefault("25% increased ranged damage for flamethrowers");
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.buyPrice(0, 30, 0, 0);
			Item.rare = 8;
			Item.defense = 11; //62
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ItemID.ShroomiteBreastplate && legs.type == ItemID.ShroomiteLeggings;
		}

		public override void UpdateArmorSet(Player player)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			player.shroomiteStealth = true;
			player.setBonus = "Ranged stealth while standing still";
		}

		public override void UpdateEquip(Player player)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.flamethrowerBoost = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.ShroomiteBar, 12);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}