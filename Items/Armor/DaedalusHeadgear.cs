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
public class DaedalusHeadgear : ModItem
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
        Item.defense = 3; //33
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("DaedalusBreastplate").Type && legs.type == Mod.Find<ModItem>("DaedalusLeggings").Type;
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Grants minion damage and defensive boosts as health gets lower\n" +
        	"A daedalus crystal floats above you to protect you";
        CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
        modPlayer.daedalusCrystal = true;
        if (player.whoAmI == Main.myPlayer)
		{
			if (player.FindBuffIndex(Mod.Find<ModBuff>("DaedalusCrystal").Type) == -1)
			{
				player.AddBuff(Mod.Find<ModBuff>("DaedalusCrystal").Type, 3600, true);
			}
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("DaedalusCrystal").Type] < 1)
			{
				Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("DaedalusCrystal").Type, 0, 0f, Main.myPlayer, 0f, 0f);
			}
		}
        if(player.statLife <= (player.statLifeMax2 * 0.8f) && player.statLife > (player.statLifeMax2 * 0.6f))
		{
			player.endurance += 0.025f;
			player.GetDamage(DamageClass.Summon) += 0.025f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.6f) && player.statLife > (player.statLifeMax2 * 0.4f))
		{
			player.endurance += 0.05f;
			player.GetDamage(DamageClass.Summon) += 0.05f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.4f) && player.statLife > (player.statLifeMax2 * 0.2f))
		{
			player.endurance += 0.075f;
			player.GetDamage(DamageClass.Summon) += 0.075f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.2f))
		{
			player.endurance += 0.1f;
			player.GetDamage(DamageClass.Summon) += 0.1f;
		}
    }
    
    public override void UpdateEquip(Player player)
    {
    	player.GetDamage(DamageClass.Summon) += 0.1f;
    	player.maxMinions += 2;
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