using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Armor {
[AutoloadEquip(EquipType.Head)]
public class AtaxiaHelm : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 450000;
        Item.rare = ItemRarityID.Yellow;
        Item.defense = 31; //67
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("AtaxiaArmor").Type && legs.type == Mod.Find<ModItem>("AtaxiaSubligar").Type;
    }
    
    public override void ArmorSetShadows(Player player)
    {
    	player.armorEffectDrawOutlines = true;
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Melee damage buffs and slight defense debuffs as health decreases\n" +
        	"Inferno effect when below 50% life\n" +
        	"Melee attacks and projectiles cause chaos flames to erupt on enemy hits\n" +
        	"You have a 20% chance to emit a blazing explosion when you are hit";
        CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
		modPlayer.ataxiaBlaze = true;
    	modPlayer.ataxiaGeyser = true;
    	if(player.statLife <= (player.statLifeMax2 * 0.8f) && player.statLife > (player.statLifeMax2 * 0.6f))
		{
			player.endurance -= 0.025f;
			player.GetCritChance(DamageClass.Melee) += 5;
			player.GetDamage(DamageClass.Melee) += 0.05f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.6f) && player.statLife > (player.statLifeMax2 * 0.4f))
		{
			player.endurance -= 0.05f;
			player.GetCritChance(DamageClass.Melee) += 10;
			player.GetDamage(DamageClass.Melee) += 0.1f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.4f) && player.statLife > (player.statLifeMax2 * 0.2f))
		{
			player.endurance -= 0.1f;
			player.GetCritChance(DamageClass.Melee) += 15;
			player.GetDamage(DamageClass.Melee) += 0.15f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.2f))
		{
			player.endurance -= 0.15f;
			player.GetCritChance(DamageClass.Melee) += 20;
			player.GetDamage(DamageClass.Melee) += 0.2f;
		}
        if(player.statLife <= (player.statLifeMax2 * 0.5f))
       	{
       		player.AddBuff(BuffID.Inferno, 2);
       	}
    }
    
    public override void UpdateEquip(Player player)
    {
        CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
    	modPlayer.ataxiaFire = true;
        player.GetDamage(DamageClass.Melee) += 0.12f;
        player.GetCritChance(DamageClass.Melee) += 10;
    	player.lavaImmune = true;
    	player.buffImmune[BuffID.OnFire] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "CruptixBar", 7);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}