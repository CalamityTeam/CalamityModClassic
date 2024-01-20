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
public class DaedalusHat : ModItem
{
    public override void SetStaticDefaults()
    {
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
            ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
        }

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 300000;
        Item.rare = ItemRarityID.Pink;
        Item.defense = 5; //35
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
        player.setBonus = "Grants magic damage, crit chance, and defensive boosts as health gets lower\n" +
        	"You have a 10% chance to absorb physical attacks and projectiles when hit\n" +
        	"If you absorb an attack you are healed for 1/2 of that attack's damage";
        CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
        modPlayer.daedalusAbsorb = true;
        if(player.statLife <= (player.statLifeMax2 * 0.8f) && player.statLife > (player.statLifeMax2 * 0.6f))
		{
			player.endurance += 0.025f;
			player.GetCritChance(DamageClass.Magic) += 2;
			player.GetDamage(DamageClass.Magic) += 0.025f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.6f) && player.statLife > (player.statLifeMax2 * 0.4f))
		{
			player.endurance += 0.05f;
			player.GetCritChance(DamageClass.Magic) += 5;
			player.GetDamage(DamageClass.Magic) += 0.05f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.4f) && player.statLife > (player.statLifeMax2 * 0.2f))
		{
			player.endurance += 0.075f;
			player.GetCritChance(DamageClass.Magic) += 7;
			player.GetDamage(DamageClass.Magic) += 0.075f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.2f))
		{
			player.endurance += 0.1f;
			player.GetCritChance(DamageClass.Magic) += 10;
			player.GetDamage(DamageClass.Magic) += 0.1f;
		}
    }
    
    public override void UpdateEquip(Player player)
    {
    	player.GetDamage(DamageClass.Magic) += 0.1f;
        player.GetCritChance(DamageClass.Magic) += 10;
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