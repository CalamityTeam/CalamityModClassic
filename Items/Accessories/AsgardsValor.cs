using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
[AutoloadEquip(EquipType.Shield)]
public class AsgardsValor : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 38;
		Item.height = 44;
		Item.value = 5000000;
		Item.rare = ItemRarityID.Cyan;
		Item.defense = 5;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
		modPlayer.dashMod = 2;
		player.buffImmune[46] = true;
		player.buffImmune[44] = true;
		player.noKnockback = true;
		player.fireWalk = true;
		player.buffImmune[33] = true;
		player.buffImmune[36] = true;
		player.buffImmune[30] = true;
		player.buffImmune[20] = true;
		player.buffImmune[32] = true;
		player.buffImmune[31] = true;
		player.buffImmune[35] = true;
		player.buffImmune[23] = true;
		player.buffImmune[22] = true;
		player.buffImmune[Mod.Find<ModBuff>("BrimstoneFlames").Type] = true;
		player.buffImmune[Mod.Find<ModBuff>("HolyLight").Type] = true;
		player.buffImmune[Mod.Find<ModBuff>("GlacialState").Type] = true;
		player.lifeRegen += 2;
		player.statLifeMax2 += 50;
		if(player.statLife <= (player.statLifeMax2 * 0.25f))
		{
			player.statDefense += 10;
		}
		if(player.wet == true || player.honeyWet == true || player.lavaWet == true)
		{
			player.endurance += 0.12f;
		}
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.AnkhShield);
		recipe.AddIngredient(null, "OrnateShield");
		recipe.AddIngredient(null, "ShieldoftheOcean");
		recipe.AddIngredient(null, "Abaddon");
		recipe.AddIngredient(null, "CoreofEleum", 3);
		recipe.AddIngredient(null, "CoreofCinder", 3);
		recipe.AddIngredient(null, "CoreofChaos", 3);
		recipe.AddIngredient(ItemID.LifeFruit, 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}