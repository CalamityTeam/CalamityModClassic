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
public class StatigelHood : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 200000;
        Item.rare = ItemRarityID.Pink;
        Item.defense = 2; //20
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("StatigelArmor").Type && legs.type == Mod.Find<ModItem>("StatigelGreaves").Type;
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Grants minion damage and movement speed as health gets lower\n" +
        	"Summons a mini slime god to fight for you, the type depends on what world evil you have";
        CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
		modPlayer.slimeGod = true;
		if (player.whoAmI == Main.myPlayer)
		{
			if (player.FindBuffIndex(Mod.Find<ModBuff>("SlimeGod").Type) == -1)
			{
				player.AddBuff(Mod.Find<ModBuff>("SlimeGod").Type, 3600, true);
			}
			if (WorldGen.crimson && player.ownedProjectileCounts[Mod.Find<ModProjectile>("SlimeGodAlt").Type] < 1)
			{
				Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("SlimeGodAlt").Type, 33, 0f, Main.myPlayer, 0f, 0f);
			}
			else if (!WorldGen.crimson && player.ownedProjectileCounts[Mod.Find<ModProjectile>("SlimeGod").Type] < 1)
			{
				Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("SlimeGod").Type, 33, 0f, Main.myPlayer, 0f, 0f);
			}
		}
		if(player.statLife <= (player.statLifeMax2 * 0.8f) && player.statLife > (player.statLifeMax2 * 0.6f))
		{
			player.GetDamage(DamageClass.Summon) += 0.03f;
			player.moveSpeed += 0.025f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.6f) && player.statLife > (player.statLifeMax2 * 0.4f))
		{
			player.GetDamage(DamageClass.Summon) += 0.06f;
			player.moveSpeed += 0.05f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.4f) && player.statLife > (player.statLifeMax2 * 0.2f))
		{
			player.GetDamage(DamageClass.Summon) += 0.09f;
			player.moveSpeed += 0.1f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.2f))
		{
			player.GetDamage(DamageClass.Summon) += 0.15f;
			player.moveSpeed += 0.15f;
		}
    }
    
    public override void UpdateEquip(Player player)
    {
    	player.GetKnockback(DamageClass.Summon).Base += 1.5f;
    	player.GetDamage(DamageClass.Summon) += 0.1f;
    	player.maxMinions++;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "PurifiedGel", 5);
        recipe.AddIngredient(ItemID.HellstoneBar, 9);
		recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}