using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
	[AutoloadEquip(EquipType.Shield)]
public class AsgardianAegis : ModItem
{	
	public override void SetDefaults()
	{
		Item.width = 60;
		Item.height = 54;
		Item.value = 10000000;
		Item.expert = true;
		Item.defense = 15;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer1Point1 modPlayer = player.GetModPlayer<CalamityPlayer1Point1>();
		modPlayer.dashMod = 4;
		modPlayer.elysianAegis = true;
		player.noKnockback = true;
		player.fireWalk = true;
		player.lifeRegen += 5;
		player.statLifeMax2 += 150;
		player.buffImmune[46] = true;
		player.buffImmune[44] = true;
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
        recipe.AddIngredient(null, "AsgardsValor");
        recipe.AddIngredient(null, "ElysianAegis");
        recipe.AddIngredient(null, "CosmiliteBar", 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}