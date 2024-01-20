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
public class XerocMask : ModItem
{

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 500000;
        Item.rare = ItemRarityID.Cyan;
        Item.defense = 20; //71
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("XerocPlateMail").Type && legs.type == Mod.Find<ModItem>("XerocCuisses").Type;
    }
    
    public override void ArmorSetShadows(Player player)
    {
    	player.armorEffectDrawShadow = true;
    	player.armorEffectDrawOutlines = true;
    }

    public override void UpdateArmorSet(Player player)
    {
    	CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
    	modPlayer.xerocSet = true;
        player.setBonus = "Imbued with wrath and rage as health drops\n" +
        	"Defense up and damage down when health is critical\n" +
        	"All projectile types have special effects on enemy hits\n" +
        	"Imbued with cosmic wrath and rage when you are damaged";
        if(player.statLife <= (player.statLifeMax2 * 0.8f) && player.statLife > (player.statLifeMax2 * 0.6f))
		{
        	player.manaCost *= 0.95f;
			player.GetCritChance(DamageClass.Magic) += 5;
			player.GetDamage(DamageClass.Magic) += 0.05f;
			player.GetCritChance(DamageClass.Melee) += 5;
			player.GetDamage(DamageClass.Melee) += 0.05f;
			player.GetCritChance(DamageClass.Throwing) += 5;
			player.GetDamage(DamageClass.Throwing) += 0.05f;
			player.GetCritChance(DamageClass.Ranged) += 5;
			player.GetDamage(DamageClass.Ranged) += 0.05f;
			player.GetDamage(DamageClass.Summon) += 0.05f;
			player.GetAttackSpeed(DamageClass.Melee) += 0.05f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.6f) && player.statLife > (player.statLifeMax2 * 0.4f))
		{
			player.manaCost *= 0.9f;
			player.GetCritChance(DamageClass.Magic) += 10;
			player.GetDamage(DamageClass.Magic) += 0.1f;
			player.GetCritChance(DamageClass.Melee) += 10;
			player.GetDamage(DamageClass.Melee) += 0.1f;
			player.GetCritChance(DamageClass.Throwing) += 10;
			player.GetDamage(DamageClass.Throwing) += 0.1f;
			player.GetCritChance(DamageClass.Ranged) += 10;
			player.GetDamage(DamageClass.Ranged) += 0.1f;
			player.GetDamage(DamageClass.Summon) += 0.1f;
			player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.4f) && player.statLife > (player.statLifeMax2 * 0.2f))
		{
			player.manaCost *= 0.8f;
			player.GetCritChance(DamageClass.Magic) += 20;
			player.GetDamage(DamageClass.Magic) += 0.2f;
			player.GetCritChance(DamageClass.Melee) += 20;
			player.GetDamage(DamageClass.Melee) += 0.2f;
			player.GetCritChance(DamageClass.Throwing) += 20;
			player.GetDamage(DamageClass.Throwing) += 0.2f;
			player.GetCritChance(DamageClass.Ranged) += 20;
			player.GetDamage(DamageClass.Ranged) += 0.2f;
			player.GetDamage(DamageClass.Summon) += 0.2f;
			player.GetAttackSpeed(DamageClass.Melee) += 0.2f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.2f))
		{
			player.endurance += 0.3f;
			player.manaCost *= 0.6f;
			player.GetCritChance(DamageClass.Magic) -= 5;
			player.GetDamage(DamageClass.Magic) -= 0.05f;
			player.GetCritChance(DamageClass.Melee) -= 5;
			player.GetDamage(DamageClass.Melee) -= 0.05f;
			player.GetCritChance(DamageClass.Throwing) -= 5;
			player.GetDamage(DamageClass.Throwing) -= 0.05f;
			player.GetCritChance(DamageClass.Ranged) -= 5;
			player.GetDamage(DamageClass.Ranged) -= 0.05f;
			player.GetDamage(DamageClass.Summon) -= 0.05f;
			player.GetAttackSpeed(DamageClass.Melee) += 0.25f;
		}
        if(player.statLife <= (player.statLifeMax2 * 0.5f))
   		{
   			player.AddBuff(BuffID.Wrath, 2);
   			player.AddBuff(BuffID.Rage, 2);
   		}
    }
    
    public override void UpdateEquip(Player player)
    {
    	player.maxMinions += 3;
    	player.GetDamage(DamageClass.Melee) += 0.1f;
        player.GetCritChance(DamageClass.Melee) += 10;
        player.GetDamage(DamageClass.Ranged) += 0.1f;
        player.GetCritChance(DamageClass.Ranged) += 10;
        player.GetDamage(DamageClass.Magic) += 0.1f;
        player.GetCritChance(DamageClass.Magic) += 10;
        player.GetDamage(DamageClass.Throwing) += 0.1f;
        player.GetCritChance(DamageClass.Throwing) += 10;
        player.GetDamage(DamageClass.Summon) += 0.1f;
    	player.lavaImmune = true;
    	player.buffImmune[BuffID.OnFire] = true;
    	player.buffImmune[BuffID.CursedInferno] = true;
    	player.buffImmune[BuffID.Cursed] = true;
    	player.buffImmune[BuffID.Chilled] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "MeldiateBar", 9);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}}