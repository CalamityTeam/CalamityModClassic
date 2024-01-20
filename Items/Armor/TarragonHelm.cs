using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items.Armor;

namespace CalamityModClassic1Point2.Items.Armor {
[AutoloadEquip(EquipType.Head)]
public class TarragonHelm : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 1550000;
        Item.defense = 27; //83
    }
    
    public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(0, 255, 200);
            }
        }
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("TarragonBreastplate").Type && legs.type == Mod.Find<ModItem>("TarragonLeggings").Type;
    }
    
    public override void ArmorSetShadows(Player player)
    {
    	player.armorEffectDrawShadowSubtle = true;
    	player.armorEffectDrawOutlines = true;
    }

    public override void UpdateArmorSet(Player player)
    {
    	CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
    	modPlayer.tarraSet = true;
        player.setBonus = "Grants multiple defense boosts if health is low\n" +
        	"You regen health quickly when you damage enemies\n" +
        	"Enemies are less likely to target you\n" +
        	"Increased heart pickup range\n" +
        	"Enemies have a chance to drop extra hearts on death";
        if(player.statLife <= (player.statLifeMax2 * 0.8f) && player.statLife > (player.statLifeMax2 * 0.6f))
		{
			player.statDefense += 4;
			player.endurance += 0.025f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.6f) && player.statLife > (player.statLifeMax2 * 0.4f))
		{
			player.statDefense += 8;
			player.endurance += 0.05f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.4f) && player.statLife > (player.statLifeMax2 * 0.2f))
		{
			player.statDefense += 12;
			player.endurance += 0.1f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.2f))
		{
			player.statDefense += 20;
			player.endurance += 0.15f;
		}
    }
    
    public override void UpdateEquip(Player player)
    {
    	player.endurance += 0.05f;
    	player.maxMinions += 3;
    	player.lavaImmune = true;
    	player.ignoreWater = true;
    	player.buffImmune[BuffID.CursedInferno] = true;
    	player.buffImmune[BuffID.OnFire] = true;
    	player.buffImmune[BuffID.Cursed] = true;
    	player.buffImmune[BuffID.Chilled] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "UeliaceBar", 7);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}}