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
public class DaedalusHelmet : ModItem
{

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 300000;
        Item.rare = ItemRarityID.Pink;
        Item.defense = 9; //39
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("DaedalusBreastplate").Type && legs.type == Mod.Find<ModItem>("DaedalusLeggings").Type;
    }
    
    public override void ArmorSetShadows(Player player)
    {
    	player.armorEffectDrawShadowSubtle = true;
    	player.armorEffectDrawOutlines = true;
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Grants ranged damage, critical strike chance, and defensive boosts as health gets lower\n" +
        	"Getting hit causes you to emit a blast of crystal shards";
        CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
        modPlayer.daedalusShard = true;
        if(player.statLife <= (player.statLifeMax2 * 0.8f) && player.statLife > (player.statLifeMax2 * 0.6f))
		{
			player.endurance += 0.025f;
			player.GetCritChance(DamageClass.Ranged) += 2;
			player.GetDamage(DamageClass.Ranged) += 0.025f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.6f) && player.statLife > (player.statLifeMax2 * 0.4f))
		{
			player.endurance += 0.05f;
			player.GetCritChance(DamageClass.Ranged) += 5;
			player.GetDamage(DamageClass.Ranged) += 0.05f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.4f) && player.statLife > (player.statLifeMax2 * 0.2f))
		{
			player.endurance += 0.075f;
			player.GetCritChance(DamageClass.Ranged) += 7;
			player.GetDamage(DamageClass.Ranged) += 0.075f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.2f))
		{
			player.endurance += 0.1f;
			player.GetCritChance(DamageClass.Ranged) += 10;
			player.GetDamage(DamageClass.Ranged) += 0.1f;
		}
    }
    
    public override void UpdateEquip(Player player)
    {
    	player.ammoCost80 = true;
    	player.GetDamage(DamageClass.Ranged) += 0.1f;
    	player.GetCritChance(DamageClass.Ranged) += 10;
        player.AddBuff(BuffID.Gravitation, 2);
    	player.buffImmune[BuffID.Cursed] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "VerstaltiteBar", 8);
		recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}