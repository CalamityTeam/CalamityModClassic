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
public class ReaverHelmet : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 350000;
        Item.rare = ItemRarityID.LightPurple;
        Item.defense = 3; //36
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("ReaverScaleMail").Type && legs.type == Mod.Find<ModItem>("ReaverCuisses").Type;
    }
    
    public override void ArmorSetShadows(Player player)
    {
    	player.armorEffectDrawShadowSubtle = true;
    	player.armorEffectDrawOutlines = true;
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Grants increased summon and movement stats as health decreases\n" +
        	"You take more damage as health decreases\n" +
        	"Projectiles explode on hit\n" +
        	"Summons a reaver orb that emits spore gas when enemies are near\n" +
        	"Rage activates when you are damaged";
        CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
		modPlayer.reaverOrb = true;
		if (player.whoAmI == Main.myPlayer)
		{
			if (player.FindBuffIndex(Mod.Find<ModBuff>("ReaverOrb").Type) == -1)
			{
				player.AddBuff(Mod.Find<ModBuff>("ReaverOrb").Type, 3600, true);
			}
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("ReaverOrb").Type] < 1)
			{
				Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("ReaverOrb").Type, 0, 0f, Main.myPlayer, 0f, 0f);
			}
		}
		if(player.statLife <= (player.statLifeMax2 * 0.8f) && player.statLife > (player.statLifeMax2 * 0.6f))
		{
			player.endurance -= 0.05f;
			player.moveSpeed += 0.05f;
			player.GetDamage(DamageClass.Summon) += 0.05f;
			player.GetKnockback(DamageClass.Summon).Base += 0.05f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.6f) && player.statLife > (player.statLifeMax2 * 0.4f))
		{
			player.endurance -= 0.1f;
			player.moveSpeed += 0.1f;
			player.GetDamage(DamageClass.Summon) += 0.1f;
			player.GetKnockback(DamageClass.Summon).Base += 0.1f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.4f) && player.statLife > (player.statLifeMax2 * 0.2f))
		{
			player.endurance -= 0.2f;
			player.moveSpeed += 0.2f;
			player.GetDamage(DamageClass.Summon) += 0.15f;
			player.GetKnockback(DamageClass.Summon).Base += 0.15f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.2f))
		{
			player.endurance -= 0.3f;
			player.moveSpeed += 0.3f;
			player.GetDamage(DamageClass.Summon) += 0.2f;
			player.GetKnockback(DamageClass.Summon).Base += 0.2f;
		}
    }
    
    public override void UpdateEquip(Player player)
    {
    	player.lavaImmune = true;
    	player.ignoreWater = true;
    	player.buffImmune[BuffID.CursedInferno] = true;
    	player.GetDamage(DamageClass.Summon) += 0.1f;
        player.GetKnockback(DamageClass.Summon).Base += 1f;
		player.maxMinions += 2;
        player.moveSpeed += 0.05f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "DraedonBar", 8);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}