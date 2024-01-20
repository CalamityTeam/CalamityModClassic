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
public class AuricTeslaHelm : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 10000000;
        Item.defense = 40; //132
    }
    
    public override void ModifyTooltips(List<TooltipLine> list)
	{
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
            }
        }
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("AuricTeslaBodyArmor").Type && legs.type == Mod.Find<ModItem>("AuricTeslaCuisses").Type;
    }
    
    public override void ArmorSetShadows(Player player)
    {
    	player.armorEffectDrawShadow = true;
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Tarragon, Bloodflare, God Slayer, and Silva armor effects\n" +
        	"Reduces all damage taken by 10%, this is calculated separately from damage reduction\n" +
        	"All projectiles spawn healing auric orbs on enemy hits\n" +
        	"Max run speed and acceleration boosted by 40%";
        CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
    	modPlayer.bloodflareSet = true;
    	modPlayer.godSlayerDamage = true;
    	modPlayer.godSlayerReflect = true;
    	modPlayer.godSlayer = true;
    	modPlayer.tarraSet = true;
    	player.thorns = 2f;
    	modPlayer.auricSet = true;
    	for (int k = 0; k < player.buffImmune.Length; k++)
		{
    		if (Main.debuff[k])
    		{
				player.buffImmune[k] = true;
				player.buffImmune[BuffID.MonsterBanner] = false;
				player.buffImmune[BuffID.HeartLamp] = false;
				player.buffImmune[BuffID.Sunflower] = false;
				player.buffImmune[BuffID.PeaceCandle] = false;
				player.buffImmune[BuffID.Campfire] = false;
				player.buffImmune[BuffID.WaterCandle] = false;
				player.buffImmune[BuffID.Werewolf] = false;
				player.buffImmune[BuffID.ChaosState] = false;
				player.buffImmune[BuffID.PotionSickness] = false;
				player.buffImmune[BuffID.ManaSickness] = false;
				player.buffImmune[Mod.Find<ModBuff>("ScarfCooldown").Type] = false;
				player.buffImmune[Mod.Find<ModBuff>("GodSlayerCooldown").Type] = false;
				player.buffImmune[Mod.Find<ModBuff>("VulnerabilityHex").Type] = false;
				player.buffImmune[Mod.Find<ModBuff>("HeartAttack").Type] = false;
				player.buffImmune[Mod.Find<ModBuff>("Warped").Type] = false;
				player.buffImmune[Mod.Find<ModBuff>("ExtremeGrav").Type] = false;
    		}
		}
    	player.lavaImmune = true;
    	player.ignoreWater = true;
    	if(player.statLife <= (player.statLifeMax2 * 0.8f) && player.statLife > (player.statLifeMax2 * 0.6f))
		{
			player.statDefense += 2;
			player.endurance += 0.01f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.6f) && player.statLife > (player.statLifeMax2 * 0.4f))
		{
			player.statDefense += 4;
			player.endurance += 0.02f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.4f) && player.statLife > (player.statLifeMax2 * 0.2f))
		{
			player.statDefense += 8;
			player.endurance += 0.05f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.2f))
		{
			player.statDefense += 16;
			player.endurance += 0.1f;
		}
		player.crimsonRegen = true;
        player.aggro += 1200;
        if (player.lavaWet == true)
    	{
        	player.statDefense += 30;
        	player.lifeRegen += 10;
        }
    }
    
    public override void UpdateEquip(Player player)
    {
    	CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
    	modPlayer.auricBoost = true;
    	player.maxMinions += 7;
    	player.GetDamage(DamageClass.Melee) += 0.3f;
        player.GetCritChance(DamageClass.Melee) += 30;
        player.GetDamage(DamageClass.Ranged) += 0.3f;
        player.GetCritChance(DamageClass.Ranged) += 30;
        player.GetDamage(DamageClass.Magic) += 0.3f;
        player.GetCritChance(DamageClass.Magic) += 30;
        player.GetDamage(DamageClass.Throwing) += 0.3f;
        player.GetCritChance(DamageClass.Throwing) += 30;
        player.GetDamage(DamageClass.Summon) += 0.3f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "SilvaHelm");
        recipe.AddIngredient(null, "GodSlayerHelm");
        recipe.AddIngredient(null, "BloodflareMask");
        recipe.AddIngredient(null, "TarragonHelm");
        recipe.AddIngredient(null, "EndothermicEnergy", 200);
        recipe.AddIngredient(null, "NightmareFuel", 200);
        recipe.AddIngredient(null, "Phantoplasm", 70);
        recipe.AddIngredient(null, "DarksunFragment", 30);
        recipe.AddIngredient(null, "BarofLife", 20);
        recipe.AddIngredient(null, "CoreofCalamity", 15);
        recipe.AddIngredient(null, "GalacticaSingularity", 10);
        recipe.AddIngredient(null, "PsychoticAmulet");
        recipe.AddTile(null, "DraedonsForge");
        recipe.Register();
    }
}}