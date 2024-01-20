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
public class AtaxiaHelmet : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 450000;
        Item.rare = ItemRarityID.Yellow;
        Item.defense = 4; //40
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
        player.setBonus = "Summon damage buffs and slight defense debuffs as health decreases\n" +
        	"Inferno effect when below 50% life\n" +
        	"Summons a chaos spirit to protect you\n" +
        	"You have a 20% chance to emit a blazing explosion when you are hit";
        CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
        modPlayer.ataxiaBlaze = true;
		modPlayer.chaosSpirit = true;
		if (player.whoAmI == Main.myPlayer)
		{
			if (player.FindBuffIndex(Mod.Find<ModBuff>("ChaosSpirit").Type) == -1)
			{
				player.AddBuff(Mod.Find<ModBuff>("ChaosSpirit").Type, 3600, true);
			}
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("ChaosSpirit").Type] < 1)
			{
				Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("ChaosSpirit").Type, 0, 0f, Main.myPlayer, 0f, 0f);
			}
		}
		if(player.statLife <= (player.statLifeMax2 * 0.8f) && player.statLife > (player.statLifeMax2 * 0.6f))
		{
			player.endurance -= 0.025f;
			player.GetDamage(DamageClass.Summon) += 0.05f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.6f) && player.statLife > (player.statLifeMax2 * 0.4f))
		{
			player.endurance -= 0.05f;
			player.GetDamage(DamageClass.Summon) += 0.1f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.4f) && player.statLife > (player.statLifeMax2 * 0.2f))
		{
			player.endurance -= 0.1f;
			player.GetDamage(DamageClass.Summon) += 0.15f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.2f))
		{
			player.endurance -= 0.15f;
			player.GetDamage(DamageClass.Summon) += 0.2f;
		}
        if(player.statLife <= (player.statLifeMax2 * 0.5f))
       	{
       		player.AddBuff(BuffID.Inferno, 2);
       	}
    }
    
    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Summon) += 0.12f;
        player.GetKnockback(DamageClass.Summon).Base += 1.5f;
		player.maxMinions += 2;
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