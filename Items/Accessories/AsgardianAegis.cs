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
public class AsgardianAegis : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 60;
		Item.height = 54;
		Item.value = 10000000;
		Item.defense = 15;
		Item.accessory = true;
	}
	
	public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(43, 96, 222);
            }
        }
    }
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
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
        recipe.AddIngredient(null, "Phantoplasm", 5);
        recipe.AddTile(null, "DraedonsForge");
        recipe.Register();
	}
}}